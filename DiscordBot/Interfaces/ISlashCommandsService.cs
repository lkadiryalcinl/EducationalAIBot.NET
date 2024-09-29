using DiscordBot.Wrapper;

namespace DiscordBot.Interfaces
{
    public interface ISlashCommandsService
    {
        Task PingSlashCommandAsync(IInteractionContextWrapper context);
        Task ChatGeminiSlashCommandAsync(IInteractionContextWrapper context, string text);
    }
}