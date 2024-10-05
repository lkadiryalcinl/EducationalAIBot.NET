using DiscordBot.Models;

namespace DiscordBot.Interfaces
{
    public interface IAnyFileReaderAsync
    {
        Task<List<FileContentModel>> ReadFileAsync(string filePath, bool IsImage);
    }
}
