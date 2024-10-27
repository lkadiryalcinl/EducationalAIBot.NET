using BotAPI.Application.Interfaces;
using BotAPI.Infrastructure.FileReader.Services;
using Microsoft.Extensions.Configuration;

namespace BotAPI.Infrastructure.FileReader.Factories
{
    internal class AnyFileReaderFactory(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;
        public IAnyFileReader GetFileReader(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();

            return extension switch
            {
                ".pdf" => new PdfFileReader(_configuration),
                ".docx" => new WordFileReader(_configuration),
                ".xlsx" => new ExcelFileReader(_configuration),
                ".pptx" => new PowerPointFileReader(_configuration),
                _ => throw new NotSupportedException($"File type {extension} is not supported."),
            };
        }
    }
}
