using BotAPI.Application.DTOs.FileReader.Responses;

namespace BotAPI.Application.Interfaces
{
    public interface IAnyFileReader
    {
        FileOutputModel ReadFile(string filePath);
    }
}
