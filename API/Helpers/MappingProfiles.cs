using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos.EmployeeDtos;
using API.Dtos.MasterDtos;
using AutoMapper;
using Core.Entities.Employees;
using Core.Entities.Masters;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<BranchDto, Branch>();
            CreateMap<DivisionDto, Division>();
            CreateMap<DepartmentDto, Department>();
            CreateMap<DesignationDto, Designation>();
            CreateMap<ShiftDto, Shift>();
            CreateMap<EmployeeRequestDto, Employee>();
            CreateMap<Employee, EmployeeResponseDto>();
            CreateMap<TeamDto, Team>();
            CreateMap<TeamDetailsDto, TeamDetails>();
            CreateMap<TeamResponseDto, Team>();
            CreateMap<TeamDetailsResponseDto, TeamDetails>();
        }


    }
}