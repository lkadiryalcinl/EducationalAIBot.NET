using EducationalAIBot.Interfaces;

namespace EducationalAIBot.Services
{
    public class AnyFileReaderFactory
    {
        public static IAnyFileReader GetFileReader(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();

            return extension switch
            {
                ".pdf" => new PdfFileReader(),
                ".docx" => new WordFileReader(),
                ".xlsx" => new ExcelFileReader(),
                ".pptx" => new PowerPointFileReader(),
                _ => throw new NotSupportedException($"File type {extension} is not supported."),
            };
        }
    }
}
