using DiscordBot.Interfaces;
using DiscordBot.Models;
using DocumentFormat.OpenXml.Packaging;
using System.Text;

namespace DiscordBot.Services
{
    public class PowerPointFileReader : IAnyFileReader
    {
        public List<FileContentModel> ReadFile(string filePath)
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
            return model.Contents;
        }
    }
}
