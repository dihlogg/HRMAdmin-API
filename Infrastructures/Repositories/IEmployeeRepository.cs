using AdminHRM.Server.Dtos;
using AdminHRM.Server.Entities;

namespace AdminHRM.Server.Infrastructures;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    Task<List<EmployeeDto>> GetInCludeParentChild();
}
