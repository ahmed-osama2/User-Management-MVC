using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.DataAccess.models.Shared;

namespace Demo.DataAccess.Data.Configurations
{
    public class BaseEntityConfigurations<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(propertyExpression: D => D.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(propertyExpression: D => D.LastModifiedOn).HasComputedColumnSql("GETDATE()");
        }
    }
}
