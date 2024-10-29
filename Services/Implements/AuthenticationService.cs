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
    Task<(Response response, string? token, DateTime? expiration, object? employee)> LoginUserAsync(LoginUser loginUser);
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

    public async Task<(Response response, string? token, DateTime? expiration, object? employee)> LoginUserAsync(LoginUser loginUser)
    {
        {
            var user = await _userManager.FindByNameAsync(loginUser.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginUser.Password))
            {
                // Create claims
                var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }

                // Generate token with claims
                var jwtToken = GetToken(authClaims); // Assuming GetToken is a private method to generate JWT
                var tokenString = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                // Fetch Employee details
                var employee = await _identityContext.Employees
                    .Include(e => e.SubUnits) // Assuming navigation property for sub-unit
                    .FirstOrDefaultAsync(e => e.UserId == user.Id);

                if (employee == null)
                {
                    return (new Response { Status = "Error", Message = "Employee not found for the user" }, null, null, null);
                }

                // Prepare employee info
                var employeeInfo = new
                {
                    employee.EmployeeId,
                    employee.FirstName,
                    employee.LastName,
                    employee.JobTitle,
                    employee.Status,
                    employee.SupperEmployee,
                    employee.SubUnits?.SubName
                };

                return (new Response { Status = "Success", Message = "Login successful" }, tokenString, jwtToken.ValidTo, employeeInfo);
            }

            return (new Response { Status = "Error", Message = "Unauthorized" }, null, null, null);
        }

        JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            // Token generation logic
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            return new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }
    }

    public async Task<Response?> RegisterUserAsync(RegisterUser registerUser, string role, IUrlHelper urlHelper, HttpRequest request)
    {
        // Check user existence
        var userExist = await _userManager.FindByEmailAsync(registerUser.Email);
        if (userExist != null)
        {
            return new Response { Status = "Error", Message = "User already exists!" };
        }

        // Check if the Employee exists
        var employee = await _identityContext.Employees.FirstOrDefaultAsync(e => e.Id == registerUser.EmployeeId);
        if (employee == null)
        {
            return new Response { Status = "Error", Message = "Employee not found!" };
        }

        // Create a new IdentityUser
        IdentityUser user = new()
        {
            Email = registerUser.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = registerUser.UserName,
            TwoFactorEnabled = true,
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

            // Update Employee with UserId
            employee.UserId = user.Id;
            _identityContext.Employees.Update(employee);
            await _identityContext.SaveChangesAsync();

            // Send confirmation email
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = urlHelper.Action("ConfirmEmail", "Authentication", new { token, email = user.Email }, request.Scheme);
            var message = new Message(new[] { user.Email }, "Confirmation Email Link", confirmationLink!);
            _emailService.SendEmail(message);

            return new Response { Status = "Success", Message = $"User created and confirmation email sent to {user.Email}!" };
        }

        return new Response { Status = "Error", Message = "Role does not exist!" };
    }


}
