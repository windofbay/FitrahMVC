using FitrahWeb.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace FitrahWeb;

public class AuthController : Controller
{
    private readonly AuthService _service;

    public AuthController(AuthService service)
    {
        _service = service;
    }
    public IActionResult Index()
    {
        return RedirectToAction("Login");
    }
    [HttpGet("Login")]
    public IActionResult Login()
    {
        if(User?.Identity?.IsAuthenticated??false)
            return RedirectToAction("Index","Dashboard");
        return View();
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login(AuthLoginViewModel viewModel)
    {
        if(ModelState.IsValid)
        {
            try
            {
                var ticket = await _service.SetLogin(viewModel); 
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ticket.Principal,
                    ticket.Properties
                );
                return RedirectToAction("Index","Dashboard");
            }
            catch (System.Exception exception)
            {
                ViewBag.Message = exception.Message;
            }
        }
        return View();
    }
}
