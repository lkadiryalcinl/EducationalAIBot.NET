namespace BotAPI.Application.DTOs.AI.Requests
{
    public record GetResultData
    {
        public string Req { get; set; }
        public string FileContents { get; set; }
    }
}
