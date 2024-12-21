using DepartmentEmployeeDemo.Context;
using DepartmentEmployeeDemo.Services;
using DepartmentEmployeeDemo.Services.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace DepartmentEmployeeDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(a =>
            {
                a.LogoutPath = "/account/login";
            });
            //Inject Services
            builder.Services.AddTransient<IEmployeeService , EmployeeService>();
            builder.Services.AddTransient<IDepartmentService , DepartmentService>();

            //Add Connection String
            builder.Services.AddDbContext<EmployeeDepartmentDB>(
                option => option.UseSqlServer(builder.Configuration.GetConnectionString("ConSring"))
                );


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
