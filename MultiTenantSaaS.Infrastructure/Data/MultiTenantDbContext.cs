using Microsoft.EntityFrameworkCore;
using MultiTenantSaaS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSaaS.Infrastructure.Data
{
    public class MultiTenantDbContext : DbContext
    {
        public MultiTenantDbContext(DbContextOptions<MultiTenantDbContext> options)
            : base(options)
        {
            
        }

        // Add your entities here
        public DbSet<Tenant> Tenants { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add your entity configurations here

            // Optional: Fluent API configuration for the Tenant entity
            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.HasKey(t => t.TenantId);
                entity.Property(t => t.Name).IsRequired().HasMaxLength(200);
            });
        }
    }
}
