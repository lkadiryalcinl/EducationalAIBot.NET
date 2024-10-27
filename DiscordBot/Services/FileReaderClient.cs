using EducationalAIBot.Models;

namespace EducationalAIBot.Services
{
    public static class FileReaderClient
    {
        public static List<FileContentModel> ProcessFile(string filePath)
        {
            try
            {
                return AnyFileReaderFactory.GetFileReader(filePath).ReadFile(filePath);
            }
            catch (Exception ex)
            {
                return [new FileContentModel() { Content = ex.ToString(), PageNumber = ex.Data.Count }];
            }
        }
    }
}
