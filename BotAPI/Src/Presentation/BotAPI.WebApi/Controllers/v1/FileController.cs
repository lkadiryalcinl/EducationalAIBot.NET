using BotAPI.Application.DTOs.FileReader.Responses;
using BotAPI.Application.Interfaces;
using BotAPI.Application.Wrappers;
using BotAPI.Infrastructure.FileReader.Clients;
using BotAPI.WebApi.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;

namespace BotAPI.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class FileController(IFileManagerService fileManagerService,IConfiguration configuration) : BaseApiController
    {
        private readonly IConfiguration _configuration = configuration;

        [HttpPost]
        public async Task<IActionResult> UploadFileAsync(IFormFile file)
        {
            var filePath = GetTempFilePath.GetPathAsync(file);

            FileReaderClient readerClient = new(_configuration);
            var data = readerClient.ProcessFile(filePath.Result);
            fileManagerService.CreateVector("EducationalAIBot", data).Wait();
            return Ok(); 
        }
    }
}
