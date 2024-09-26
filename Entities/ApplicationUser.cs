using Microsoft.AspNetCore.Identity;

namespace AdminHRM.Server.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public Employee Employee { get; set; }
    }
}
