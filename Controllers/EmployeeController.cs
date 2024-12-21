using DepartmentEmployeeDemo.Context;
using DepartmentEmployeeDemo.Models;
using DepartmentEmployeeDemo.Services.Interface;
using DepartmentEmployeeDemo.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DepartmentEmployeeDemo.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeDepartmentDB db;
        public readonly IEmployeeService employeeService;
        public EmployeeController(IEmployeeService _employeeService ,EmployeeDepartmentDB employeeDepartmentDB) {
            employeeService = _employeeService;
            db = employeeDepartmentDB;
        }

        //List All Employee
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await employeeService.getAllEmployees();
            return View(employees);
        }

        //Get Employee By Id
        public async Task<IActionResult> Detail(int id) {
            Employee employee = await employeeService.getEmployeeById(id);
            if(employee == null)
            {
                return NotFound();
            }
            else
            {
                return View("details",employee);
            }
        }

        //Add Employee
        public IActionResult Add() {
            List<Department> departments = db.Department.ToList();
            SelectList deptSelectList = new SelectList(departments,"id","name");
            ViewBag.departments = deptSelectList;
            return View();
        }
        [HttpPost]
        public IActionResult Add(Employee employee) {
            if (ModelState.IsValid)
            {
                employeeService.AddEmployee(employee);
                return RedirectToAction("index");
            }
            else {
                return BadRequest("invalid data!");
            }
        }

        //Delete Employee
        
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id) {
            Employee employee = await employeeService.getEmployeeById(id);
            if (employee == null) {
                return NotFound();
            }
            else
            {
                await employeeService.DeleteEmployee(employee);
                return RedirectToAction("index");
            }
        
        }

        //Update Employee
        
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(int id) {
            List<Department> departments = db.Department.ToList();
            SelectList deptSelectList = new SelectList(departments, "id", "name");
            ViewBag.departments = deptSelectList;
            Employee employee = await employeeService.getEmployeeById(id);
            if (employee == null) {
                return NotFound();
            }
            return View(employee);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Update(Employee employee)
        {
            if (ModelState.IsValid) {
               await employeeService.UpdateEmployee(employee);
                return RedirectToAction("index");
            }
            else
            {
                return BadRequest();
            }

        }


    }
}
