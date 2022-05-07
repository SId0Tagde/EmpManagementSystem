
using AutoMapper;
using EmployeeManagementSystem.Data.Entities;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Data
{
    public class DepartmentProfile : Profile
    {

        public DepartmentProfile()
        {
            CreateMap<DepartmentModel,Department>()
                .ReverseMap();

            CreateMap<EmployeeModel, Employee>()
                .ReverseMap();

            CreateMap<DepartmentPutModel, Department>()
                .ReverseMap();

 
        }
    }
}