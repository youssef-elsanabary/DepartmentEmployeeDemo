using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DepartmentEmployeeDemo.Models
{
    public class Employee
    {
        
        public int Id { get; set; }
        
        [Required,Length(3,30)]
        public string? Name { get; set; }
        
        [Required,MinLength(6)]
        public string? Password { get; set; }
        [Required]
        public Role Role { get; set; }
        
        [ForeignKey(nameof(Department))]
        public int DeptId { get; set; }
       
        public virtual Department? Department { get; set; }
    }
}
