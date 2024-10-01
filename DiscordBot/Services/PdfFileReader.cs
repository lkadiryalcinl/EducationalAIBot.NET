using DiscordBot.Interfaces;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System.Text;

namespace DiscordBot.Services
{
    public class PdfFileReader : IAnyFileReader
    {
        public string ReadFile(string filePath)
        {
            StringBuilder text = new();

            using (var pdfReader = new PdfReader(filePath))
            using (var pdfDocument = new PdfDocument(pdfReader))
            {
                for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
                {
                    var page = pdfDocument.GetPage(i);
                    var pageText = PdfTextExtractor.GetTextFromPage(page);
                    text.Append(pageText);
                }
            }

            return text.ToString();
        }
    }
}
