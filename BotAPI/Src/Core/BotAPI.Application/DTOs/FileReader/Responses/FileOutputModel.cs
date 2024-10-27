using System.Collections.Generic;

namespace BotAPI.Application.DTOs.FileReader.Responses
{
    public class FileOutputModel
    {
        public ICollection<FileContentModel> Contents { get; set; }

        public FileOutputModel()
        {
            Contents = [];
        }
    }
}
