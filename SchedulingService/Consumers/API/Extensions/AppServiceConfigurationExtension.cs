using Application.Auth.Ports;
using IdentityAuth;
using IdentityAuth.Jwt;

namespace API.Extensions;

public static class AppServiceConfigurationExtension
{
    public static void ConfigureDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IAuthUserService, IdentityAuthService>();
        builder.Services.AddScoped<JwtGenerator>();
    }
}