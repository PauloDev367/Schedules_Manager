using Application.Auth.Ports;
using Application.User.Ports;
using DataEF.Repositories;
using Domain.Ports;
using IdentityAuth;
using IdentityAuth.Jwt;
using Services.User;

namespace API.Extensions;

public static class AppServiceConfigurationExtension
{
    public static void ConfigureDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<IAuthUserService, IdentityAuthService>();
        builder.Services.AddScoped<JwtGenerator>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<IUserService, UserService>();
    }
}