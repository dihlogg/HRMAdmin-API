using AdminHRM.Dtos;
using AdminHRM.Server.Entities;
using AdminHRM.Server.Infrastructures;
using AutoMapper;

namespace AdminHRM.Server.Services;

public interface IEmployeeServive
{
    Task<List<EmployeeDto>> GetEmployeeDtosAsync();
    Task<bool> AddEmployeeAsync(EmployeeCreateDto employeeCreateDto);
    Task<bool?> EditEmployeeAsync(EmployeeDto employeeDto);
    Task<bool?> RemoveEmployeeDtosAsync(Guid id);
    Task<List<EmployeeDto>> SearchEmployeeDtosAsync(SearchEmployeeDto searchEmployeeDto);

    /// <summary>
    /// Get paging for PIM screen
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="sortFields"></param>
    /// <param name="sortOrders"></param>
    /// <returns></returns>
    Task<PagedResult<EmployeeDto>> GetPagedEmployeesAsync(int page, int pageSize, string[] sortFields, string[] sortOrders);

}

public class EmployeeServive : IEmployeeServive
{
    private readonly ILogger<EmployeeServive> _logger;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;


    public EmployeeServive(IEmployeeRepository employeeRepository, ILogger<EmployeeServive> logger, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<List<EmployeeDto>> GetEmployeeDtosAsync()
    {
        try
        {
            return await _employeeRepository.GetInCludeParentChild();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool> AddEmployeeAsync(EmployeeCreateDto employeeCreateDto)
    {
        try
        {
            var info = _mapper.Map<Employee>(employeeCreateDto);
            return await _employeeRepository.AddAsync(info);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool?> EditEmployeeAsync(EmployeeDto employeeDto)
    {
        try
        {
            var userInfo = await _employeeRepository.GetByIdAsync(employeeDto.Id);
            if (userInfo == null)
            {
                return null;
            }
            var infoUpdate = _mapper.Map<Employee>(employeeDto);
            var result = await _employeeRepository.UpdateAsync(infoUpdate);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool?> RemoveEmployeeDtosAsync(Guid id)
    {
        try
        {
            return await _employeeRepository.DeleteByKey(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<List<EmployeeDto>> SearchEmployeeDtosAsync(SearchEmployeeDto searchEmployeeDto)
    {
        try
        {
            return await _employeeRepository.SearchEmployeeDtosAsync(searchEmployeeDto);
            //return _mapper.Map<List<EmployeeDto>>(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<PagedResult<EmployeeDto>> GetPagedEmployeesAsync(int page, int pageSize, string[] sortFields, string[] sortOrders)
    {
        try
        {
            var totalEmployees = await _employeeRepository.CountAsync().ConfigureAwait(false);
            var employees = await _employeeRepository.GetPagedAsync(page, pageSize, sortFields, sortOrders).ConfigureAwait(false);
            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

            return new PagedResult<EmployeeDto>
            {
                Items = employeeDtos,
                TotalCount = totalEmployees,
                PageIndex = page,
                PageSize = pageSize,
                SortFields = sortFields,
                SortOrders = sortOrders
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}