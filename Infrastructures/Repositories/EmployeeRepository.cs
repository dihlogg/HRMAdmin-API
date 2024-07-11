using AdminHRM.Server.DataContext;
using AdminHRM.Server.Dtos;
using AdminHRM.Server.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace AdminHRM.Server.Infrastructures;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(HrmDbContext hrmDbContext) : base(hrmDbContext)
    {
    }

    public async Task<int> CountAsync()
    {
        return await _hrmDbContext.Employees.CountAsync();
    }
    public async Task<List<EmployeeDto>> GetPagedAsync(int page, int pageSize, string sortField, string sortOrder)
    {
        var query = _hrmDbContext.Employees.AsQueryable();

        if (!string.IsNullOrEmpty(sortField))
        {
            var sortOrderString = sortOrder.ToUpper() == "DESC" ? "descending" : "ascending";
            query = query.OrderBy($"{sortField} {sortOrderString}");
        }

        return await query.Skip((page - 1) * pageSize).Take(pageSize)
            .Select(s => new EmployeeDto()
            {
                Id = s.Id,
                LastName = s.LastName,
                FirstName = s.FirstName,
                JobTitle = s.JobTitle,
                Status = s.Status,
                SubUnitId = s.SubUnitId,
                SubUnitName = s.SubUnits.SubName,
                SupperVisor = new EmployeeParentChildDto()
                {
                    Id = s.SupperEmployee.Id,
                    FullName = s.SupperEmployee.FirstName + " " + s.SupperEmployee.LastName
                },
                EmployeeChildrens = s.Employees.Select(p => new EmployeeParentChildDto()
                {
                    Id = p.Id,
                    FullName = p.FirstName + " " + p.LastName
                }).ToList()
            }).ToListAsync();

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
                SubUnitName = s.SubUnits.SubName,
                SupperVisor = new EmployeeParentChildDto()
                {
                    Id = s.SupperEmployee.Id,
                    FullName = s.SupperEmployee.FirstName + " " + s.SupperEmployee.LastName
                },
                EmployeeChildrens = s.Employees.Select(p => new EmployeeParentChildDto()
                {
                    Id =p.Id,
                    FullName = p.FirstName + " " + p.LastName
                })
            })
            .ToListAsync();
    }

    public async Task<List<EmployeeDto>> SearchEmployeeDtosAsync(
        string? employeeName = null,
        string? status = null,
        string? jobTitle = null,
        string? supervisorName = null,
        string? subName = null)
    {
        var query = _hrmDbContext.Employees
        .Include(e => e.SubUnits)
        .Include(e => e.SupperEmployee)
        .AsQueryable();

        if (!string.IsNullOrEmpty(employeeName))
        {
            query = query.Where(s => (s.FirstName + " " + s.LastName).Contains(employeeName));
        }

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(s => s.Status == status);
        }

        if (!string.IsNullOrEmpty(jobTitle))
        {
            query = query.Where(s => s.JobTitle == jobTitle);
        }

        if (!string.IsNullOrEmpty(supervisorName))
        {
            query = query.Where(s => (s.SupperEmployee.FirstName + " " + s.SupperEmployee.LastName).Contains(supervisorName));
        }

        if (!string.IsNullOrEmpty(subName))
        {
            query = query.Where(s => s.SubUnits.SubName.Contains(subName));
        }

        return await query
            .AsNoTracking()
            .Select(s => new EmployeeDto()
            {
                Id = s.Id,
                LastName = s.LastName,
                FirstName = s.FirstName,
                JobTitle = s.JobTitle,
                Status = s.Status,
                SubUnitId = s.SubUnitId,
                SubUnitName = s.SubUnits.SubName,
                SupperVisor = new EmployeeParentChildDto()
                {
                    Id = s.SupperEmployee.Id,
                    FullName = s.SupperEmployee.FirstName + " " + s.SupperEmployee.LastName
                },
                EmployeeChildrens = s.Employees.Select(p => new EmployeeParentChildDto()
                {
                    Id = p.Id,
                    FullName = p.FirstName + " " + p.LastName
                })
            }).ToListAsync();
    }

    public IQueryable<Employee> Query()
    {
        return _hrmDbContext.Employees.AsQueryable();
    }
}
