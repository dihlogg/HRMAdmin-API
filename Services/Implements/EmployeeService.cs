using AdminHRM.Dtos;
using AdminHRM.Server.Entities;
using AdminHRM.Server.Entities.Authentication.SignUp;
using AdminHRM.Server.Infrastructures;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace AdminHRM.Server.Services;

public interface IEmployeeServive
{
    Task<List<EmployeeDto>> GetEmployeeDtosAsync();

    Task<Response> AddEmployeeAsync(EmployeeCreateDto employeeCreateDto);

    Task<bool?> EditEmployeeAsync(EmployeeDto employeeDto);

    Task<bool?> RemoveEmployeeDtosAsync(Guid id);

    Task<List<EmployeeDto>> SearchEmployeeDtosAsync(SearchEmployeeDto searchEmployeeDto);

    Task<PagedResult<EmployeeDto>> GetPagedEmployeesAsync(int page, int pageSize, string[] sortFields, string[] sortOrders);

    Task<Employee?> GetEmployeeByIdAsync(Guid id);

    Task<EmployeeDto?> GetEmployeeByUserIdAsync(string userId);
}

public class EmployeeServive : IEmployeeServive
{
    private readonly ILogger<EmployeeServive> _logger;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public EmployeeServive(IEmployeeRepository employeeRepository, ILogger<EmployeeServive> logger, IMapper mapper, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _employeeRepository = employeeRepository;
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<List<EmployeeDto>> GetEmployeeDtosAsync()
    {
        try
        {
            return await _employeeRepository.GetInCludeParentChild();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<Response> AddEmployeeAsync(EmployeeCreateDto employeeCreateDto)
    {
        try
        {
            string userId = null;
            if (!string.IsNullOrEmpty(employeeCreateDto.UserName))
            {
                // Check user exist
                var userExist = await _userManager.FindByEmailAsync(employeeCreateDto.Email);
                if (userExist != null)
                {
                    return new Response { Status = "Error", Message = "User already exists!" };
                }

                // Create a new IdentityUser
                IdentityUser user = new()
                {
                    Email = employeeCreateDto.Email,
                    UserName = employeeCreateDto.UserName,
                    TwoFactorEnabled = false,
                };
                // Create the user and assign role
                if (await _roleManager.RoleExistsAsync("Admin"))
                {
                    var result = await _userManager.CreateAsync(user, employeeCreateDto.Password);
                    if (!result.Succeeded)
                    {
                        return new Response { Status = "Error", Message = "User creation failed!" };
                    }
                    await _userManager.AddToRoleAsync(user, "Admin");
                    userId = await _userManager.GetUserIdAsync(user);
                }
            }
            var info = _mapper.Map<Employee>(employeeCreateDto);
            info.UserId = userId;
            var results = await _employeeRepository.AddAsync(info);
            if (results)
            {
                return new Response { Status = "Success", Message = $"User created Success" };
            }
            else
            {
                return new Response { Status = "Error", Message = $"User created Fail" };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool?> EditEmployeeAsync(EmployeeDto employeeDto)
    {
        try
        {
            var userInfo = await _employeeRepository.GetByIdAsync(employeeDto.Id);
            if (userInfo == null)
            {
                return null;
            }
            var infoUpdate = _mapper.Map<Employee>(employeeDto);
            var result = await _employeeRepository.UpdateAsync(infoUpdate);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool?> RemoveEmployeeDtosAsync(Guid id)
    {
        try
        {
            return await _employeeRepository.DeleteByKey(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<Employee?> GetEmployeeByIdAsync(Guid id)
    {
        try
        {
            return await _employeeRepository.GetEmployeeByIdAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<List<EmployeeDto>> SearchEmployeeDtosAsync(SearchEmployeeDto searchEmployeeDto)
    {
        try
        {
            return await _employeeRepository.SearchEmployeeDtosAsync(searchEmployeeDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<PagedResult<EmployeeDto>> GetPagedEmployeesAsync(int page, int pageSize, string[] sortFields, string[] sortOrders)
    {
        try
        {
            var totalEmployees = await _employeeRepository.CountAsync().ConfigureAwait(false);
            var employees = await _employeeRepository.GetPagedAsync(page, pageSize, sortFields, sortOrders).ConfigureAwait(false);

            return new PagedResult<EmployeeDto>
            {
                Items = employees,
                TotalCount = totalEmployees,
                PageIndex = page,
                PageSize = pageSize,
                SortFields = sortFields,
                SortOrders = sortOrders
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<EmployeeDto?> GetEmployeeByUserIdAsync(string userId)
    {
        var employee = await _employeeRepository.GetEmployeeByUserIdAsync(userId);
        if (employee == null) return null;

        return new EmployeeDto
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            JobTitle = employee.JobTitle,
            Status = employee.Status,
            UserId = employee.UserId,
            UserName = employee.User.UserName,
            Email = employee.User.Email
        };
    }
}