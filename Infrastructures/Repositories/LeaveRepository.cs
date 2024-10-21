using AdminHRM.Dtos;
using AdminHRM.Dtos.Leaves;
using AdminHRM.Entities;
using AdminHRM.Server.DataContext;
using AdminHRM.Server.Entities;
using AdminHRM.Server.Infrastructures;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace AdminHRM.Infrastructures.Repositories;

public interface ILeaveRepository : IGenericRepository<Leave>
{
    Task<List<LeaveDto>> GetOnlyLeaves();
    Task<List<LeaveDto>> SearchLeaveDtosAsync(SearchLeaveDto searchLeaveDto);
}
public class LeaveRepository : GenericRepository<Leave>, ILeaveRepository
{
    public LeaveRepository(HrmDbContext hrmDbContext) : base(hrmDbContext)
    {
    }

    public async Task<List<LeaveDto>> GetOnlyLeaves()
    {
        var query = _hrmDbContext.Leaves
            .AsQueryable()
            .AsNoTracking();
        return await query
            .Select(s => new LeaveDto()
            {
                Id = s.Id,
                FromDate = s.FromDate,
                ToDate = s.ToDate,
                LeaveStatus = s.LeaveStatus,
                LeaveType = s.LeaveType,
                Comment = s.Comment,
                EmployeeId = s.Employees.Id,
                SubName = s.Employees.SubUnits.SubName,
                EmployeeName = s.Employees.FirstName + " " + s.Employees.LastName,
            })
            .ToListAsync();
    }

    public async Task<List<LeaveDto>> SearchLeaveDtosAsync(SearchLeaveDto searchLeaveDto)
    {
        var query = _hrmDbContext.Leaves
            .Include(s => s.Employees)
            .AsQueryable();
        if (searchLeaveDto.FromDate.HasValue)
        {
            query = query.Where(s => s.FromDate >= searchLeaveDto.FromDate.Value);
        }
        if (searchLeaveDto.ToDate.HasValue)
        {
            query = query.Where(s => s.ToDate <= searchLeaveDto.ToDate.Value);
        }
        if (!string.IsNullOrEmpty(searchLeaveDto.LeaveType))
        {
            query = query.Where( s => s.LeaveType == searchLeaveDto.LeaveType);
        }
        if (!string.IsNullOrEmpty(searchLeaveDto.LeaveStatus))
        {
            query = query.Where( s => s.LeaveStatus == searchLeaveDto.LeaveStatus);
        }
        if (!string.IsNullOrEmpty(searchLeaveDto.EmployeeName))
        {
            query = query.Where(s => (s.Employees.FirstName + " " + s.Employees.LastName)
                .Contains(searchLeaveDto.EmployeeName));
        }
        if (!string.IsNullOrEmpty(searchLeaveDto.SubName))
        {
            query = query.Where( s => s.Employees.SubUnits.SubName == searchLeaveDto.SubName);
        }
        return await query.AsNoTracking()
            .Select(s => new LeaveDto()
            {
                Id = s.Id,
                FromDate = s.FromDate,
                ToDate = s.ToDate,
                LeaveType = s.LeaveType,
                LeaveStatus = s.LeaveStatus,
                EmployeeName = s.Employees.FirstName + " " + s.Employees.LastName,
                SubName = s.Employees.SubUnits.SubName,
            }).ToListAsync();
    }
}
