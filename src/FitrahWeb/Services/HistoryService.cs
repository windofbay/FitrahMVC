using FitrahBusiness;
using FitrahBusiness.Interfaces;
using FitrahDataAccess.Models;
using FitrahWeb.Helper;
using FitrahWeb.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace FitrahWeb.Services;

public class HistoryService
{
  private readonly IHistoryRepository _historyRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IRecapRepository _recapRepository;
    public HistoryService(IHistoryRepository historyRepository, IAccountRepository accountRepository, IRecapRepository recapRepository)
    {
        _historyRepository = historyRepository;
        _accountRepository = accountRepository;
        _recapRepository = recapRepository;
    }

    public HistoryIndexViewModel Get(int page, int pageSize, string? name, string? address, string? year)
    {
        var model = _historyRepository.Get(page,pageSize,name,address,year)
        .Select(history=>new HistoryViewModel(){
            MuzakkiName = history.MuzakkiName,
            Address = history.Address,
            Quantity = history.Quantity,
            Date = Convertion.ConvertToIndonesianDate(history.Date),
            FitrahMoney = Convertion.ConvertToRupiahTwoZeros(history.FitrahMoney)??"-",
            FitrahRice = history.FitrahRice??0,
            InfaqMoney = Convertion.ConvertToRupiahTwoZeros(history.InfaqMoney)??"-",
            InfaqRice = history.InfaqRice??0,
            FidiyaMoney = Convertion.ConvertToRupiahTwoZeros(history.FidiyaMoney)??"-",
            FidiyaRice = history.FidiyaRice??0,
            MaalMoney = Convertion.ConvertToRupiahTwoZeros(history.MaalMoney)??"-",
            MaalRice = history.MaalRice??0,
            AmilUsername = history.AmilUsername,
            Code = history.Code
        });
        return new HistoryIndexViewModel(){
            Histories = model.ToList(),
            Pagination = new PaginationViewModel(){
                PageSize = pageSize,
                Page = page,
                TotalRows = _historyRepository.Count(name,address, year)
            },
            Name = name??"",
            Address = address??"",
            Year = year??"",
            Years = GetYears()
        };
    }
    public void Insert(HistoryUpsertViewModel ViewModel)
    {
        //untuk situasi pada waktu pergantian hari
        var recap = _recapRepository.Get(ViewModel.Date);
        if(recap==null){
            var newRecap = new Recap(){
                Date = ViewModel.Date
            };
            _recapRepository.Insert(newRecap);
        };  
        var model = new History(){
            MuzakkiName  = ViewModel.MuzakkiName,
            Address = ViewModel.Address,
            Quantity = ViewModel.Quantity,
            FitrahMoney = ViewModel.FitrahMoney,
            FitrahRice = ViewModel.FitrahRice,
            InfaqMoney = ViewModel.InfaqMoney,
            InfaqRice = ViewModel.InfaqRice,
            FidiyaMoney = ViewModel.FidiyaMoney,
            FidiyaRice = ViewModel.FidiyaRice,
            MaalMoney = ViewModel.MaalMoney,
            MaalRice = ViewModel.MaalRice,
            AmilUsername = ViewModel.AmilUsername,
            Note = ViewModel.Note,
            Code = GenerateCode(ViewModel.MuzakkiName),
            Date = ViewModel.Date
        };
        _historyRepository.Insert(model);
        // return new HistoryViewModel(){
        //     Code = inserted.Code,
        //     MuzakkiName = inserted.MuzakkiName,
        //     Date = Convertion.ConvertToIndonesianDate(inserted.Date),
        //     AmilUsername = inserted.AmilUsername
        // };
    }

    public HistoryUpsertViewModel Get()
    {
        return new HistoryUpsertViewModel(){
            Date = DateTime.Now,
            Amils = GetAmils()
        };
    }
    public HistoryUpsertViewModel Get(string code)
    {
        var model = _historyRepository.Get(code);
        return new HistoryUpsertViewModel(){
            Code = model.Code,
            MuzakkiName = model.MuzakkiName,
            Address = model.Address,
            Quantity = model.Quantity,
            FidiyaMoney = model.FidiyaMoney,
            FidiyaRice = model.FidiyaRice,
            FitrahMoney = model.FitrahMoney,
            FitrahRice = model.FitrahRice,
            InfaqMoney = model.InfaqMoney,
            InfaqRice = model.InfaqRice,
            MaalMoney = model.MaalMoney,
            MaalRice = model.MaalRice,
            AmilUsername = model.AmilUsername,
            Note = model.Note,
            Date = model.Date,
            Amils = GetAmils()
        };
    }
    public HistoryUpsertViewModel Update(HistoryUpsertViewModel ViewModel)
    {
        var model = _historyRepository.Get(ViewModel.Code);
        model.MuzakkiName = ViewModel.MuzakkiName;
        model.Quantity = ViewModel.Quantity;
        model.Address = ViewModel.Address;
        model.FitrahMoney = ViewModel.FitrahMoney;
        model.FitrahRice = ViewModel.FitrahRice;
        model.InfaqMoney = ViewModel.InfaqMoney;
        model.InfaqRice = ViewModel.InfaqRice;
        model.MaalMoney = ViewModel.MaalMoney;
        model.MaalRice = ViewModel.MaalRice;
        model.FidiyaMoney = ViewModel.FidiyaMoney;
        model.FidiyaRice = ViewModel.FidiyaRice;
        model.Note = ViewModel.Note;
        model.AmilUsername = ViewModel.AmilUsername;
        model.Date = ViewModel.Date;
        var updated = _historyRepository.Update(model);
        return new HistoryUpsertViewModel (){
            Code = updated.Code,
            MuzakkiName = updated.MuzakkiName,
            Quantity = updated.Quantity,
            FitrahMoney = updated.FitrahMoney,
            FitrahRice = updated.FitrahRice,
            FidiyaMoney = updated.FidiyaMoney,
            FidiyaRice = updated.FidiyaRice,
            InfaqMoney = updated.InfaqMoney,
            InfaqRice = updated.InfaqRice,
            MaalMoney = updated.MaalMoney,
            MaalRice = updated.MaalRice,
            Note = updated.Note,
            Date = updated.Date
        };
    }
    private List<SelectListItem> GetYears()
    {
        var models = _historyRepository.GetYears();
        List<SelectListItem> years = models
        .Select(year => 
            new SelectListItem(){
                Text = year.ToString(),
                Value = year.ToString()
            }
        )
        .ToList();
        return years;
    }
    private List<SelectListItem> GetAmils()
    {
        var models = _accountRepository.Get();
        List<SelectListItem> amils = models
        .Select(account => 
            new SelectListItem(){
                Text = account.Username,
                Value = account.Username
        })
        .ToList();
        return amils;
    }
    private  Random random = new Random();



    private  string GenerateCode(string name)
    {
        string nameCode = name.Substring(0, Math.Min(name.Length, 3)).ToUpper();
        string dateCode = DateTime.Today.ToString("yyyyMMdd");
        string randomNumber = random.Next(100, 1000).ToString();
        string code = $"{nameCode}{dateCode}{randomNumber}";
        return code;
    }   
}
