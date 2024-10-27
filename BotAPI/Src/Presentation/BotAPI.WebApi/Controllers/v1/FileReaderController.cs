using BotAPI.Infrastructure.FileReader.Clients;
using BotAPI.WebApi.Infrastructure.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BotAPI.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class FileReaderController(IConfiguration configuration) : BaseApiController
    {
        private readonly IConfiguration _configuration = configuration;
        [HttpGet]
        public string ReadFileFromFilePath(string filepath) {
            FileReaderClient readerClient = new(_configuration);
            return SerializeFileContent.SerializeFile(readerClient.ProcessFile(filepath)); 
        }

    }
}
