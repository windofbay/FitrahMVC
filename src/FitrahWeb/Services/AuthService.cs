using System.Security.Claims;
using FitrahBusiness.Interfaces;
using FitrahWeb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FitrahWeb;

public class AuthService
{
    private readonly IAccountRepository _repository;
    private readonly IConfiguration _configuration;

    public AuthService(IAccountRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }
    private async Task<ClaimsPrincipal> GetPrincipal(AuthLoginViewModel viewModel)
    {
        var claims = new List<Claim>(){
            new Claim("username",viewModel.Username),
        };
        ClaimsIdentity identity = 
            new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        return new ClaimsPrincipal(identity);
    }
    private AuthenticationTicket GetTicket(ClaimsPrincipal claimsPrincipal)
    {   
        AuthenticationProperties authenticationProperties = new AuthenticationProperties(){
            IssuedUtc = DateTime.Now,
            ExpiresUtc = DateTime.Now.AddMinutes(30),
            AllowRefresh = false
        };
        AuthenticationTicket authenticationTicket = new AuthenticationTicket(
            claimsPrincipal, authenticationProperties, CookieAuthenticationDefaults.AuthenticationScheme
        );
        return authenticationTicket;
    }

    public async Task<AuthenticationTicket> SetLogin(AuthLoginViewModel viewModel)
    {
        var model = _repository.Get(viewModel.Username);
        bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(viewModel.Password,model.Password);
        if(!isCorrectPassword) throw new Exception("username atau password salah");

        viewModel = new AuthLoginViewModel(){
            Username = model.Username,
            Password = viewModel.Password, //dari input
        };
        ClaimsPrincipal principal = await GetPrincipal(viewModel);
        AuthenticationTicket ticket = GetTicket(principal);
        return ticket;
    }
}
