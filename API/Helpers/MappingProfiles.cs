using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.EmployeeDtos;
using API.Dtos.EntriesDtos;
using API.Dtos.Identity;
using API.Dtos.MasterDtos;
using AutoMapper;
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

            CreateMap<DesignationDto, Designation>();
            CreateMap<Designation, DesignationDto>();

            CreateMap<ShiftDto, Shift>();

            CreateMap<EmployeeRequestDto, Employee>();
            CreateMap<Employee, EmployeeResponseDto>();
            CreateMap<EmployeeCommonDto, Employee>();
            CreateMap<EmployeeCommonDto, Employee>().ReverseMap();

            CreateMap<LeaveRequestDto, Leave>();
            CreateMap<Leave, LeaveResponseDto>();
            CreateMap<LeaveTypeResDto, LeaveType>();

            CreateMap<TeamDto, Team>();
            CreateMap<TeamDetailsDto, TeamDetails>();

            CreateMap<TeamResponseDto, Team>();
            CreateMap<TeamDetailsResponseDto, TeamDetails>();

            CreateMap<TeamResponseDto, Team>().ReverseMap();
            CreateMap<TeamDetailsResponseDto, TeamDetails>().ReverseMap();

            CreateMap<AppUserDto, AppUser>()
               .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.EmployeeCode));
        }


    }
}