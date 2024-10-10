using AdminHRM.Dtos;
using AdminHRM.Dtos.Leaves;
using AdminHRM.Entities;
using AdminHRM.Server.DataContext;
using AdminHRM.Server.Entities;
using AdminHRM.Server.Infrastructures;
using Microsoft.EntityFrameworkCore;

namespace AdminHRM.Infrastructures.Repositories;

public interface ILeaveRepository : IGenericRepository<Leave>
{
    Task<List<LeaveDto>> GetOnlyLeaves();
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
                EmployeeId = s.Employees.Id,
                SubName = s.Employees.SubUnits.SubName,
                EmployeeName = s.Employees.FirstName + " " + s.Employees.LastName,
            })
            .ToListAsync();
    }
}
