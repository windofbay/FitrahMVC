using FitrahWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitrahWeb.Controllers;
[Authorize]
public class RecapController : Controller
{
    private readonly RecapService _service;

    public RecapController(RecapService service)
    {
        _service = service;
    }
    [HttpGet("recap")]
    public IActionResult Get(string? period="")
    {
        string currentYear;
        if (period==null){
            currentYear = DateTime.Now.Year.ToString();
            var viewModel = _service.Get(currentYear);
            return View("Index",viewModel);
        } 
        else {
            var viewModel = _service.Get(period);
            return View("Index",viewModel);
        }
    }
}
