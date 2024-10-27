using Microsoft.Extensions.Configuration;
using Mscc.GenerativeAI;

namespace BotAPI.Infrastructure.FileReader.Base
{
    public abstract class BaseFileReader
    {
        private readonly string? GeminiApiKey;
        private readonly string? Image2TextPrompt;
        private readonly Content Image2TextPromptContent;

        protected readonly GoogleAI GoogleAI;
        protected readonly GenerativeModel GenerativeModel;
        protected BaseFileReader(IConfiguration configuration)
        {
            GeminiApiKey = configuration["Gemini:ApiKey"];
            Image2TextPrompt = configuration["Prompts:Image2TextPrompt"];
            Image2TextPromptContent = new Content($""" {Image2TextPrompt} """);

            GoogleAI = new GoogleAI(GeminiApiKey);
            GenerativeModel = GoogleAI.GenerativeModel(
                model:Model.Gemini15ProLatest,
                systemInstruction: Image2TextPromptContent
                );
        }

        //protected async Task<string> ProcessImageForText(string filePath, int pageNumber)
        //{
        //    // Create an image-to-text request to GoogleAI using the Image2TextPrompt
        //    var response = await GoogleAI.UploadFile(filePath);
            
        //    return response?.Text ?? "Unable to extract text from image.";
        //}

    }
}