using AdminHRM.Entities;
using AdminHRM.Server.DataContext;
using AdminHRM.Server.Entities;
using AdminHRM.Server.Infrastructures;

namespace AdminHRM.Infrastructures.Repositories;

public interface ILeaveRepository : IGenericRepository<Leave>
{
}
public class LeaveRepository : GenericRepository<Leave>, ILeaveRepository
{
    public LeaveRepository(HrmDbContext hrmDbContext) : base(hrmDbContext)
    {
    }
}
