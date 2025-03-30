using System.ComponentModel.DataAnnotations;

namespace AdminHRM.Dtos.Authentication
{
    public class UserDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? EmployeeId { get; set; }
    }
}
