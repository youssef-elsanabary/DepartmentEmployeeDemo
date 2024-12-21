using DepartmentEmployeeDemo.Context;
using DepartmentEmployeeDemo.Models;
using DepartmentEmployeeDemo.Services;
using DepartmentEmployeeDemo.Services.Interface;
using DepartmentEmployeeDemo.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace DepartmentEmployeeDemo.Controllers
{
    public class AccountController : Controller
    {
        EmployeeDepartmentDB db;
        public readonly IEmployeeService EmployeeService;
        public AccountController(IEmployeeService employeeService, EmployeeDepartmentDB employeeDepartmentDB) 
        {
            EmployeeService = employeeService;
            db = employeeDepartmentDB;
        }

        //register
        public IActionResult Register() {
             List<Department> departments = db.Department.ToList();
            SelectList deptSelectList = new SelectList(departments,"id","name");
            ViewBag.departments = deptSelectList;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await EmployeeService.AddEmployee(employee);
                LoginViewModel loginViewModel = new LoginViewModel() {UserName = employee.Name , Password = employee.Password };
                await Login(loginViewModel);
                return  RedirectToAction("index","Employee");
            }
            else
            {
                return BadRequest("invalid data!");
            }
        }

        // Login
        public IActionResult Login() { return View(); }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model){ 
            Employee emp = await EmployeeService.getEmployeeByName(model.UserName);
            if (ModelState.IsValid)
            {
                if (emp == null || model.Password != emp.Password)
                {
                    return Unauthorized("Invalid userName or password");
                }
                else
                {
                    Claim c1 = new Claim(ClaimTypes.Name ,model.UserName);
                    Claim c2 = new Claim(ClaimTypes.Role ,$"{emp.Role}");

                    ClaimsIdentity cli = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    cli.AddClaim(c1);
                    cli.AddClaim(c2);

                    ClaimsPrincipal cp = new ClaimsPrincipal(cli);

                    await HttpContext.SignInAsync(cp);
      
                    return RedirectToAction("Index", "Employee");
                    //login complete
                }
            }
            else
            {
                ModelState.AddModelError("", "username or password not correct");
                return View(model);
            }

        }
        
        //Logout

        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
