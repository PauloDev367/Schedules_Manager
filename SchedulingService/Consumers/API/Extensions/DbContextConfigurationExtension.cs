using DataEF;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class DbContextConfigurationExtension
{
    public static void ConfigureDbContext(this WebApplicationBuilder builder)
    {
        var connString = builder.Configuration.GetConnectionString("SqlServer");
        builder.Services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlServer(connString)
        );
    }
}