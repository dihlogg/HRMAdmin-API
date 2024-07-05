using AdminHRM.Server.Dtos;
using AdminHRM.Server.Entities;
using AutoMapper;
using System.Dynamic;

namespace AdminHRM.Server
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<SubUnitCreateDto, SubUnit>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<SubUnit, SubUnitDto >().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<SubUnitDto, SubUnit>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));

            CreateMap<EmployeeCreateDto, Employee>().ReverseMap()
               .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<Employee, EmployeeDto>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<EmployeeDto, Employee>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
        }
    }
}
