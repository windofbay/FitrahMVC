using FitrahDataAccess;
using FitrahWeb.Configurations;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FitrahWeb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        IServiceCollection services = builder.Services;
        IConfiguration configuration = builder.Configuration;
        services.AddControllersWithViews();
        services.AddBusinessService();
        Dependencies.ConfigureService(builder.Configuration, builder.Services);
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options => {
            options.Cookie.Name = "AuthCookie";
            options.LoginPath = "/Login";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
            options.AccessDeniedPath = "/AccessDenied";
        });
        services.AddAuthorization();

        var app = builder.Build();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern : "{controller=Auth}/{action=Index}"
        );
        app.UseStaticFiles();
        app.Run();
    }
}
