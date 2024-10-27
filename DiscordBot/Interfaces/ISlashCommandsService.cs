using EducationalAIBot.Wrapper;

namespace EducationalAIBot.Interfaces
{
    public interface ISlashCommandsService
    {
        Task PingSlashCommandAsync(IInteractionContextWrapper context);
        Task LearnSlashCommandAsync(IInteractionContextWrapper context, string text);
    }
}