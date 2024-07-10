using AdminHRM.Server.Dtos;
using AdminHRM.Server.Entities;

namespace AdminHRM.Server.Infrastructures;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    Task<List<EmployeeDto>> GetInCludeParentChild();
    Task<List<EmployeeDto>> SearchEmployeeDtosAsync(
        string? employeeName = null,
        string? status = null,
        string? jobTitle = null,
        string? supervisorName = null,
        string? subName = null);
    Task<int> CountAsync();
    Task<List<Employee>> GetPagedAsync(int page, int pageSize);
}
