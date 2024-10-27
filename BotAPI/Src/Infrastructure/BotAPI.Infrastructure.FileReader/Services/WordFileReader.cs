using BotAPI.Application.DTOs.FileReader.Responses;
using BotAPI.Application.Interfaces;
using BotAPI.Infrastructure.FileReader.Base;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Configuration;

namespace BotAPI.Infrastructure.FileReader.Services
{
    public class WordFileReader(IConfiguration configuration) : BaseFileReader(configuration), IAnyFileReader
    {
        public FileOutputModel ReadFile(string filePath)
        {
            FileOutputModel model = new();

            using WordprocessingDocument wordDocument = WordprocessingDocument.Open(filePath, false);
            var body = wordDocument?.MainDocumentPart?.Document.Body;

            int pageNumber = 1;

            foreach (var paragraph in body.Elements<Paragraph>())
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

            return model;
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
