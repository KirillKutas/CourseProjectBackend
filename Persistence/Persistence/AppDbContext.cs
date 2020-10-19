using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Domain.Common;
using Application.Common.Interfaces;
using Persistence.Configurations;

namespace Persistence
{
    public class AppDbContext : DbContext, IDbContext
    {
        private readonly ITokenService _tokenService;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        
        /// <summary>
        /// https://github.com/aspnet/EntityFrameworkCore/issues/7533
        /// </summary>
        protected AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            ITokenService tokenService)
            : base(options)
        {
            _tokenService = tokenService;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetModifyUserAndDate();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            SetModifyUserAndDate();
            return base.SaveChanges();
        }

        private void SetModifyUserAndDate()
        {
            ChangeTracker.DetectChanges();

            var modifiedUser = _tokenService.GetUserName();
            var modifiedDate = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = modifiedUser;
                    entry.Entity.Created = modifiedDate;

                    entry.Entity.LastModifiedBy = modifiedUser; 
                    entry.Entity.LastModified = modifiedDate;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedBy = modifiedUser;
                    entry.Entity.LastModified = modifiedDate;

                    entry.Property(x => x.CreatedBy).IsModified = false;
                    entry.Property(x => x.Created).IsModified = false;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly)
                .ApplyAuditableEntityConfiguration();

            SetUtcDateTime(modelBuilder);
            RestrictCascadeDelete(modelBuilder);
        }

        private void RestrictCascadeDelete(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .Where(e => !e.IsOwned()).SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        private void SetUtcDateTime(ModelBuilder modelBuilder)
        {
            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                        property.SetValueConverter(dateTimeConverter);
                }
            }
        }
    }
}
