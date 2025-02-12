using Microsoft.AspNetCore.Identity;
using System;

namespace BotAPI.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
