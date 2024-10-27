using BotAPI.Application.Parameters;

namespace BotAPI.Application.DTOs.Account.Requests
{
    public class GetAllUsersRequest : PaginationRequestParameter
    {
        public string Name { get; set; }
    }
}
