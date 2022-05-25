using EmployeeManagementSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeemanagementSystem.Tests
{
    public class EmployeeTests : TestWithSqllite
    {
        [Fact]
        public void TableShouldGetCreated()
        {
            Assert.False(context.employees.Any());
        }

        [Fact]
        public void GetEmployee()
        {
            //Arrange

            var department = new Department()
            {
                Id = 34,
                DepartmentName = "Adyaay"
            };

            var employee = new Employee()
            {
                Id = 23,
                Name = "Dhanjay",
                Surname="Veer",
                Address="Behind Sahiba Chowk,Guru Darbaar",
                Department= department,
                PhoneNumber = "3453452314",
                Qualification = "tanjnya"
            };

            //Act
            context.departments.Add(department);
            context.SaveChanges();
            context.employees.Add(employee);
            context.SaveChanges();

            //Assert
            var emp = context.employees
                .Where(emp => emp.Id == 23)
                .FirstOrDefault();

            Assert.True(emp.Id == 23);
        }

    }
}
