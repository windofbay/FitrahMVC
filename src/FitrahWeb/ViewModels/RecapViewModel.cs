namespace FitrahWeb.ViewModels;

public class RecapViewModel
{
    public string Date { get; set; }
    public DateTime PlainDate { get; set; } = new DateTime();
    public int? TotalQuantity { get; set; }
    public string TotalFitrahMoney { get; set; }
    public decimal? TotalFitrahRice { get; set; }
    public string TotalFidiyaMoney { get; set; }
    public decimal? TotalFidiyaRice { get; set; }
    public string TotalInfaqMoney { get; set; }
    public decimal? TotalInfaqRice { get; set; }
    public string TotalMaalMoney { get; set; }
}
