using FitrahDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace FitrahDataAccess;

public static class Dependencies
{
    public static void ConfigureService(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<FitrahContext>(
            option=>option.UseSqlServer(configuration.GetConnectionString("FitrahConnection"))
        );
    }
}
