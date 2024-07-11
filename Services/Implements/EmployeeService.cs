using AutoMapper;
using AdminHRM.Server.Dtos;
using AdminHRM.Server.Entities;
using AdminHRM.Server.Infrastructures;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

    public async Task<List<EmployeeDto>> SearchEmployeeDtosAsync(
        string? employeeName = null,
        string? status = null,
        string? jobTitle = null,
        string? supervisorName = null,
        string? subName = null)
    {
        try
        {
            return await _employeeRepository.SearchEmployeeDtosAsync(employeeName, supervisorName, status, jobTitle, subName);
            //return _mapper.Map<List<EmployeeDto>>(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<PagedResult<EmployeeDto>> GetPagedEmployeesAsync(int page, int pageSize, string sortField, string sortOrder)
    {
        try
        {
            var query = _employeeRepository.Query();
            // Sorting
            if (!string.IsNullOrEmpty(sortField))
            {
                var sortOrderString = sortOrder.ToUpper() == "DESC" ? "descending" : "ascending";
                query = query.OrderBy($"{sortField} {sortOrderString}");
            }

            var totalEmployees = await _employeeRepository.CountAsync();
            var employees = await _employeeRepository.GetPagedAsync(page, pageSize, sortField, sortOrder);
            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

            return new PagedResult<EmployeeDto>
            {
                Items = employeeDtos,
                TotalCount = totalEmployees,
                PageIndex = page,
                PageSize = pageSize,
                SortField = sortField,
                SortOrder = sortOrder
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}