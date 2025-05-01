using Demo.BusinessLogic.Profiles;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Repositories.Classes;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Demo.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var builder = WebApplication.CreateBuilder(args);

            //#region Add services to the container
            //builder.Services.AddControllersWithViews(Options =>
            //Options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));


            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            //    options.UseLazyLoadingProxies();
            //});



            ////builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); // الطوة التانية بعد الانجكت الخاص DepartmentService
            //#endregion
            //builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            ////builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            //// Add Auto Mapper
            //builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            //// add employeeservice in controller
            //builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            //// Inject _unitOfWork
            //builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //#region  Configure the HTTP request pipelineMyRegion

            //var app = builder.Build();

            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //app.UseHttpsRedirection();

            //app.UseStaticFiles();

            //app.UseRouting();

            ////app.UseAuthorization();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");
            //#endregion

            //app.Run();

        }
    }
}
