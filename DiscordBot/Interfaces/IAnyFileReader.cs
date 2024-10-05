using DiscordBot.Models;

namespace DiscordBot.Interfaces
{
    public interface IAnyFileReader
    {
        List<FileContentModel> ReadFile(string filePath);
    }
}
