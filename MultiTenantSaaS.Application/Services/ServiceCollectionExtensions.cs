using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantSaaS.Application.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            // Register MediatR handlers, validators, etc. from this assembly
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Register all validators from this assembly
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Register JWT Service
            services.AddSingleton<IJwtService>(provider => new JwtService(
                configuration["JwtSettings:Key"] ?? throw new InvalidOperationException("JWT Key not configured"),
                configuration["JwtSettings:Issuer"] ?? throw new InvalidOperationException("JWT Issuer not configured"),
                configuration["JwtSettings:Audience"] ?? throw new InvalidOperationException("JWT Audience not configured"),
                int.Parse(configuration["JwtSettings:ExpiryInMinutes"] ?? "60")
            ));

            return services;
        }
    }
}
