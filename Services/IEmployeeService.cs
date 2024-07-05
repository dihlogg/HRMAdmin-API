using AdminHRM.Server.Dtos;

namespace AdminHRM.Server.Services;

public interface IEmployeeServive
{
    Task<List<EmployeeDto>> GetEmployeeDtosAsync();
    Task<bool> AddEmployeeAsync(EmployeeCreateDto employeeCreateDto);
    Task<bool?> EditEmployeeAsync(EmployeeDto employeeDto);
    Task<bool?> RemoveEmployeeDtosAsync(Guid id);
}
