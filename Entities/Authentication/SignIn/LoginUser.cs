using System.ComponentModel.DataAnnotations;

namespace AdminHRM.Server.Entities.Authentication.SignIn
{
    public class LoginUser
    {
        [Required ( ErrorMessage = "User Name is Required")]
        public string UserName { get; set; }

        [Required ( ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}
