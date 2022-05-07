using EmployeeManagementSystem.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class EmployeeModel
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Surname { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public string Qualification { get; set; } = null!;

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = null!;

    }
}
