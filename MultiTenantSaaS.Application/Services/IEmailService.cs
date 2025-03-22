namespace MultiTenantSaaS.Application.Services;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string plainTextContent, string htmlContent);
    Task SendWelcomeEmailAsync(string to, string userName, string tenantName);
    Task SendPasswordResetEmailAsync(string to, string resetLink);
}