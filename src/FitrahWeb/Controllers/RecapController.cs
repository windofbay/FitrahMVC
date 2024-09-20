using FitrahWeb.Services;
using FitrahWeb.ViewModels;
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
    public async Task<IActionResult> Get(string? period="")
    {
        string currentYear;
        if (period==null){
            currentYear = DateTime.Now.Year.ToString();
            var viewModel = await _service.Get(currentYear);
            return View("Index",viewModel);
        } 
        else {
            var viewModel = await _service.Get(period);
            return View("Index",viewModel);
        }
    }
    [HttpGet("recap/{date}")]
    public async Task<IActionResult> Get(DateTime date)
    {
        var viewModel = await _service.Get(date);
        return View("Upsert",viewModel);
    }
    [HttpPost("recap/insert")]
    public async Task<IActionResult> Upload(RecapUpsertViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            await _service.Upload(viewModel);
            return RedirectToAction("Get");
        }
        var vm = _service.Get(viewModel.Date);
        return View("Upsert",vm);
    }
}
