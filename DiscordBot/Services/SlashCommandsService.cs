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

        public async Task LearnSlashCommandAsync(IInteractionContextWrapper ctx, string text)
        {
            string filePath = "C:\\Users\\ASUS\\Desktop\\Veri_Madenciligi\\veri.pdf";

            string FileText = FileReaderClient.ProcessFile(filePath);

            await SendInitialResponseAsync(ctx, $"{ctx.User.Mention} tarafından gelen istek: {text}");
            DiscordEmbedBuilder embedMessage = await AIProccess(ctx, text, FileText);

            await ctx.Channel.SendMessageAsync(embedMessage);
            await FinalizeResponseAsync(ctx, nameof(LearnSlashCommandAsync), text);
        }

        private async Task<DiscordEmbedBuilder> AIProccess(IInteractionContextWrapper ctx, string text, string fileText)
        {
            var systemPrompt = new Content(
            $"""
                You are an edicational AI who will answer to the juniour year collage students.
                You will answer which launguage will students ask to you. 
                Additionally you will answer the questions just on that i will provide the text.
                If the answer is not inside of the text you can answer like the answer is not inside of the file and of course you will answer the error message which laungage the conversation is.
                The provided text : {fileText}
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
