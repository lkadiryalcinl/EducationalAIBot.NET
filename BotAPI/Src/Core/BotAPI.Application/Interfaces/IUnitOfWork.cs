using System.Threading.Tasks;

namespace BotAPI.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> SaveChangesAsync();
    }
}
