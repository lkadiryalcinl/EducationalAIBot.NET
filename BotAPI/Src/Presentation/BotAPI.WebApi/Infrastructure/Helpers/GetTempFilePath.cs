using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace BotAPI.WebApi.Infrastructure.Helpers
{
    public static class GetTempFilePath
    {
        public static async Task<string> GetPathAsync(IFormFile file)
        {
            var fileName = Path.GetFileName(file.FileName);

            var filePath = Path.Combine(Path.GetTempPath(), fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}
