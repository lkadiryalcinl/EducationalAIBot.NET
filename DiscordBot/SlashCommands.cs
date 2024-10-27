using DSharpPlus.SlashCommands;
using EducationalAIBot.Interfaces;
using EducationalAIBot.Wrapper;

namespace EducationalAIBot
{
    public class SlashCommands(ISlashCommandsService slashCommandsService) : ApplicationCommandModule
    {
        [SlashCommand("ping",
        "This is a basic ping command to check if the bot is online and what the current latency is.")]
        public async Task PingSlashCommand(InteractionContext ctx)
        {
            InteractionContextWrapper context = new(ctx);
            await slashCommandsService.PingSlashCommandAsync(context);
        }

        [SlashCommand("learn",
            "Ask a question related to a lesson to the Teaching AI bot and wait for the answer.")]
        public async Task ChatGeminiSlashCommand(InteractionContext ctx,
            [Option("prompt", "Ask questions about the topic you want to learn.")]
        string text)
        {
            InteractionContextWrapper context = new(ctx);
            await slashCommandsService.LearnSlashCommandAsync(context, text);
        }
    }

}