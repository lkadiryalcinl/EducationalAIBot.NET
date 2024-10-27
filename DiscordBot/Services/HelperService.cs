using EducationalAIBot.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace EducationalAIBot.Services
{
    public class HelperService : IHelperService
    {
        private readonly List<string> _developerExcuses = new();
        private readonly ILogger<HelperService> _logger;

        // Constructor chaining to avoid repetition
        public HelperService(ILogger<HelperService> logger) : this(logger, null) { }

        public HelperService(ILogger<HelperService> logger, string? excusesFilePath)
        {
            _logger = logger;
            LoadDeveloperExcuses(excusesFilePath);
        }

        // Method to load developer excuses from a JSON file
        private void LoadDeveloperExcuses(string? excusesFilePath)
        {
            string filePath = excusesFilePath ?? Path.Combine(Directory.GetCurrentDirectory(), "Data", "excuses.json");

            if (!File.Exists(filePath))
            {
                _logger.LogError("Geliştirici bahaneleri dosyası bulunamadı: {FilePath}", filePath);
                return;
            }

            try
            {
                string jsonContent = File.ReadAllText(filePath);
                JObject json = JObject.Parse(jsonContent);
                _developerExcuses.AddRange(json["tr"]?.ToObject<List<string>>() ?? throw new Exception());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bahaneler dosyası yüklenirken hata oluştu: {FilePath}", filePath);
            }
        }

        // Returns a random excuse
        public Task<string> GetRandomDeveloperExcuseAsync()
        {
            if (_developerExcuses.Count == 0)
            {
                return Task.FromResult("Geliştirici bahanesi bulunamadı. Lütfen yapılandırmayı kontrol edin.");
            }

            Random random = new();
            int index = random.Next(_developerExcuses.Count);
            return Task.FromResult(_developerExcuses[index]);
        }
    }
}
