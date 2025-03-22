# MultiTenant SaaS Application

## Configuration Guide

### Security Keys and Secrets

#### JWT Configuration
The JWT secret key is required for token generation and validation. In development:

1. Generate a strong secret key (at least 32 characters)
2. You can use an online generator or run this PowerShell command:
```powershell
[Convert]::ToBase64String([byte[]](1..32))
```
3. Add it to your configuration:
```json
"JwtSettings": {
    "Key": "YOUR_GENERATED_KEY_HERE",
    "Issuer": "your-issuer",
    "Audience": "your-audience",
    "DurationInMinutes": 60
}
```

⚠️ **IMPORTANT:**
- Never commit the actual JWT key to source control
- In production, use secure configuration management (e.g., Azure Key Vault)
- Rotate keys periodically following security best practices

#### SendGrid Configuration

1. Create a SendGrid Account:
   - Sign up at https://signup.sendgrid.com/
   - Navigate to Settings -> API Keys
   - Create a new API Key with "Mail Send" permissions

2. Email Sender Verification:
   - Go to Settings -> Sender Authentication
   - Either verify your domain (recommended) or verify a single sender email
   - The FromEmail in configuration must be verified in SendGrid

3. Add to configuration:
```json
"SendGrid": {
    "ApiKey": "YOUR_SENDGRID_API_KEY",
    "FromEmail": "your-verified-sender@yourdomain.com",
    "FromName": "Your Application Name"
}
```

⚠️ **IMPORTANT:**
- Never commit the actual SendGrid API key to source control
- Use environment variables or secure configuration management in production
- Keep API keys secure and rotate them periodically
- The FromEmail must be verified in SendGrid before sending emails

### Development Setup

1. Create a `secrets.json` file for development:
   ```bash
   dotnet user-secrets init --project MultiTenantSaaS.API
   ```

2. Add your secrets:
   ```bash
   dotnet user-secrets set "JwtSettings:Key" "your-generated-key"
   dotnet user-secrets set "SendGrid:ApiKey" "your-sendgrid-api-key"
   ```

### Production Deployment

For production environments:
- Use Azure Key Vault or similar service for secret management
- Enable SSL/TLS encryption
- Follow security best practices for key rotation
- Monitor API usage and implement rate limiting
- Set up proper logging and monitoring