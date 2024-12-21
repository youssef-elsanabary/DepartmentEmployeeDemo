using DepartmentEmployeeDemo.Models;
using DepartmentEmployeeDemo.Services;
using DepartmentEmployeeDemo.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DepartmentEmployeeDemo.Controllers
{
    public class DepartmentController : Controller
    {   
        public readonly IDepartmentService deptService;
        public DepartmentController(IDepartmentService departmentService) 
        {
            deptService = departmentService;
        }
        public async Task<IActionResult> Index()
        {
            List<Department> departments = await deptService.getAllDepartments();
            return View(departments);
        }


        // Get Department By Id
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DepartmentById(int id)
        {
            Department department = await deptService.getDepartmentById(id);
            if(department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        //Add Department
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Add() 
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Add(Department department)
        {
            if (ModelState.IsValid) 
            { 
                await deptService.AddDepartment(department);
                return RedirectToAction("index");
            }
            else
            {
                return BadRequest();
            }
        }

        //Delete Department
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Department dept = await deptService.getDepartmentById(id);
            if (dept == null)
            {
                return NotFound();
            }
            else
            {
                await deptService.DeleteDepartment(dept);
                return RedirectToAction("index");
            }
        }


        //Update Department
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            Department dept = await deptService.getDepartmentById(id);
            if (dept == null)
            {
                return NotFound();
            }
            return View("Edit",dept); 
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(Department dept)
        {
            if (ModelState.IsValid)
            {
                await deptService.UpdateDepartment(dept);
                return RedirectToAction("index");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
