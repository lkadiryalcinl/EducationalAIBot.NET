using System.Net.NetworkInformation;
using DiscordBot.Interfaces;
using DiscordBot.Wrapper;
using DSharpPlus;
using DSharpPlus.Entities;
using Microsoft.Extensions.Configuration;
using Mscc.GenerativeAI;

namespace DiscordBot.Services
{
    public class SlashCommandsService : ISlashCommandsService
    {
        private readonly string? _geminiApiKey;

        public SlashCommandsService(IConfiguration configuration) => _geminiApiKey = configuration["Gemini:ApiKey"];

        public async Task PingSlashCommandAsync(IInteractionContextWrapper ctx)
        {
            await SendInitialResponseAsync(ctx, "Ping...");

            var latency = GetPingLatency("google.com");

            var embedMessage = CreateEmbedMessage("Pong!", $"Gecikme: {latency} ms", ctx.User);

            await ctx.Channel.SendMessageAsync(embedMessage);
            await FinalizeResponseAsync(ctx, nameof(PingSlashCommandAsync));
        }

        public async Task ChatGeminiSlashCommandAsync(IInteractionContextWrapper ctx, string text)
        {
            await SendInitialResponseAsync(ctx, $"{ctx.User.Mention} tarafından gelen istek: {text}");

            var googleAI = new GoogleAI(_geminiApiKey);
            var model = googleAI.GenerativeModel(model: Model.Gemini15ProLatest);
            var response = await model.GenerateContent(text);

            var embedMessage = CreateEmbedMessage("Öğretici Yapay Zeka Botu", response.Text, ctx.User);

            await ctx.Channel.SendMessageAsync(embedMessage);
            await FinalizeResponseAsync(ctx, nameof(ChatGeminiSlashCommandAsync), text);
        }

        // Yardımcı Metotlar
        private static async Task SendInitialResponseAsync(IInteractionContextWrapper ctx, string message)
        {
            await ctx.Channel.SendMessageAsync(message);
            await ctx.CreateResponseAsync(InteractionResponseType.DeferredChannelMessageWithSource,
                new DiscordInteractionResponseBuilder().WithContent("Cevap üretiliyor..."));
        }

        private static long GetPingLatency(string host)
        {
            using Ping pinger = new();
            PingReply reply = pinger.Send(host);
            return reply.RoundtripTime;
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
