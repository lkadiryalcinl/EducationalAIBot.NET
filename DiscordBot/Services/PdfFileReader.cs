using DiscordBot.Interfaces;
using DiscordBot.Models;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;

namespace DiscordBot.Services
{
    public class PdfFileReader : IAnyFileReader
    {
        public List<FileContentModel> ReadFile(string filePath)
        {
            FileOutputModel model = new();

            using (var pdfReader = new PdfReader(filePath))
            using (var pdfDocument = new PdfDocument(pdfReader))
            {
                for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
                {
                    var page = pdfDocument.GetPage(i);
                    var pageText = PdfTextExtractor.GetTextFromPage(page);
                    model.Contents.Add(new FileContentModel { Content = pageText, PageNumber = i});
                }
            }

            return model.Contents;
        }
    }
}
