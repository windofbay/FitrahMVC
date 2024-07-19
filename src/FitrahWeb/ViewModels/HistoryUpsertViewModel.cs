using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FitrahWeb.ViewModels;

public class HistoryUpsertViewModel
{
    public string? Code { get; set; } 
    [Required(ErrorMessage ="Nama Muzakki wajib diisi!")]
    public string MuzakkiName { get; set; } = null!;
    [Required(ErrorMessage ="Alamat wajib diisi!")]
    public string Address { get; set; } = null!;
    public int? Quantity { get; set; }
    public decimal? FitrahMoney { get; set; }
    public decimal? FitrahRice { get; set; }
    public decimal? InfaqMoney { get; set; }
    public decimal? InfaqRice { get; set; }
    public decimal? FidiyaMoney { get; set; }
    public decimal? FidiyaRice { get; set; }
    public decimal? MaalMoney { get; set; }
    public decimal? MaalRice { get; set; }
    [Required(ErrorMessage ="Nama Amil wajib diisi!")]
    public string AmilUsername { get; set; } = null!;
    public string? Note { get; set; }
    public List<SelectListItem>? Amils { get; set; }
    public DateTime Date { get; set; }
}
