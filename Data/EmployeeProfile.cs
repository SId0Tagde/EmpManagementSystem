using AutoMapper;
using EmployeeManagementSystem.Data.Entities;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Data
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeModel, Employee>()
                .ReverseMap();

            CreateMap<EmpPostModel, Employee>()
                .ForPath(dest => dest.Department.Id,
                 opt => opt.MapFrom(src => src.DepartmentId))
                .ReverseMap()
                .ForPath(dest => dest.DepartmentId,
                         opt => opt.MapFrom(src => src.Department.Id));


        }
    }
}
