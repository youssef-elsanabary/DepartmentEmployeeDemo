using DepartmentEmployeeDemo.Context;
using DepartmentEmployeeDemo.Models;
using DepartmentEmployeeDemo.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace DepartmentEmployeeDemo.Services
{
    public class DepartmentService : IDepartmentService
    {
        EmployeeDepartmentDB db;
        public DepartmentService(EmployeeDepartmentDB employeeDepartmentDB)
        {
            db = employeeDepartmentDB;
        }


        public async Task AddDepartment(Department department)
        {
            await db.Department.AddAsync(department);
            db.SaveChanges();
        }

        public async Task DeleteDepartment(Department department)
        {
            db.Department.Remove(department);
            db.SaveChanges();
        }

        public async Task<List<Department>> getAllDepartments()
        {
            return await db.Department.Include(a=> a.Employees).ToListAsync();
        }

        public async Task<Department> getDepartmentById(int id)
        {
            return await db.Department.Include(a=>a.Employees).FirstOrDefaultAsync(d=> d.id == id);
        }

        public async Task<Department> getDepartmentByName(string name)
        {
            return await db.Department.Include(a => a.Employees).FirstOrDefaultAsync(d => d.name == name);
        }

        public async Task UpdateDepartment(Department dept)
        {
            db.Entry(dept).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
