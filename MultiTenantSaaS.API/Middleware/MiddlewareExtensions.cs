namespace MultiTenantSaaS.API.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }

        public static IApplicationBuilder UseTenantAuthorization(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TenantAuthorizationMiddleware>();
        }
    }
}