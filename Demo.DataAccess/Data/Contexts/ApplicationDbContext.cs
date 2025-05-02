

using System.Reflection;
using Demo.DataAccess.models.DepartmentModel;
using Demo.DataAccess.models.IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Demo.DataAccess.Data.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employees { get; set; }

        //public DbSet<IdentityUser> Users { get; set; }  
        //public DbSet<IdentityRole> Roles { get; set; } 

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(connectionString: "ConnectionString");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());
            //modelBuilder. ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly); للعموم بالطريقة القديمة 
            //modelBuilder. ApplyConfiguration<Department>(new DepartmentConfigurations());  بعمل باسم  كونفجرشن بتاعي

            base.OnModelCreating( modelBuilder);
        }
    }
}
 