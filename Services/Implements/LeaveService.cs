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
    Task<List<LeaveDto>> SearchLeaveDtosAsync(SearchLeaveDto searchLeaveDto);
    Task<List<LeaveDashboardCardDto>> GetDashboardCardsAsync();
    Task<bool> AddDashboardCardsAsync(LeaveDashboardCreateDto leaveDashboardCreateDto);
    Task<bool?> EditDashboardCardsAsync(LeaveDashboardCreateDto leaveDashboardCreateDto);
    Task<bool?> RemoveDashboardCardsAsync(string cardId);
    Task<List<LeaveReasonDto>> GetRequestReasonAsync();
    Task<bool> AddRequestReasonAsync(LeaveReasonDto leaveReasonDto);
    Task<bool?> EditRequestReasonAsync(LeaveReasonDto leaveReasonDto);
    Task<bool?> RemoveRequestReasonAsync(string reasonId);
    Task<List<LeaveStatusDto>> GetRequestStatusAsync();
    Task<bool> AddRequestStatusAsync(LeaveStatusDto leaveStatusDto);
    Task<bool?> EditRequestStatusAsync(LeaveStatusDto leaveStatusDto);
    Task<bool?> RemoveRequestStatusAsync(string statusId);
}

public class LeaveService : ILeaveServive
{
    private readonly ILogger<LeaveService> _logger;
    private readonly ILeaveRepository _leaveRepository;
    private readonly ILeaveDashboardCardRepository _leaveDashboardCardRepository;
    private readonly IRequestReasonRepository _requestReasonRepository;
    private readonly IMapper _mapper;


    public LeaveService(ILeaveRepository leaveRepository, 
        ILogger<LeaveService> logger, 
        IMapper mapper, 
        ILeaveDashboardCardRepository leaveDashboardCardRepository, 
        IRequestReasonRepository requestReasonRepository)
    {
        _leaveRepository = leaveRepository;
        _logger = logger;
        _mapper = mapper;
        _leaveDashboardCardRepository = leaveDashboardCardRepository;
        _requestReasonRepository = requestReasonRepository;
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

    public async Task<List<LeaveDto>> SearchLeaveDtosAsync(SearchLeaveDto searchLeaveDto)
    {
        try
        {
            return await _leaveRepository.SearchLeaveDtosAsync(searchLeaveDto);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<List<LeaveDashboardCardDto>> GetDashboardCardsAsync()
    {
        try
        {
            var data = await _leaveDashboardCardRepository.GetAllAsync();
            return _mapper.Map<List<LeaveDashboardCardDto>>(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool> AddDashboardCardsAsync(LeaveDashboardCreateDto leaveDashboardCreateDto)
    {
        try
        {
            var info = _mapper.Map<DashboardCard>(leaveDashboardCreateDto);
            return await _leaveDashboardCardRepository.AddAsync(info);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    public async Task<bool?> EditDashboardCardsAsync(LeaveDashboardCreateDto leaveDashboardCreateDto)
    {
        try
        {
            var cardInfo = await _leaveDashboardCardRepository.GetByCardIdAsync(leaveDashboardCreateDto.CardId);
            if (cardInfo == null)
            {
                return null;
            }
            var cardUpdate = _mapper.Map<DashboardCard>(leaveDashboardCreateDto);
            var result = await _leaveDashboardCardRepository.UpdateAsync(cardUpdate);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool?> RemoveDashboardCardsAsync(string cardId)
    {
        try
        {
            return await _leaveDashboardCardRepository.DeleteByCardId(cardId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<List<LeaveReasonDto>> GetRequestReasonAsync()
    {
        try
        {
            var data = await _requestReasonRepository.GetAllAsync();
            return _mapper.Map<List<LeaveReasonDto>>(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool> AddRequestReasonAsync(LeaveReasonDto leaveReasonDto)
    {
        try
        {
            var info = _mapper.Map<RequestReason>(leaveReasonDto);
            return await _requestReasonRepository.AddAsync(info);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool?> EditRequestReasonAsync(LeaveReasonDto leaveReasonDto)
    {
        try
        {
            var reasonInfo = await _requestReasonRepository.GetByReasonIdAsync(leaveReasonDto.ReasonId);
            if (reasonInfo == null)
            {
                return null;
            }
            var reasonUpdate = _mapper.Map<RequestReason>(leaveReasonDto);
            var result = await _requestReasonRepository.UpdateAsync(reasonUpdate);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    public async Task<bool?> RemoveRequestReasonAsync(string reasonId)
    {
        try
        {
            return await _requestReasonRepository.DeleteByReasonId(reasonId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public Task<List<LeaveStatusDto>> GetRequestStatusAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddRequestStatusAsync(LeaveStatusDto leaveStatusDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool?> EditRequestStatusAsync(LeaveStatusDto leaveStatusDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool?> RemoveRequestStatusAsync(string statusId)
    {
        throw new NotImplementedException();
    }
}
