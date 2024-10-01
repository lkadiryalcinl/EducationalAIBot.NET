using DiscordBot.Interfaces;
using DocumentFormat.OpenXml.Packaging;

namespace DiscordBot.Services
{
    public class WordFileReader : IAnyFileReader
    {
        public string ReadFile(string filePath)
        {
            using WordprocessingDocument wordDocument = WordprocessingDocument.Open(filePath, false);
            var body = wordDocument.MainDocumentPart.Document.Body;
            return body.InnerText;
        }
    }
}
