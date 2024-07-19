using FitrahWeb.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FitrahWeb;

public class HistoryIndexViewModel
{
    public PaginationViewModel Pagination { get; set; }
    public List<HistoryViewModel> Histories { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Year { get; set; }
    public List<SelectListItem> Years { get; set; } = new List<SelectListItem>();
}
