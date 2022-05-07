using EmployeeManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<Department> departments { get; set; } = null! ;
        public DbSet<Employee> employees { get; set; } = null! ;


    }
}
