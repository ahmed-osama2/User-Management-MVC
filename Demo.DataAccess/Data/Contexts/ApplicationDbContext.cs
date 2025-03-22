

using System.Reflection;

namespace Demo.DataAccess.Data.Contexts
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext>options) : base(options)
        {

        }
        public DbSet<Department> Department { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: "ConnectionString");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly: Assembly.GetExecutingAssembly());
           //modelBuilder. ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly); للعموم بالطريقة القديمة 
           //modelBuilder. ApplyConfiguration<Department>(new DepartmentConfigurations());  بعمل باسم  كونفجرشن بتاعي
        }
    }
}
 