using BotAPI.Application.DTOs.FileReader.Responses;
using System.Threading.Tasks;

namespace BotAPI.Application.Interfaces
{
    public interface IFileManagerService
    {
        Task CreateVector(string ChannelId, FileOutputModel fileOutput);
    }

}
