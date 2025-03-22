using Microsoft.Extensions.Configuration;
using MultiTenantSaaS.Application.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace MultiTenantSaaS.Infrastructure.ExternalServices;

public class SendGridEmailService : IEmailService
{
    private readonly ISendGridClient _sendGridClient;
    private readonly string _fromEmail;
    private readonly string _fromName;

    public SendGridEmailService(IConfiguration configuration)
    {
        var apiKey = configuration["SendGrid:ApiKey"] 
            ?? throw new ArgumentNullException("SendGrid:ApiKey", "SendGrid API key is not configured");
        _sendGridClient = new SendGridClient(apiKey);
        _fromEmail = configuration["SendGrid:FromEmail"] 
            ?? throw new ArgumentNullException("SendGrid:FromEmail", "SendGrid From Email is not configured");
        _fromName = configuration["SendGrid:FromName"] 
            ?? throw new ArgumentNullException("SendGrid:FromName", "SendGrid From Name is not configured");
    }

    public async Task SendEmailAsync(string to, string subject, string plainTextContent, string htmlContent)
    {
        var msg = new SendGridMessage
        {
            From = new EmailAddress(_fromEmail, _fromName),
            Subject = subject,
            PlainTextContent = plainTextContent,
            HtmlContent = htmlContent
        };
        msg.AddTo(new EmailAddress(to));

        var response = await _sendGridClient.SendEmailAsync(msg);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to send email. Status code: {response.StatusCode}");
        }
    }

    public async Task SendWelcomeEmailAsync(string to, string userName, string tenantName)
    {
        var subject = $"Welcome to {tenantName}!";
        var plainTextContent = $"Hi {userName},\n\nWelcome to {tenantName}. We're excited to have you on board!\n\nBest regards,\nThe Team";
        var htmlContent = $@"
            <h2>Welcome to {tenantName}!</h2>
            <p>Hi {userName},</p>
            <p>Welcome to {tenantName}. We're excited to have you on board!</p>
            <p>Best regards,<br>The Team</p>";

        await SendEmailAsync(to, subject, plainTextContent, htmlContent);
    }

    public async Task SendPasswordResetEmailAsync(string to, string resetLink)
    {
        var subject = "Password Reset Request";
        var plainTextContent = $"Please click the following link to reset your password: {resetLink}";
        var htmlContent = $@"
            <h2>Password Reset Request</h2>
            <p>Please click the following link to reset your password:</p>
            <p><a href='{resetLink}'>Reset Password</a></p>
            <p>If you didn't request this, please ignore this email.</p>";

        await SendEmailAsync(to, subject, plainTextContent, htmlContent);
    }
}