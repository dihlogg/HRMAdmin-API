using AdminHRM.Dtos;
using AdminHRM.Dtos.Leaves;
using AdminHRM.Entities;
using AdminHRM.Infrastructures.Repositories;
using AdminHRM.Server.Dtos;
using AdminHRM.Server.Entities;
using AdminHRM.Server.Infrastructures;
using AdminHRM.Server.Services;
using AdminHRM.Services.Implements;
using AutoMapper;

namespace AdminHRM.Services.Implements;

public interface ILeaveServive
{
    Task<List<LeaveDashboardCardDto>> GetDashboardCardsAsync();
    Task<bool> AddDashboardCardsAsync(LeaveDashboardCreateDto leaveDashboardCreateDto);
    Task<bool?> EditDashboardCardsAsync(LeaveDashboardCreateDto leaveDashboardCreateDto);
    Task<bool?> RemoveDashboardCardsAsync(string cardId);
    Task<List<LeaveReasonDto>> GetRequestReasonAsync();
    Task<bool> AddRequestReasonAsync(LeaveReasonCreateDto leaveReasonCreateDto);
    Task<bool?> EditRequestReasonAsync(LeaveReasonDto leaveReasonDto);
    Task<bool?> RemoveRequestReasonAsync(Guid id);
    Task<List<LeaveStatusDto>> GetRequestStatusAsync();
    Task<bool> AddRequestStatusAsync(LeaveStatusCreateDto leaveStatusCreaeteDto);
    Task<bool?> EditRequestStatusAsync(LeaveStatusDto leaveStatusDto);
    Task<bool?> RemoveRequestStatusAsync(Guid id);
    Task<List<RequestTypeDto>> GetRequestTypesAsync();
    Task<bool> AddRequestTypeAsync(RequestTypeCreateDto requestTypeCreateDto);
    Task<bool?> EditRequestTypeAsync(RequestTypeDto requestTypeDto);
    Task<bool?> RemoveRequestTypeAsync(Guid id);
}


public class LeaveService : ILeaveServive
{
    private readonly ILogger<LeaveService> _logger;
    private readonly IMapper _mapper;
    private readonly ILeaveDashboardCardRepository _leaveDashboardCardRepository;
    private readonly IRequestReasonRepository _requestReasonRepository;
    private readonly IRequestStatusRepository _requestStatusRepository;
    private readonly IRequestTypeRepository _requestTypeRepository;


    public LeaveService(
        ILogger<LeaveService> logger, 
        IMapper mapper, 
        ILeaveDashboardCardRepository leaveDashboardCardRepository, 
        IRequestReasonRepository requestReasonRepository,
        IRequestStatusRepository requestStatusRepository,
        IRequestTypeRepository requestTypeRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _leaveDashboardCardRepository = leaveDashboardCardRepository;
        _requestReasonRepository = requestReasonRepository;
        _requestStatusRepository = requestStatusRepository;
        _requestTypeRepository = requestTypeRepository;
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

    public async Task<bool> AddRequestReasonAsync(LeaveReasonCreateDto leaveReasonCreateDto)
    {
        try
        {
            var info = _mapper.Map<RequestReason>(leaveReasonCreateDto);
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
            var reasonInfo = await _requestReasonRepository.GetByIdAsync(leaveReasonDto.Id);
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
    public async Task<bool?> RemoveRequestReasonAsync(Guid id)
    {
        try
        {
            return await _requestReasonRepository.DeleteByKey(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<List<LeaveStatusDto>> GetRequestStatusAsync()
    {
        try
        {
            var data = await _requestStatusRepository.GetAllAsync();
            return _mapper.Map<List<LeaveStatusDto>>(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool> AddRequestStatusAsync(LeaveStatusCreateDto leaveStatusCreateDto)
    {
        try
        {
            var info = _mapper.Map<RequestStatus>(leaveStatusCreateDto);
            return await _requestStatusRepository.AddAsync(info);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool?> EditRequestStatusAsync(LeaveStatusDto leaveStatusDto)
    {
        try
        {
            var statusInfo = await _requestStatusRepository.GetByIdAsync(leaveStatusDto.Id);
            if (statusInfo == null)
            {
                return null;
            }
            var statusUpdate = _mapper.Map<RequestStatus>(leaveStatusDto);
            var result = await _requestStatusRepository.UpdateAsync(statusUpdate);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool?> RemoveRequestStatusAsync(Guid id)
    {
        try
        {
            return await _requestStatusRepository.DeleteByKey(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
    public async Task<List<RequestTypeDto>> GetRequestTypesAsync()
    {
        try
        {
            var data = await _requestTypeRepository.GetAllAsync();
            return _mapper.Map<List<RequestTypeDto>>(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool> AddRequestTypeAsync(RequestTypeCreateDto requestTypeCreateDto)
    {
        try
        {
            var info = _mapper.Map<RequestType>(requestTypeCreateDto);
            return await _requestTypeRepository.AddAsync(info);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool?> EditRequestTypeAsync(RequestTypeDto requestTypeDto)
    {
        try
        {
            var typeInfo = await _requestTypeRepository.GetByIdAsync(requestTypeDto.Id);
            if (typeInfo == null)
            {
                return null;
            }
            var typeUpdate = _mapper.Map<RequestType>(requestTypeDto);
            var result = await _requestTypeRepository.UpdateAsync(typeUpdate);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool?> RemoveRequestTypeAsync(Guid id)
    {
        try
        {
            return await _requestTypeRepository.DeleteByKey(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}
