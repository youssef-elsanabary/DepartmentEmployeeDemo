using DepartmentEmployeeDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace DepartmentEmployeeDemo.Context
{
    public class EmployeeDepartmentDB : DbContext
    {
        public EmployeeDepartmentDB(DbContextOptions<EmployeeDepartmentDB> options):base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Department { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasIndex(a => a.Name).IsUnique(true);
            base.OnModelCreating(modelBuilder);
        }
    }
}
