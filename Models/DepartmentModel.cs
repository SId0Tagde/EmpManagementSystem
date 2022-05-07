using EmployeeManagementSystem.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class DepartmentModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string DepartmentName { get; set; } = null!;
        public ICollection<EmployeeModel> Departmentemployees { get; set; } = null!;
    }
}
