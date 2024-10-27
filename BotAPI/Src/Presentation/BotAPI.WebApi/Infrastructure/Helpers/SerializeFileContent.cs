using BotAPI.Application.DTOs.FileReader.Responses;
using System.Text;

namespace BotAPI.WebApi.Infrastructure.Helpers
{
    public static class SerializeFileContent
    {
        public static string SerializeFile(FileOutputModel FileContents)
        {
            var serializedTextBuilder = new StringBuilder();

            foreach (var page in FileContents.Contents)
            {
                serializedTextBuilder.AppendLine($"Page Number: {page.PageNumber} |");
                serializedTextBuilder.AppendLine($"Page Content: {page.Content} ||");
            }

            return serializedTextBuilder.ToString();
        }
    }
}
