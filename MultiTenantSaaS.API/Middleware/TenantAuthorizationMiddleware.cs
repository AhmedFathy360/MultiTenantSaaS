using System.Security.Claims;

namespace MultiTenantSaaS.API.Middleware
{
    public class TenantAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var endpoint = context.GetEndpoint();
                if (endpoint?.Metadata?.GetMetadata<BypassTenantAuthorizationAttribute>() == null)
                {
                    var tenantId = context.User.Claims.FirstOrDefault(c => c.Type == "tenantId")?.Value;
                    var requestTenantId = GetTenantIdFromRequest(context.Request);

                    if (!string.IsNullOrEmpty(requestTenantId) && tenantId != requestTenantId)
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        return;
                    }
                }
            }

            await _next(context);
        }

        private string? GetTenantIdFromRequest(HttpRequest request)
        {
            // Try to get tenant ID from route
            if (request.RouteValues.TryGetValue("tenantId", out var tenantId))
                return tenantId?.ToString();

            // Try to get from query string
            if (request.Query.TryGetValue("tenantId", out var queryTenantId))
                return queryTenantId.ToString();

            return null;
        }
    }

    // Attribute to bypass tenant authorization for specific endpoints
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BypassTenantAuthorizationAttribute : Attribute { }
}