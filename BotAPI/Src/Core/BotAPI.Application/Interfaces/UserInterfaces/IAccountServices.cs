using BotAPI.Application.DTOs.Account.Requests;
using BotAPI.Application.DTOs.Account.Responses;
using BotAPI.Application.Wrappers;
using System.Threading.Tasks;

namespace BotAPI.Application.Interfaces.UserInterfaces
{
    public interface IAccountServices
    {
        Task<BaseResult<string>> RegisterGhostAccount();
        Task<BaseResult> ChangePassword(ChangePasswordRequest model);
        Task<BaseResult> ChangeUserName(ChangeUserNameRequest model);
        Task<BaseResult<AuthenticationResponse>> Authenticate(AuthenticationRequest request);
        Task<BaseResult<AuthenticationResponse>> AuthenticateByUserName(string username);

    }
}
