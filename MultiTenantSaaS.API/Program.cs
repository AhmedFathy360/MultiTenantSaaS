
using MultiTenantSaaS.Application.Services;
using MultiTenantSaaS.Infrastructure.ExternalServices;

namespace MultiTenantSaaS.API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Register the Application layer
            builder.Services.AddApplicationLayer();

            // Register Infrastructure layer (we’ll do that next, once we implement the repository)
            builder.Services.AddInfrastructureLayer(builder.Configuration);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
