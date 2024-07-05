using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdminHRM.Server.Dtos;

namespace AdminHRM.Server.Services;

public interface ISubUnitService
{
    Task<List<SubUnitDto>> GetSubUnitDtosAsync();
    Task<bool> AddSubUnitAsync(SubUnitCreateDto subUnitCreateDto);
    Task<bool?> EditSubUnitAsync(SubUnitDto subUnitDto);
    Task<bool?> RemoveSubUnitDtosAsync(Guid id);
}