using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantSaaS.Application.Services;
using MultiTenantSaaS.Domain.Interfaces;
using MultiTenantSaaS.Infrastructure.Data;
using MultiTenantSaaS.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSaaS.Infrastructure.ExternalServices
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure EF Core
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MultiTenantDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Register EF-based repository
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Register Email Service
            services.AddScoped<IEmailService, SendGridEmailService>();

            return services;
        }
    }

}
