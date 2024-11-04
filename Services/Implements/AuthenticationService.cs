using AdminHRM.Server.DataContext;
using AdminHRM.Server.Entities.Authentication.SignUp;
using AdminHRM.Server.Entities;
using AdminHRM.Server.Services.Implements;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AdminHRM.Server.Infrastructures;
using AdminHRM.Server.Services;
using AutoMapper;
using AdminHRM.Services;
using AdminHRM.Server.Entities.Authentication.SignIn;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdminHRM.Server.Services;

public interface IAuthenticationService
{
    Task<Response?> RegisterUserAsync(RegisterUser registerUser, string role, IUrlHelper urlHelper, HttpRequest request);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    private readonly IdentityContext _identityContext;
    private readonly ILogger<AuthenticationService> _logger;
    private readonly IMapper _mapper;

    public AuthenticationService(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IEmailService emailService,
            IdentityContext identityContext,
            ILogger<AuthenticationService> logger,
            IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _emailService = emailService;
        _identityContext = identityContext;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Response?> RegisterUserAsync(RegisterUser registerUser, string role, IUrlHelper urlHelper, HttpRequest request)
    {
        // Check user existence
        var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
        if (userExist != null)
        {
            return new Response { Status = "Error", Message = "User already exists!" };
        }

        // Create a new IdentityUser
        IdentityUser user = new()
        {
            Email = registerUser.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = registerUser.UserName,
            TwoFactorEnabled = false,
        };

        // Create the user and assign role
        if (await _roleManager.RoleExistsAsync(role))
        {
            var result = await _userManager.CreateAsync(user, registerUser.Password);
            if (!result.Succeeded)
            {
                return new Response { Status = "Error", Message = "User creation failed!" };
            }

            await _userManager.AddToRoleAsync(user, role);


            // Send confirmation email
            if(urlHelper != null)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = urlHelper.Action("ConfirmEmail", "Authentication", new { token, email = user.Email }, request.Scheme);
                var message = new Message(new[] { user.Email }, "Confirmation Email Link", confirmationLink!);
                _emailService.SendEmail(message);
            }

            return new Response { Status = "Success", Message = $"User created and confirmation email sent to {user.Email}!" };
        }

        return new Response { Status = "Error", Message = "Role does not exist!" };
    }


}
