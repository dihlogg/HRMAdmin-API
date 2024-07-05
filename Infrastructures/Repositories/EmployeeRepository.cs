using AdminHRM.Server.DataContext;
using AdminHRM.Server.Dtos;
using AdminHRM.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdminHRM.Server.Infrastructures;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(HrmDbContext hrmDbContext) : base(hrmDbContext)
    {
    }

    public async Task<List<EmployeeDto>> GetInCludeParentChild()
    {
        var query = _hrmDbContext.Employees
            .AsQueryable()
            .AsNoTracking();
        return await query
            .Select(s => new EmployeeDto()
            {
                Id = s.Id,
                LastName = s.LastName,
                FirstName = s.FirstName,
                JobTitle = s.JobTitle,
                Status = s.Status,
                SubUnitId = s.SubUnitId,
                SupperVisor = new EmployeeParentChildDto()
                {
                    Id = s.SupperEmployee.Id,
                    FullName = s.SupperEmployee.FirstName + s.SupperEmployee.LastName
                },
                EmployeeChildrens = s.Employees.Select(p => new EmployeeParentChildDto()
                {
                    Id =p.Id,
                    FullName = p.FirstName + p.LastName
                })
            })
            .ToListAsync();
    }
}
