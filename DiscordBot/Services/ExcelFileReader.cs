using ClosedXML.Excel;
using DiscordBot.Interfaces;
using System.Text;

namespace DiscordBot.Services
{
    public class ExcelFileReader : IAnyFileReader
    {
        public string ReadFile(string filePath)
        {
            using var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(1);
            StringBuilder text = new();

            foreach (var row in worksheet.RowsUsed())
            {
                foreach (var cell in row.CellsUsed())
                {
                    text.Append(cell.GetValue<string>() + " ");
                }
                text.AppendLine();
            }

            return text.ToString();
        }
    }
}
