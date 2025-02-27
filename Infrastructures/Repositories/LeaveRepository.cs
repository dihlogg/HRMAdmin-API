using AdminHRM.Dtos;
using AdminHRM.Dtos.Leaves;
using AdminHRM.Entities;
using AdminHRM.Server.DataContext;
using AdminHRM.Server.Entities;
using AdminHRM.Server.Infrastructures;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace AdminHRM.Infrastructures.Repositories;
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
}

public class RequestReasonRepository : GenericRepository<RequestReason>, IRequestReasonRepository
{
    public RequestReasonRepository(HrmDbContext hrmDbContext) : base(hrmDbContext)
    {
    }
}
public interface IRequestStatusRepository : IGenericRepository<RequestStatus>
{
}

public class RequestStatusRepository : GenericRepository<RequestStatus>, IRequestStatusRepository
{
    public RequestStatusRepository(HrmDbContext hrmDbContext) : base(hrmDbContext)
    {
    }
}

public interface IRequestTypeRepository : IGenericRepository<RequestType>
{
}

public class RequesttypeRepository : GenericRepository<RequestType>, IRequestTypeRepository
{
    public RequesttypeRepository(HrmDbContext hrmDbContext) : base(hrmDbContext)
    {
    }
}

