using Microsoft.Extensions.Configuration;
using Mscc.GenerativeAI;

namespace EducationalAIBot.Services
{
    public static class ImageFileReader
    {
        private static readonly string? _geminiApiKey;

        static ImageFileReader()
        {
            _geminiApiKey = "AIzaSyC6av3X6aATgXSgQsv6Xw3i_8H_Q-svPVo";
        }

        public static async Task<string> ReadFile(string filePath)
        {

            var prompt = "You are an image reader AI and you will analyze and Interpretatiate images that i gave you";

            GoogleAI googleAI = new(_geminiApiKey);

            GenerativeModel model = googleAI.GenerativeModel(
                model: Model.GeminiProVisionLatest);

            GenerateContentRequest request = new(prompt);

            await request.AddMedia(uri: filePath, mimeType: "application/pdf");

            GenerateContentResponse response = await model.GenerateContent(request);

            return response.Text;
        }
    }
}
