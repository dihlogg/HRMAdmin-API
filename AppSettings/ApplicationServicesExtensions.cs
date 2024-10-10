using AdminHRM.Server.Infrastructures;
using Microsoft.Extensions.DependencyInjection;
using AdminHRM.Server.Infrastructures;
using AdminHRM.Server.Services;
using AdminHRM.Infrastructures.Repositories;
using AdminHRM.Services.Implements;

namespace AdminHRM.Server.AppSettings;

public static class ApplicationServicesExtensions
{
    public static void AddApplicationServicesExtension(this IServiceCollection services)
    {
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<ISubUnitRepository, SubUnitRepository>();
        services.AddTransient<ISubUnitService, SubUnitService>();
        services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        services.AddTransient<IEmployeeServive, EmployeeServive>();
        services.AddTransient<ILeaveRepository, LeaveRepository>();
        services.AddTransient<ILeaveServive, LeaveService>();
    }
}
