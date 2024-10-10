using AdminHRM.Dtos;
using AdminHRM.Server.DataContext;
using AdminHRM.Server.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Data;
using System.Linq;
using System;

namespace AdminHRM.Server.Infrastructures;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    Task<List<EmployeeDto>> GetInCludeParentChild();

    Task<int> CountAsync();

    IQueryable<Employee> Query();

    IQueryable<Employee> AsQueryable();

    Task<List<EmployeeDto>> GetPagedAsync(int page, int pageSize, string[] sortFields, string[] sortOrders);

    Task<List<EmployeeDto>> SearchEmployeeDtosAsync(SearchEmployeeDto searchEmployeeDto);
    Task<Employee?> GetEmployeeByIdAsync(Guid id);
}

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(HrmDbContext hrmDbContext) : base(hrmDbContext)
    {
    }

    public async Task<int> CountAsync()
    {
        return await _hrmDbContext.Employees.CountAsync();
    }

    public async Task<List<EmployeeDto>> GetPagedAsync(int page, int pageSize, string[] sortFields, string[] sortOrders)
    {
        var query = _hrmDbContext.Employees.AsQueryable();

        if (sortFields != null && sortOrders != null && sortFields.Length == sortOrders.Length)
        {
            for (int i = 0; i < sortFields.Length; i++)
            {
                var sortOrderString = sortOrders[i].ToUpper() == "DESC" ? "descending" : "ascending";
                query = i == 0 ? query.OrderBy($"{sortFields[i]} {sortOrderString}")
                               : ((IOrderedQueryable<Employee>)query).ThenBy($"{sortFields[i]} {sortOrderString}");
            }
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
                    Id = p.Id,
                    FullName = p.FirstName + " " + p.LastName
                })
            })
            .ToListAsync();
    }

    public async Task<List<EmployeeDto>> SearchEmployeeDtosAsync(SearchEmployeeDto searchEmployeeDto)
    {
        var query = _hrmDbContext.Employees
        .Include(e => e.SubUnits)
        .Include(e => e.SupperEmployee)
        .AsQueryable();

        if (!string.IsNullOrEmpty(searchEmployeeDto.EmployeeName))
        {
            query = query.Where(s => (s.FirstName + " " + s.LastName).Contains(searchEmployeeDto.EmployeeName));
        }

        if (!string.IsNullOrEmpty(searchEmployeeDto.Status))
        {
            query = query.Where(s => s.Status == searchEmployeeDto.Status);
        }

        if (!string.IsNullOrEmpty(searchEmployeeDto.JobTitle))
        {
            query = query.Where(s => s.JobTitle == searchEmployeeDto.JobTitle);
        }

        if (!string.IsNullOrEmpty(searchEmployeeDto.SupervisorName))
        {
            query = query.Where(s => (s.SupperEmployee.FirstName + " " + s.SupperEmployee.LastName).Contains(searchEmployeeDto.SupervisorName));
        }

        if (!string.IsNullOrEmpty(searchEmployeeDto.SubName))
        {
            query = query.Where(s => s.SubUnits.SubName.Contains(searchEmployeeDto.SubName));
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

    public IQueryable<Employee> AsQueryable()
    {
        throw new NotImplementedException();
    }

    public async Task<Employee?> GetEmployeeByIdAsync(Guid id)
    {
        return await _hrmDbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
    }
}