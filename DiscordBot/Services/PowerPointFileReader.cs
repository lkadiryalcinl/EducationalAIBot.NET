using DiscordBot.Interfaces;
using DocumentFormat.OpenXml.Packaging;
using System.Text;

namespace DiscordBot.Services
{
    public class PowerPointFileReader : IAnyFileReader
    {
        public string ReadFile(string filePath)
        {
            StringBuilder allText = new();
            using (PresentationDocument presentation = PresentationDocument.Open(filePath, false))
            {
                var slideParts = presentation?.PresentationPart.SlideParts;

                foreach (var slidePart in slideParts)
                {
                    var textElements = slidePart.Slide.Descendants<DocumentFormat.OpenXml.Drawing.Text>();
                    foreach (var text in textElements)
                    {
                        allText.AppendLine(text.Text);
                    }
                }
            }
            return allText.ToString();
        }
    }
}
