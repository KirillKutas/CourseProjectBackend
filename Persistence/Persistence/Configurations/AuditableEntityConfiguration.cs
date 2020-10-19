using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.Common;

namespace Persistence.Configurations
{
    public static class AuditableEntityConfiguration
    {
        public static ModelBuilder ApplyAuditableEntityConfiguration(this ModelBuilder modelBuilder)
        {
            var auditableEntities = modelBuilder.Model.GetEntityTypes().Where(e => typeof(AuditableEntity).IsAssignableFrom(e.ClrType));
            foreach (var entityType in auditableEntities)
            {
                modelBuilder
                    .Entity(entityType.ClrType)
                    .Property(nameof(AuditableEntity.CreatedBy))
                    .IsRequired();
                modelBuilder
                    .Entity(entityType.ClrType)
                    .Property(nameof(AuditableEntity.LastModifiedBy))
                    .IsRequired();
            }

            return modelBuilder;
        }
    }
}
