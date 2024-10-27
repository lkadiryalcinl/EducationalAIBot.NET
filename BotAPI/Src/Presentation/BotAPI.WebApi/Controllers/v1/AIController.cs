using BotAPI.Application.DTOs.AI.Requests;
using BotAPI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BotAPI.WebApi.Controllers.v1
{
    [ApiVersion("1")]
    public class AIController(IAIProcessService processService) : BaseApiController
    {
        private readonly IAIProcessService _processService = processService;
        
        [HttpPost]
        public async Task<string> GetResult(GetResultData resultData) => await _processService.GetResponse(resultData);


    }
}
