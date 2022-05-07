using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Data.Entities
{
    public class Department
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string DepartmentName { get; set; } = null! ;
        public ICollection<Employee> Departmentemployees { get; set; } = null!;

    }
}
