using BotAPI.Application.DTOs.AI.Requests;
using BotAPI.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Mscc.GenerativeAI;

namespace BotAPI.Infrastructure.AI.Services
{
    internal class AIProcessService : IAIProcessService
    {
        private readonly string? _geminiApiKey;
        private readonly GoogleAI googleAI;
        private readonly string? _learnPrompt;

        public AIProcessService(IConfiguration configuration)
        {
            _geminiApiKey = configuration["Gemini:ApiKey"];
            googleAI = new GoogleAI(_geminiApiKey);
            _learnPrompt = configuration["Prompts:LearnPrompt"];
        }

        public async Task<string> GetResponse(GetResultData resultData)
        {
            var learnPrompt = new Content($""" {_learnPrompt}: {resultData.FileContents} """);

            GenerativeModel model = googleAI.GenerativeModel(
                model: Model.Gemini15ProLatest,
                systemInstruction: learnPrompt);

            GenerateContentRequest content = new(resultData.Req);

            GenerateContentResponse response = await model.GenerateContent(content);

            return response.Text;
        }
    }
}
