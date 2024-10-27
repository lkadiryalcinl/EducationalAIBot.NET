using BotAPI.Application.DTOs.FileReader.Responses;
using BotAPI.Application.Interfaces;
using BotAPI.Infrastructure.FileReader.Base;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using Microsoft.Extensions.Configuration;

namespace BotAPI.Infrastructure.FileReader.Services
{
    public class PdfFileReader(IConfiguration configuration) : BaseFileReader(configuration), IAnyFileReader
    {
        public FileOutputModel ReadFile(string filePath)
        {
            FileOutputModel model = new();

            using (var pdfReader = new PdfReader(filePath))
            using (var pdfDocument = new PdfDocument(pdfReader))
            {
                for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
                {
                    var page = pdfDocument.GetPage(i);
                    var pageText = PdfTextExtractor.GetTextFromPage(page);
                    model.Contents.Add(new FileContentModel { Content = pageText, PageNumber = i });
                }
            }
             
            return model;
        }
    }
}
