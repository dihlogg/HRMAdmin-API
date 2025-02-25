using AdminHRM.Dtos;
using AdminHRM.Dtos.Leaves;
using AdminHRM.Entities;
using AdminHRM.Server.Dtos;
using AdminHRM.Server.Entities;
using AutoMapper;
using System.Linq.Dynamic.Core;

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
            CreateMap<EmployeeDto, PagedResult>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));

            CreateMap<LeaveCreateDto, Leave>().ReverseMap()
               .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<Leave, LeaveDto>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<LeaveDto, Leave>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));

            CreateMap<LeaveDashboardCreateDto, DashboardCard>().ReverseMap()
               .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<DashboardCard, LeaveDashboardCardDto>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<LeaveDashboardCardDto, DashboardCard>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));

            CreateMap<LeaveReasonCreateDto, RequestReason>().ReverseMap()
              .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<RequestReason, LeaveReasonDto>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<LeaveReasonDto, RequestReason>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));

            CreateMap<RequestStatus, LeaveStatusDto>().ReverseMap()
               .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
            CreateMap<LeaveStatusDto, RequestStatus>().ReverseMap()
                .ForAllMembers(opt => opt.Condition((src, dst, srcMember) => srcMember != null));
        }
    }
}
