using BotAPI.Application.DTOs.Account.Requests;
using BotAPI.Application.DTOs.Account.Responses;
using BotAPI.Application.Wrappers;
using System.Threading.Tasks;

namespace BotAPI.Application.Interfaces.UserInterfaces
{
    public interface IGetUserServices
    {
        Task<PagedResponse<UserDto>> GetPagedUsers(GetAllUsersRequest model);
    }
}
