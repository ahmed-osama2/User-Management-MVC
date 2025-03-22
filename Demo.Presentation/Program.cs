using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Demo.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container
            builder.Services.AddControllersWithViews();

            //builder.Services.AddScoped<ApplicationDbContext>(); // 2. Register To Service In DI Container ** 

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString: builder.Configuration.GetConnectionString(name: "DefaultConnection"));

                #region طريقة اخري
                //options.UseSqlServer(connectionString: builder.Configuration[key: "ConnectionStrings: DefaultConnection"]);
                //options.UseSqlServer(connectionString: builder.Configuration.GetSection(key: "ConnectionStrings")[key: "DefaultConnection"]); 
                #endregion
            });

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); // الطوة التانية بعد الانجكت الخاص DepartmentService
            #endregion



            #region  Configure the HTTP request pipelineMyRegion

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            #endregion

            app.Run();
        }
    }
}
