using Hangfire.Dashboard;

namespace MultiTenantSaaS.API.Middleware;

public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();
        
        // Only allow authenticated users who are system administrators
        // TODO: Add proper admin role check
        return httpContext.User.Identity?.IsAuthenticated ?? false;
    }
}