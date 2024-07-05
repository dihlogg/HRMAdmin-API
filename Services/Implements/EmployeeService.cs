using AutoMapper;
using AdminHRM.Server.Dtos;
using AdminHRM.Server.Entities;
using AdminHRM.Server.Infrastructures;

namespace AdminHRM.Server.Services;

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
}