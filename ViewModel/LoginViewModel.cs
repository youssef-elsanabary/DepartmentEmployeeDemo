using System.ComponentModel.DataAnnotations;

namespace DepartmentEmployeeDemo.ViewModel
{
    public class LoginViewModel
    {
        [Required,Length(3,30)]
        public string? UserName { get; set; }

        [Required,MinLength(6)]
        public string? Password { get; set; }
    }
}
