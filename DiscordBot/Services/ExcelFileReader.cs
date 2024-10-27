using ClosedXML.Excel;
using EducationalAIBot.Interfaces;
using EducationalAIBot.Models;

namespace EducationalAIBot.Services
{
    public class ExcelFileReader : IAnyFileReader
    {
        public List<FileContentModel> ReadFile(string filePath)
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

            return model.Contents;
        }
    }
}
