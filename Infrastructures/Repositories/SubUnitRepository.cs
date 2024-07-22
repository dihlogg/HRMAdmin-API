using AdminHRM.Server.DataContext;
using AdminHRM.Server.Entities;

namespace AdminHRM.Server.Infrastructures;

public interface ISubUnitRepository : IGenericRepository<SubUnit>
{
}

public class SubUnitRepository : GenericRepository<SubUnit>, ISubUnitRepository
{
    public SubUnitRepository(HrmDbContext hrmDbContext) : base(hrmDbContext)
    {
    }
}
