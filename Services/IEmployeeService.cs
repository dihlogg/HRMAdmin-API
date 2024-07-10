using AdminHRM.Server.Dtos;

namespace AdminHRM.Server.Services;

public interface IEmployeeServive
{
    Task<List<EmployeeDto>> GetEmployeeDtosAsync();
    Task<bool> AddEmployeeAsync(EmployeeCreateDto employeeCreateDto);
    Task<bool?> EditEmployeeAsync(EmployeeDto employeeDto);
    Task<bool?> RemoveEmployeeDtosAsync(Guid id);
    Task<List<EmployeeDto>> SearchEmployeeDtosAsync(
        string? employeeName = null,
        string? status = null,
        string? jobTitle = null,
        string? supervisorName = null,
        string? subName = null);
    Task<PagedResult<EmployeeDto>> GetPagedEmployeesAsync(int page, int pageSize);

}

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new List<T>();
    public int TotalCount { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}