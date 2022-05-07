using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagementSystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using Microsoft.Data.SqlClient;

namespace EmployeeManagementSystem.Data
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDBContext context;
        private readonly ILogger<DepartmentRepository> _logger;

        public DepartmentRepository(AppDBContext dbContext, ILogger<DepartmentRepository> logger)
        {
            context = dbContext;
            _logger = logger;
        }


        //General Method
        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType} to the context");
            context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Removing an object of type {entity.GetType} to the context");
            context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempting to save changes in the context");
            return (await context.SaveChangesAsync()) > 0;
        }

        //Employee Methods
        public async Task<Employee[]> GetAllEmployees()
        {
            var query = context.employees.Include(em => em.Department);
             return await query.ToArrayAsync();
        }

        public async Task<Employee> getemployee(int empId)
        {
            var query = context.employees.Include(em => em.Department);
            return await context.employees.Where(e => e.Id == empId).FirstOrDefaultAsync()!;
        }


        //Department Methods 
        public async Task<Department> getDepartmentwithEmployee(int departmentId)
        {
            var query = context.departments
                                .Include(dt => dt.Departmentemployees);

            return await query.Where(dt => dt.Id == departmentId)
                              .FirstOrDefaultAsync();
        }

        public async Task<Department[]> getAllDepartments(bool includeEmployee)
        {
            if(includeEmployee == true)
            {
             var query = context.
                            departments.
                            Include(dt => dt.Departmentemployees);
                return await query.ToArrayAsync();
            }
            return await context.departments.ToArrayAsync();
        }

        public async Task<Department> Getdepartment(int departmentId)
        {
           var iop = context.departments
                               .Where(dt => dt.Id == departmentId)
                               .FirstOrDefaultAsync();
            return await iop;
        }

        public async Task<bool> exists(int departmentId)
        {
            return await context.departments
                    .Where(dt => dt.Id == departmentId)
                    .AnyAsync();
        }

        public async Task<int[]> getDeptIDArrayAsync(string department)
        {
            return await context.departments
                                .Where(dt => dt.DepartmentName == department)
                                .Select(dt => dt.Id)
                                .ToArrayAsync();
        }
    }
}
