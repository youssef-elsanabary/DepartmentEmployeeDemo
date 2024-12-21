using DepartmentEmployeeDemo.Models;

namespace DepartmentEmployeeDemo.Services.Interface
{
    public interface IDepartmentService
    {
        public Task<List<Department>> getAllDepartments();
        public Task<Department> getDepartmentById(int id);
        public Task<Department> getDepartmentByName(string name);
        public Task AddDepartment(Department department);
        public Task UpdateDepartment(Department department);
        public Task DeleteDepartment(Department department);
    }
}
