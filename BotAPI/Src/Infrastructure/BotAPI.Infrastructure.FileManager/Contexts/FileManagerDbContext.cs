using BotAPI.Infrastructure.FileManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BotAPI.Infrastructure.FileManager.Contexts
{
    public class FileManagerDbContext(DbContextOptions<FileManagerDbContext> options) : DbContext(options)
    {
        public DbSet<FileEntity> Files { get; set; }
    }
}
