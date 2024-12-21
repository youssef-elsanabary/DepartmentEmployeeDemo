using DepartmentEmployeeDemo.Models;

namespace DepartmentEmployeeDemo.Services.Interface
{
    public interface IEmployeeService
    {
        public Task<List<Employee>> getAllEmployees();
        public Task<Employee> getEmployeeById(int id);
        public Task<Employee> getEmployeeByName(string name);
        public Task AddEmployee(Employee employee);
        public Task UpdateEmployee(Employee employee);
        public Task DeleteEmployee(Employee employee);
    }
}
