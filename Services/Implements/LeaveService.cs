using AdminHRM.Dtos;
using AdminHRM.Dtos.Leaves;
using AdminHRM.Entities;
using AdminHRM.Infrastructures.Repositories;
using AdminHRM.Server.Dtos;
using AdminHRM.Server.Entities;
using AdminHRM.Server.Infrastructures;
using AdminHRM.Server.Services;
using AutoMapper;

namespace AdminHRM.Services.Implements;

public interface ILeaveServive
{
    Task<List<LeaveDto>> GetLeaveDtosAsync();
    Task<bool> AddLeaveAsync(Dtos.Leaves.LeaveCreateDto leaveCreateDto);
    Task<bool?> EditLeaveAsync(LeaveDto leaveDto);
    Task<bool?> RemoveLeaveDtosAsync(Guid id);
}

public class LeaveService : ILeaveServive
{
    private readonly ILogger<LeaveService> _logger;
    private readonly ILeaveRepository _leaveRepository;
    private readonly IMapper _mapper;


    public LeaveService(ILeaveRepository leaveRepository, ILogger<LeaveService> logger, IMapper mapper)
    {
        _leaveRepository = leaveRepository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<List<LeaveDto>> GetLeaveDtosAsync()
    {
        try
        {
            var data = await _leaveRepository.GetOnlyLeaves();
            return data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    public async Task<bool> AddLeaveAsync(Dtos.Leaves.LeaveCreateDto leaveCreateDto)
    {
        try
        {
            var info = _mapper.Map<Leave>(leaveCreateDto);
            return await _leaveRepository.AddAsync(info);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool?> EditLeaveAsync(LeaveDto leaveDto)
    {
        try
        {
            var userInfo = await _leaveRepository.GetByIdAsync(leaveDto.Id);
            if (userInfo == null)
            {
                return null;
            }
            var infoUpdate = _mapper.Map<Leave>(leaveDto);
            var result = await _leaveRepository.UpdateAsync(infoUpdate);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }


    public async Task<bool?> RemoveLeaveDtosAsync(Guid id)
    {
        try
        {
            return await _leaveRepository.DeleteByKey(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}
