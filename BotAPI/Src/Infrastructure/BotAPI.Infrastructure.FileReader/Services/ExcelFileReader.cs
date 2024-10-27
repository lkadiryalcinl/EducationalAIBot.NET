using BotAPI.Application.DTOs.FileReader.Responses;
using BotAPI.Application.Interfaces;
using BotAPI.Infrastructure.FileReader.Base;
using ClosedXML.Excel;
using Microsoft.Extensions.Configuration;

namespace BotAPI.Infrastructure.FileReader.Services
{
    public class ExcelFileReader(IConfiguration configuration) : BaseFileReader(configuration), IAnyFileReader
    {
        public FileOutputModel ReadFile(string filePath)
        {
            using var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(1);
            FileOutputModel model = new();
            int pageNumber = 1;

            foreach (var row in worksheet.RowsUsed())
            {
                foreach (var cell in row.CellsUsed())
                {
                    model.Contents.Add(new FileContentModel { Content = cell.GetValue<string>() + " ", PageNumber = pageNumber });

                }
                pageNumber++;
            }

            return model;
        }
    }
}
