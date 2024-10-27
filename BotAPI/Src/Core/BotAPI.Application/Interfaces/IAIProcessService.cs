using BotAPI.Application.DTOs.AI.Requests;
using Mscc.GenerativeAI;
using System.Threading.Tasks;

namespace BotAPI.Application.Interfaces
{
    public interface IAIProcessService
    {
        Task<string> GetResponse(GetResultData resultData);
    }
}
