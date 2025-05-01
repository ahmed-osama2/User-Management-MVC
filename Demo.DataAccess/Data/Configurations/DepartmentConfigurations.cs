


using Demo.DataAccess.models.DepartmentModel;

namespace Demo.DataAccess.Data.Configurations
{
    internal class DepartmentConfigurations : BaseEntityConfigurations<Department> , IEntityTypeConfiguration<Department>
    {
        public new void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(propertyExpression: D => D.Id).UseIdentityColumn(seed: 10, increment: 10);
            builder.Property(propertyExpression: D => D.Name).HasColumnType(typeName: "varchar(20)");
            builder.Property(propertyExpression: D => D.Code).HasColumnType(typeName: "varchar(20)");
            builder.HasMany(D => D.Employees)
                .WithOne(E => E.Department)
                .HasForeignKey(E => E.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
                
            base.Configure(builder);


        }
    }
}
