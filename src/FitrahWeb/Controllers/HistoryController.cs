using FitrahWeb.Services;
using FitrahWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitrahWeb.Controllers;
[Authorize]
public class HistoryController : Controller
{
    private readonly HistoryService _service;

    public HistoryController(HistoryService service)
    {
        _service = service;
    }
    [HttpGet("history")]
    public ActionResult Get(int page=1, int pageSize=7,string? name="",string? address="", string? period="")
    {
        string currentYear;
        if (name==null && address==null && period==null){
            currentYear = DateTime.Now.Year.ToString();
            var viewModel = _service.Get(page,pageSize,name,address,currentYear);
            return View("Index",viewModel);
        } 
        else {
            var viewModel = _service.Get(page,pageSize,name,address,period);
            return View("Index",viewModel);
        }
    }
    [HttpGet("history/insert")]
    public IActionResult Insert()
    {
        var viewModel = _service.Get();
        return View("Upsert",viewModel);
    }
    [HttpPost("history/insert")]
    public IActionResult Insert(HistoryUpsertViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            _service.Insert(viewModel);
            return RedirectToAction("Get");
        }
        var vm = _service.Get();
        return View("Upsert",vm);
    }
    [HttpGet("history/{code}/edit")]
    public IActionResult Get(string code)
    {
        var viewModel = _service.Get(code);
        return View("Upsert",viewModel);
    }
    [HttpPost("history/{code}/edit")]
    public IActionResult Edit(HistoryUpsertViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            _service.Update(viewModel);
            return RedirectToAction("Get");
        }
        var vm = _service.Get(viewModel.Code??"");
        return View("Upsert",vm);   
    }

}
