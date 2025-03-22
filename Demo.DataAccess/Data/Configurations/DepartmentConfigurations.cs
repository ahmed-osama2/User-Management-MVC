


namespace Demo.DataAccess.Data.Configurations
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(propertyExpression: D => D.Id).UseIdentityColumn(seed: 10, increment: 10);
            builder.Property(propertyExpression: D => D.Name).HasColumnType(typeName: "varchar(20)");
            builder.Property(propertyExpression: D => D.Code).HasColumnType(typeName: "varchar(20)");
            builder.Property(propertyExpression: D => D.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(propertyExpression: D => D.LastModifiedOn).HasComputedColumnSql("GETDATE()");


        }
    }
}
