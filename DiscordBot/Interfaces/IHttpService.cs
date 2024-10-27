using EducationalAIBot.Services;
using RestSharp;

namespace EducationalAIBot.Interfaces
{
    public interface IHttpService
    {
        public Task<HttpResponse> GetResponseFromUrl(string resource, Method method = Method.Get,
            string? errorMessage = null, List<KeyValuePair<string, string>>? headers = null, object? jsonBody = null);
    }
}