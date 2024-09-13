namespace FitrahWeb.ViewModels;

public class HistoryViewModel
{
        public string Code { get; set; } = null!;
        public string MuzakkiName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Date { get; set; }
        public int? Quantity { get; set; } 
        public string? FitrahMoney { get; set; } 
        public decimal? FitrahRice { get; set; }
        public string? InfaqMoney { get; set; }
        public decimal? InfaqRice { get; set; }
        public string? FidiyaMoney { get; set; }
        public decimal? FidiyaRice { get; set; }
        public string? MaalMoney { get; set; }
        public decimal? MaalRice { get; set; }
        public string AmilUsername { get; set; } = null!;
}