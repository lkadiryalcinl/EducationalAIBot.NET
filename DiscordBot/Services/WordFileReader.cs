using DiscordBot.Interfaces;
using DiscordBot.Models;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace DiscordBot.Services
{
    public class WordFileReader : IAnyFileReader
    {
        public List<FileContentModel> ReadFile(string filePath)
        {
            FileOutputModel model = new();

            using WordprocessingDocument wordDocument = WordprocessingDocument.Open(filePath, false);
            var body = wordDocument?.MainDocumentPart?.Document.Body;

            int pageNumber = 1;

            foreach (var paragraph in body.Elements<DocumentFormat.OpenXml.Wordprocessing.Paragraph>())
            {
                
                model.Contents.Add(new FileContentModel
                {
                    Content = paragraph.InnerText,
                    PageNumber = pageNumber
                });

                
                 if (IsPageBreak(paragraph)) 
                {
                    pageNumber++;
                }
            }

            return model.Contents;
        }

        private static bool IsPageBreak(Paragraph paragraph)
        {
            foreach (var run in paragraph.Elements<Run>())
            {
                var breakElement = run.Elements<Break>().FirstOrDefault();
                if (breakElement != null && breakElement.Type == BreakValues.Page)
                {
                    return true; 
                }
            }
            return false; 
        }
    }
}
