using EducationalAIBot.Models;

namespace EducationalAIBot.Interfaces
{
    public interface IAnyFileReaderAsync
    {
        Task<List<FileContentModel>> ReadFileAsync(string filePath, bool IsImage);
    }
}
