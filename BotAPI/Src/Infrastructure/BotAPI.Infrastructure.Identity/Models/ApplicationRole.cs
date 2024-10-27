using Microsoft.AspNetCore.Identity;
using System;

namespace BotAPI.Infrastructure.Identity.Models
{
    public class ApplicationRole(string name) : IdentityRole<Guid>(name)
    {
    }
}
