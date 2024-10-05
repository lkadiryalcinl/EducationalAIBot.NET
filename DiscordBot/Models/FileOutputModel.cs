namespace DiscordBot.Models
{
    public class FileOutputModel
    {
        public List<FileContentModel> Contents { get; set; }

        public FileOutputModel()
        {
            Contents = [];
        }
    }
}
