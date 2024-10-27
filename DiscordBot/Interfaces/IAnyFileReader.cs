using EducationalAIBot.Models;

namespace EducationalAIBot.Interfaces
{
    public interface IAnyFileReader
    {
        List<FileContentModel> ReadFile(string filePath);
    }
}
