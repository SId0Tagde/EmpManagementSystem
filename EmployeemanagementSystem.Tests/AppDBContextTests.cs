using EmployeeManagementSystem.Data.Entities;

namespace EmployeemanagementSystem.Tests
{
    public class AppDBContextTests : TestWithSqllite
    {
        [Fact]
        public async Task DatabaseIsAvailableAndCanBeConnecctedTo()
        {
            Assert.True(await context.Database.CanConnectAsync());
        }




        //[Fact]
        //public void GetEmployee()
        //{
        //    //Arrange

        //    var factory = new ConnectionFactory();

        //    //Get the instance of AppDbContext
        //    var context = factory.CreateContextForSQLLite();

        //    var department  = new Department()
        //    {
        //        Id = 34,
        //        DepartmentName = "Vishesajnya"
        //    };

        //    var emp = new Employee()
        //    {
        //        Id = 23,
        //        Name = "Dhanajay",
        //        Surname = "Veer",
        //        Address = "Behind Sahiba Darbaar,Tevha tashi",
        //        Department = department,
        //        PhoneNumber = "4567345645",
        //        Qualification = "Tanjnya",
        //    };



        ////Act
        //    context.employees.Add(emp);
        //    context.SaveChanges();

        //    //Assert
        //    var employee = context.employees
        //                            .Where(emp => emp.Id == 23)
        //                            .FirstOrDefault();
        //    if(employee != null)
        //    {
        //        Assert.Equal(23, employee.Id);
        //    }

        //}
    }
}