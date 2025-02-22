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

public interface ILeaveDashboardCardRepository : IGenericRepository<DashboardCard>
{
    Task<bool?> DeleteByCardId(string cardId);
    Task<DashboardCard?> GetByCardIdAsync(string cardId);
}

public class LeaveDashboardCardRepository : GenericRepository<DashboardCard>, ILeaveDashboardCardRepository
{
    public LeaveDashboardCardRepository(HrmDbContext hrmDbContext) : base(hrmDbContext)
    {
    }

    public async Task<bool?> DeleteByCardId(string cardId)
    {
        try
        {
            var entity = await _hrmDbContext.DashboardCards
                .FirstOrDefaultAsync(x => x.CardId == cardId);

            if (entity == null)
                return false;

            _hrmDbContext.DashboardCards.Remove(entity);
            await _hrmDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<DashboardCard?> GetByCardIdAsync(string cardId)
    {
        return await _hrmDbContext.DashboardCards.FirstOrDefaultAsync(c => c.CardId == cardId);
    }
}

public interface IRequestReasonRepository : IGenericRepository<RequestReason>
{
    Task<bool?> DeleteByReasonId(string reasonId);
    Task<RequestReason?> GetByReasonIdAsync(string reasonId);
}

public class RequestReasonRepository : GenericRepositoryWithoutBase<RequestReason>, IRequestReasonRepository
{
    public RequestReasonRepository(HrmDbContext hrmDbContext) : base(hrmDbContext)
    {
    }
    public async Task<bool?> DeleteByReasonId(string reasonId)
    {
        try
        {
            var entity = await _hrmDbContext.RequestReason
                .FirstOrDefaultAsync(x => x.ReasonId == reasonId);

            if (entity == null)
                return false;

            _hrmDbContext.RequestReason.Remove(entity);
            await _hrmDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<RequestReason?> GetByReasonIdAsync(string reasonId)
    {
        return await _hrmDbContext.RequestReason.FirstOrDefaultAsync(c => c.ReasonId == reasonId);
    }
}
public interface IRequestStatusRepository : IGenericRepository<RequestStatus>
{
    Task<bool?> DeleteByStatusId(string reasonId);
    Task<RequestStatus?> GetByStatusIdAsync(string reasonId);
}

public class RequestStatusRepository : GenericRepositoryWithoutBase<RequestStatus>, IRequestStatusRepository
{
    public RequestStatusRepository(HrmDbContext hrmDbContext) : base(hrmDbContext)
    {
    }
    public async Task<bool?> DeleteByStatusId(string statusId)
    {
        try
        {
            var entity = await _hrmDbContext.RequestStatus
                .FirstOrDefaultAsync(x => x.StatusId == statusId);

            if (entity == null)
                return false;

            _hrmDbContext.RequestStatus.Remove(entity);
            await _hrmDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<RequestStatus?> GetByStatusIdAsync(string statusId)
    {
        return await _hrmDbContext.RequestStatus.FirstOrDefaultAsync(c => c.StatusId == statusId);
    }
}

