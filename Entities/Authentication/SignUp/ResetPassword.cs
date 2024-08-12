using System.ComponentModel.DataAnnotations;

namespace AdminHRM.Server.Entities.Authentication.SignUp
{
    public class ResetPassword
    {
        [Required]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The Password and confirmation password don't match")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
