using AutoMapper;
using AdminHRM.Server.Dtos;
using AdminHRM.Server.Entities;
using AdminHRM.Server.Infrastructures;

namespace AdminHRM.Server.Services;

public interface ISubUnitService
{
    Task<List<SubUnitDto>> GetSubUnitDtosAsync();
    Task<bool> AddSubUnitAsync(SubUnitCreateDto subUnitCreateDto);
    Task<bool?> EditSubUnitAsync(SubUnitDto subUnitDto);
    Task<bool?> RemoveSubUnitDtosAsync(Guid id);
}

public class SubUnitService : ISubUnitService
{
    private readonly ILogger<SubUnitService> _logger;
    private readonly ISubUnitRepository _subUnitRepository;
    private readonly IMapper _mapper;

    public SubUnitService(ISubUnitRepository subUnitRepository, ILogger<SubUnitService> logger, IMapper mapper)
    {
        _subUnitRepository = subUnitRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<List<SubUnitDto>> GetSubUnitDtosAsync()
    {
        try
        {
            var data = await _subUnitRepository.GetAllAsync();
            return _mapper.Map<List<SubUnitDto>>(data);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool> AddSubUnitAsync(SubUnitCreateDto subUnitCreateDto)
    {
        try
        {
            var info = _mapper.Map<SubUnit>(subUnitCreateDto);
            return await _subUnitRepository.AddAsync(info);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool?> EditSubUnitAsync(SubUnitDto subUnitDto)
    {
        try
        {
            var userInfo = await _subUnitRepository.GetByIdAsync(subUnitDto.Id);
            if (userInfo == null)
            {
                return null;
            }
            var infoUpdate = _mapper.Map<SubUnit>(subUnitDto);
            var result = await _subUnitRepository.UpdateAsync(infoUpdate);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<bool?> RemoveSubUnitDtosAsync(Guid id)
    {
        try
        {
            return await _subUnitRepository.DeleteByKey(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}