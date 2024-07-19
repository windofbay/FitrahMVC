using Microsoft.AspNetCore.Mvc.Rendering;

namespace FitrahWeb.ViewModels;

public class RecapIndexViewModel
{
    public string? Year { get; set; }
    public List<SelectListItem> Years { get; set; } = new List<SelectListItem>();
    public List<RecapViewModel> Recaps { get; set; }
    public OverallRecapViewModel  OverallRecap { get; set; }
}
