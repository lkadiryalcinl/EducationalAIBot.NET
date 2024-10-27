using System.Diagnostics;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using EducationalAIBot.Interfaces;
using EducationalAIBot.Services;
using EducationalAIBot.Wrapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using RestSharp;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace EducationalAIBot
{
    public class Program
    {
        private string? _discordToken;
        public DiscordClient? Client { get; private set; }
        public static ILogger? Logger { get; private set; }
        public static IHelperService? HelperService { get; private set; }
        public static ISlashCommandsService? SlashCommandsService { get; private set; }
        private static int StatusIndex { get; set; } = 0;


        public static Task Main() => new Program().MainAsync();

        public async Task MainAsync()
        {
            ConfigureNLog();
            IConfigurationRoot configuration = LoadConfiguration();
            await using var services = ConfigureServices(configuration);

            Logger = services.GetService<ILogger<Program>>();
            HelperService = services.GetService<IHelperService>();
            SlashCommandsService = services.GetService<ISlashCommandsService>();

            if (!ServicesInitialized())
                return;

            _discordToken = configuration["DiscordBot:Token"] ?? string.Empty;
            if (!ValidateDiscordToken())
                return;

            var config = CreateDiscordConfiguration(services);
            Client = new DiscordClient(config);

            ConfigureSlashCommands(services);
            ConfigureInteractivity();

            SlashCommandsExtension slash = Client.GetSlashCommands();

            await Client.ConnectAsync();
            StartStatusUpdateTimer();

            await Task.Delay(-1);
        }

        private static void ConfigureNLog() =>
            LogManager.Setup().LoadConfigurationFromFile("nlog.config");

        private static IConfigurationRoot LoadConfiguration() =>
            new ConfigurationBuilder()
                .SetBasePath("C:\\Users\\ASUS\\AppData\\Roaming\\Microsoft\\UserSecrets\\096c0e02-9bdc-41f2-9b09-d83effc29096")
                .AddJsonFile("secrets.json")
                .Build();

        private static ServiceProvider ConfigureServices(IConfiguration configuration) =>
            new ServiceCollection()
                .AddSingleton(configuration)
                .AddSingleton<IHttpService, HttpService>()
                .AddSingleton<IHelperService, HelperService>()
                .AddSingleton<IInteractionContextWrapper, InteractionContextWrapper>()
                .AddSingleton<ISlashCommandsService, SlashCommandsService>()
                .AddSingleton<SlashCommands>()
                .AddSingleton<IRestClient>(_ => new RestClient())
                .AddLogging(loggingBuilder => loggingBuilder.AddNLog())
                .BuildServiceProvider();

        private static bool ServicesInitialized()
        {
            if (HelperService == null || Logger == null || SlashCommandsService == null)
            {
                Log("Not all services could be loaded. Please check the code.", LogLevel.Critical);
                Environment.Exit(500);
                return false;
            }
            return true;
        }

        private bool ValidateDiscordToken()
        {
            if (string.IsNullOrEmpty(_discordToken))
            {
                Log("Could not load Discord token. Please check if a valid token was provided and restart the bot!", LogLevel.Error);
                Environment.Exit(404);
                return false;
            }
            Log("Got Token: " + _discordToken);
            return true;
        }

        private DiscordConfiguration CreateDiscordConfiguration(ServiceProvider services) =>
            new()
            {
                Token = _discordToken,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged | DiscordIntents.GuildMessages,
                LoggerFactory = services.GetService<ILoggerFactory>()
            };

        private void ConfigureSlashCommands(ServiceProvider services)
        {
            var slashCommandsConfig = Client.UseSlashCommands(new SlashCommandsConfiguration
            {
                Services = services
            });

            Log("Starting to register slash commands...", LogLevel.Information);

            try
            {
                slashCommandsConfig.RegisterCommands<SlashCommands>();
                Log("Slash commands registered successfully.", LogLevel.Information);
            }
            catch (Exception ex)
            {
                Log($"Error registering slash commands: {ex.Message}", LogLevel.Error);
                Log($"Stack Trace: {ex.StackTrace}", LogLevel.Error);
            }
        }

        private void ConfigureInteractivity() =>
            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(1)
            });

        private void StartStatusUpdateTimer()
        {
            System.Timers.Timer timer = new(15000);
            timer.Elapsed += async (_, _) => await UpdateStatusAsync();
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private async Task UpdateStatusAsync()
        {
            switch (StatusIndex)
            {
                case 0:
                    await UpdateStatusAsync($"Available to '{Client.Guilds.Sum(g => g.Value.MemberCount)}' Users", ActivityType.Watching);
                    break;
                case 1:
                    await UpdateStatusAsync($"Excuse: {await GetDeveloperExcuse()}", ActivityType.ListeningTo);
                    break;
            }

            StatusIndex = (StatusIndex + 1) % 3;
        }

        private static async Task<string> GetDeveloperExcuse()
        {
            string? developerExcuse = await HelperService.GetRandomDeveloperExcuseAsync();
            return developerExcuse.Length > 110 ? developerExcuse[..110] : developerExcuse;
        }

        private async Task UpdateStatusAsync(string activityMessage, ActivityType activityType) =>
            await Client.UpdateStatusAsync(new DiscordActivity(activityMessage, activityType));

        internal static void Log(string? msg, LogLevel logLevel = LogLevel.Information)
        {
            if (Logger != null)
                Logger.Log(logLevel, "{Message}", msg);
            else
                Console.WriteLine(msg);
        }
    }
}
