using Microsoft.AspNetCore.Identity;

namespace AdminHRM.Server.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public IList<Employee>? Employees { get; set; }
    }
}
