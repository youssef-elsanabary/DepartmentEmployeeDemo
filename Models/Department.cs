using System.ComponentModel.DataAnnotations;

namespace DepartmentEmployeeDemo.Models
{
    public class Department
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }
        
        public virtual List<Employee>? Employees { get; set; }
    }
}
