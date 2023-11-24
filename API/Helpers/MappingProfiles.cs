using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.ActionDtos;
using API.Dtos.EmployeeDtos;
using API.Dtos.EntriesDtos;
using API.Dtos.Identity;
using API.Dtos.MasterDtos;
using AutoMapper;
using Core.Entities.Actions;
using Core.Entities.Employees;
using Core.Entities.Entries;
using Core.Entities.Identity;
using Core.Entities.Masters;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<BranchDto, Branch>();
            CreateMap<Branch, BranchDto>();

            CreateMap<DivisionDto, Division>();
            CreateMap<Division, DivisionDto>();

            CreateMap<DepartmentDto, Department>();
            CreateMap<DepartmentDto, Department>().ReverseMap();
            CreateMap<Department, DepartmentResponseDto>();
            CreateMap<Department, DepartmentResponseDto>().ReverseMap();

            CreateMap<DesignationDto, Designation>();
            CreateMap<Designation, DesignationDto>();

            CreateMap<ShiftDto, Shift>();

            CreateMap<EmployeeRequestDto, Employee>();
            CreateMap<Employee, EmployeeRequestDto>();
            CreateMap<Employee, EmployeeResponseDto>();
            CreateMap<EmployeeCommonDto, Employee>();
            CreateMap<EmployeeCommonDto, Employee>().ReverseMap();

            CreateMap<LeaveRequestDto, Leave>();
            CreateMap<Leave, LeaveResponseDto>();
            CreateMap<LeaveTypeResDto, LeaveType>();

            //CreateMap<Request, MyRequestResponseDto>();
            CreateMap<Request, RequestResponseDto>();
            CreateMap<RequestResponseDto, Request>();
            CreateMap<Request,RequestEntriesResponseDto>();

            CreateMap<Request, RequestDto>();
            CreateMap<RequestTemplate,RequestTemplateDto>();
            CreateMap<Request, RequestDetailResponseDto>();
            CreateMap<IReadOnlyList<ActionHistory>, IReadOnlyList<ActionResponseDtos>>();
            CreateMap<ActionHistory, ActionResponseDtos>();
            

            CreateMap<TeamDto, Team>();
            CreateMap<TeamDetailsDto, TeamDetails>();

            CreateMap<TeamUpdateDto, Team>();
            CreateMap<TeamDetailsUpdateDto, TeamDetails>();

            CreateMap<TeamDto, Team>().ReverseMap();
            CreateMap<TeamDetailsDto, TeamDetails>().ReverseMap();

            CreateMap<TeamResponseDto, Team>();
            CreateMap<TeamDetailsResponseDto, TeamDetails>();

            CreateMap<TeamResponseDto, Team>().ReverseMap();
            CreateMap<TeamResponseGeneralDto, Team>().ReverseMap();
            CreateMap<TeamDetailsResponseDto, TeamDetails>().ReverseMap();

            CreateMap<LeavePolicyDto, LeavePolicy>();
            CreateMap<LeavePolicyDetailDto, LeavePolicyDetails>();

            CreateMap<LeavePolicyResponseDto, LeavePolicy>().ReverseMap();
            CreateMap<LeavePolicyDetailResponseDto, LeavePolicyDetails>().ReverseMap();
            CreateMap<LeaveTypeResponseDto, LeaveType>().ReverseMap();

            CreateMap<AppUserDto, AppUser>()
               .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.EmployeeCode));
        }


    }
}