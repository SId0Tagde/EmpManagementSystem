using EmployeeManagementSystem.Data.Entities;

namespace EmployeeManagementSystem.Data
{
    public interface IDepartmentRepository
    {
        //General
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        //Employee
        Task<Employee[]> GetAllEmployees();
        Task<Employee> getemployee(int empId);

        //Department
        Task<int[]> getDeptIDArrayAsync(string department);
        Task<bool> exists(int departmentId);
        Task<Department[]> getAllDepartments(bool includeEmployee);
        Task<Department> getDepartmentwithEmployee(int departmentId);
        Task<Department> Getdepartment(int departmentId);
    }
}
