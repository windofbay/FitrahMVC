using FitrahWeb.Services;
using Highsoft.Web.Mvc.Charts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Web;

namespace FitrahWeb.Controllers;
[Authorize]
public class DashboardController : Controller
{
    private readonly HistoryService _historyService;
    private readonly RecapService _recapService;

    public DashboardController(HistoryService historyService, RecapService recapService)
    {
        _historyService = historyService;
        _recapService = recapService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var counts = await _historyService.GetCountHistoryByAddressViewModel();
        ViewBag.CountsData = JsonConvert.SerializeObject(counts);

        var quantities = await _recapService.GetQuantityByDate("2024");
        ViewBag.QuantityData = JsonConvert.SerializeObject(quantities);
        
        var viewModel = await _recapService.GetOverallTotal("2024");
        var countAll = await _historyService.CountAll("2024");
        viewModel.CountAllHistories = countAll;

        return View(viewModel);
    }
    [HttpGet("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login","Auth");
    }
}
