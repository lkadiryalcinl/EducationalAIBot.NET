using BotAPI.Application.DTOs.FileReader.Responses;
using BotAPI.Application.Interfaces;
using BotAPI.Infrastructure.FileReader.Base;
using DocumentFormat.OpenXml.Packaging;
using Microsoft.Extensions.Configuration;

namespace BotAPI.Infrastructure.FileReader.Services
{
    public class PowerPointFileReader(IConfiguration configuration) : BaseFileReader(configuration), IAnyFileReader
    {
        public FileOutputModel ReadFile(string filePath)
        {
            FileOutputModel model = new();
            int pageNumber = 1;

            using (PresentationDocument presentation = PresentationDocument.Open(filePath, false))
            {
                var slideParts = presentation?.PresentationPart.SlideParts;

                foreach (var slidePart in slideParts)
                {

                    var textElements = slidePart.Slide.Descendants<DocumentFormat.OpenXml.Drawing.Text>();
                    foreach (var text in textElements)
                    {
                        model.Contents.Add(new FileContentModel { Content = text.Text, PageNumber = pageNumber });
                    }
                    pageNumber++;
                }
            }
            return model;
        }
    }
}
