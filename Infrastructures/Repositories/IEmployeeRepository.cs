using AdminHRM.Server.Dtos;
using AdminHRM.Server.Entities;

namespace AdminHRM.Server.Infrastructures;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    Task<List<EmployeeDto>> GetInCludeParentChild();
    Task<int> CountAsync();
    IQueryable<Employee> Query();
    IQueryable<Employee> AsQueryable();
    Task<List<EmployeeDto>> GetPagedAsync(int page, int pageSize, string[] sortFields, string[] sortOrders);
    Task<List<EmployeeDto>> SearchEmployeeDtosAsync(
        string? employeeName = null,
        string? status = null,
        string? jobTitle = null,
        string? supervisorName = null,
        string? subName = null);
}
