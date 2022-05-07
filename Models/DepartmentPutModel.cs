using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class DepartmentPutModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string DepartmentName { get; set; } = null!;
    }
}
