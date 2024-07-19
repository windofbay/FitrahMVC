using FitrahBusiness.Interfaces;
using FitrahBusiness.Repositories;
using FitrahWeb.Services;

namespace FitrahWeb.Configurations;

public static class ConfigureBusinessService
{
    public static IServiceCollection AddBusinessService(this IServiceCollection services)
    {
        services.AddScoped<IHistoryRepository,HistoryRepository>();
        services.AddScoped<IAccountRepository,AccountRepository>();
        services.AddScoped<HistoryService>();
        services.AddScoped<RecapService>();
        services.AddScoped<IRecapRepository,RecapRepository>();
        services.AddScoped<AuthService>();

        return services;
    }
}
