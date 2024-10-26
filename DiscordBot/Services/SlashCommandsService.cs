using System.Net.NetworkInformation;
using System.Text;
using DiscordBot.Interfaces;
using DiscordBot.Models;
using DiscordBot.Wrapper;
using DSharpPlus;
using DSharpPlus.Entities;
using Microsoft.Extensions.Configuration;
using Mscc.GenerativeAI;
using Newtonsoft.Json;

namespace DiscordBot.Services
{
    public class SlashCommandsService : ISlashCommandsService
    {
        private readonly string? _geminiApiKey;
        private readonly string? _systemPrompt;

        public SlashCommandsService(IConfiguration configuration)
        {
            _geminiApiKey = configuration["Gemini:ApiKey"];
            _systemPrompt = configuration["SystemPrompt:Text"];
        }

        public async Task PingSlashCommandAsync(IInteractionContextWrapper ctx)
        {
            await SendInitialResponseAsync(ctx, "Ping...");

            var latency = GetPingLatency("google.com");

            var embedMessage = CreateEmbedMessage("Pong!", $"Gecikme: {latency} ms", ctx.User);

            await ctx.Channel.SendMessageAsync(embedMessage);
            await FinalizeResponseAsync(ctx, nameof(PingSlashCommandAsync));
        }

        public async Task LearnSlashCommandAsync(IInteractionContextWrapper ctx, string text)
        {
            string filePath = "C:\\Users\\ASUS\\Desktop\\Veri_Madenciligi\\veri.pdf";

            List<FileContentModel> FileText = FileReaderClient.ProcessFile(filePath);

            await SendInitialResponseAsync(ctx, $"{ctx.User.Mention} tarafından gelen istek: {text}");
            DiscordEmbedBuilder embedMessage = await AIProccess(ctx, text, FileText);

            await ctx.Channel.SendMessageAsync(embedMessage);
            await FinalizeResponseAsync(ctx, nameof(LearnSlashCommandAsync), text);
        }

        // Yardımcı Metotlar
        private async Task<DiscordEmbedBuilder> AIProccess(IInteractionContextWrapper ctx, string text, List<FileContentModel> fileText)
        {
            var serializedTextBuilder = new StringBuilder();

            foreach (var page in fileText)
            {
                serializedTextBuilder.AppendLine($"Page Number: {page.PageNumber} |");
                serializedTextBuilder.AppendLine($"Page Content: {page.Content} ||");
            }

            string SerializedText = serializedTextBuilder.ToString();

            var systemPrompt = new Content(
            $"""
                 {_systemPrompt}: {SerializedText}
            """
            );

            GoogleAI googleAI = new(_geminiApiKey);

            GenerativeModel model = googleAI.GenerativeModel(
                model: Model.Gemini15ProLatest,
                systemInstruction: systemPrompt);

            GenerateContentRequest request = new(text);

            GenerateContentResponse response = await model.GenerateContent(request);

            DiscordEmbedBuilder embedMessage = CreateEmbedMessage("Öğretici Yapay Zeka Botu", response.Text, ctx.User);

            return embedMessage;
        }

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
