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
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add your entity configurations here

            // Optional: Fluent API configuration for the Tenant entity
            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.HasKey(t => t.TenantId);
                entity.Property(t => t.Name).IsRequired().HasMaxLength(200);
                entity.HasMany(t => t.Users)
                      .WithOne(u => u.Tenant)
                      .HasForeignKey(u => u.TenantId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(256);
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Role).IsRequired().HasMaxLength(50);
                
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => new { u.TenantId, u.Email }).IsUnique();
            });
        }
    }
}
