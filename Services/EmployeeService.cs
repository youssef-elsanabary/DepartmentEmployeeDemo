using DepartmentEmployeeDemo.Context;
using DepartmentEmployeeDemo.Models;
using DepartmentEmployeeDemo.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace DepartmentEmployeeDemo.Services
{
    public class EmployeeService : IEmployeeService
    {
        private EmployeeDepartmentDB db;
        public EmployeeService(EmployeeDepartmentDB employeeDepartmentDB ) {
            db = employeeDepartmentDB;
        }
        public async Task AddEmployee(Employee employee)
        {
            await db.Employees.AddAsync( employee );
            db.SaveChanges();
        }

        public async Task DeleteEmployee(Employee employee)
        {
            db.Employees.Remove( employee );
            db.SaveChanges();
        }

        public async Task<List<Employee>> getAllEmployees()
        {
            return await db.Employees.Include(d=>d.Department).ToListAsync();
        }

        public async Task<Employee> getEmployeeById(int id)
        {
            return await db.Employees.Include(d=>d.Department).SingleOrDefaultAsync(e=>e.Id == id);
        }

        public async Task<Employee> getEmployeeByName(string name)
        {
           return await db.Employees.Include(d=> d.Department).SingleOrDefaultAsync(e=> e.Name == name);
        }

        public async Task UpdateEmployee(Employee employee)
        {
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
