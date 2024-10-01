using DiscordBot.Wrapper;

namespace DiscordBot.Interfaces
{
    public interface ISlashCommandsService
    {
        Task PingSlashCommandAsync(IInteractionContextWrapper context);
        Task LearnSlashCommandAsync(IInteractionContextWrapper context, string text);
    }
}