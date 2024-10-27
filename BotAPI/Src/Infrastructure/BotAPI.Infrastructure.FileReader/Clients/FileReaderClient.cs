using BotAPI.Application.DTOs.FileReader.Responses;
using BotAPI.Infrastructure.FileReader.Factories;
using Microsoft.Extensions.Configuration;

namespace BotAPI.Infrastructure.FileReader.Clients
{
    public class FileReaderClient(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;
        public FileOutputModel ProcessFile(string filePath)
        {
            try
            {
                AnyFileReaderFactory readerFactory = new(_configuration);
                return readerFactory.GetFileReader(filePath).ReadFile(filePath);
            }
            catch (Exception)
            {
                return new FileOutputModel() { };
            }
        }
    }
}
