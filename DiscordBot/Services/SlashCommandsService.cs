using System.Net.NetworkInformation;
using System.Text;
using DSharpPlus;
using DSharpPlus.Entities;
using EducationalAIBot.Interfaces;
using EducationalAIBot.Wrapper;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace EducationalAIBot.Services
{
    public class SlashCommandsService(IConfiguration configuration, IHttpService httpService) : ISlashCommandsService
    {
        private readonly string? _botApiUrl = configuration["BotAPI:Url"];
        private readonly IHttpService _httpService = httpService;

        public async Task PingSlashCommandAsync(IInteractionContextWrapper ctx)
        {
            await SendInitialResponseAsync(ctx, "Ping...");

            using Ping pinger = new();
            PingReply reply = pinger.Send("google.com");
            var latency = reply.RoundtripTime;

            var embedMessage = CreateEmbedMessage("Pong!", $"Gecikme: {latency} ms", ctx.User);

            await ctx.Channel.SendMessageAsync(embedMessage);
            await FinalizeResponseAsync(ctx, nameof(PingSlashCommandAsync));
        }

        public async Task LearnSlashCommandAsync(IInteractionContextWrapper ctx, string text)
        {
            string filePath = "C:\\Users\\ASUS\\Desktop\\Veri_Madenciligi\\veri.pdf";

            var FileContents = await _httpService.GetResponseFromUrl($"{_botApiUrl}/FileReader/ReadFileFromFilePath?filepath={filePath}");

            await SendInitialResponseAsync(ctx, $"{ctx.User.Mention} tarafından gelen istek: {text}");

            var encodedFileContents = Convert.ToBase64String(Encoding.UTF8.GetBytes(FileContents.Content));

            var response = await _httpService.GetResponseFromUrl(
                resource: $"{_botApiUrl}/AI/GetResult",
                method: Method.Post,
                jsonBody: new
                {
                    req = text,
                    fileContents = encodedFileContents
                }
            );

            DiscordEmbedBuilder embedMessage = CreateEmbedMessage("Öğretici Yapay Zeka Botu", response.Content, ctx.User);

            await ctx.Channel.SendMessageAsync(embedMessage);
            await FinalizeResponseAsync(ctx, nameof(LearnSlashCommandAsync), text);
        }

        // Yardımcı Metotlar
        private static async Task SendInitialResponseAsync(IInteractionContextWrapper ctx, string message)
        {
            await ctx.Channel.SendMessageAsync(message);
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource,
                new DiscordInteractionResponseBuilder().WithContent("Cevap üretiliyor..."));
        }

        private static DiscordEmbedBuilder CreateEmbedMessage(string title, string description, DiscordUser user)
        {
            return new DiscordEmbedBuilder
            {
                Title = title,
                Description = description,
                Timestamp = DateTimeOffset.UtcNow,
                Author = new DiscordEmbedBuilder.EmbedAuthor
                {
                    Name = user.Username,
                    IconUrl = user.AvatarUrl
                }
            };
        }

        private static async Task FinalizeResponseAsync(IInteractionContextWrapper ctx, string commandName, string? inputText = null)
        {
            await ctx.DeleteResponseAsync();
            LogCommandExecution(ctx, commandName, inputText);
        }

        private static void LogCommandExecution(IInteractionContextWrapper ctx, string commandName, string? inputText = null)
        {
            var logMessage = $"Komut '{commandName}' başarıyla çalıştırıldı. Kullanıcı: {ctx.User.Username} ({ctx.User.Id})";
            if (!string.IsNullOrEmpty(inputText))
                logMessage += $". Girdi metni: {inputText}";

            Program.Log(logMessage);
        }

    }


}
