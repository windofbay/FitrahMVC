using FitrahBusiness.Interfaces;
using FitrahWeb.Helper;
using FitrahWeb.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FitrahWeb.Services;

public class RecapService
{
private readonly IHistoryRepository _historyRepository;
    private readonly IRecapRepository _recapRepository;

    public RecapService(IHistoryRepository historyRepository, IRecapRepository recapRepository)
    {
        _historyRepository = historyRepository;
        _recapRepository = recapRepository;
    }

    public RecapIndexViewModel Get(string? year)
    {
        var model = _historyRepository.GetByYear(year)
        .GroupBy(h=>h.Date)
        .OrderBy(g=>g.Key)
        .Select(g => new RecapViewModel() {
                Date = Convertion.ConvertToIndonesianDate(g.Key),
                PlainDate = g.Key,
                TotalQuantity = g.Sum(h => h.Quantity),
                TotalFitrahMoney = Convertion.ConvertToRupiahTwoZeros(g.Sum(h => h.FitrahMoney))??"-",
                TotalFitrahRice = g.Sum(h => h.FitrahRice)??0,
                TotalInfaqMoney = Convertion.ConvertToRupiahTwoZeros(g.Sum(h => h.InfaqMoney))??"-",
                TotalInfaqRice = g.Sum(h => h.InfaqRice)??0,
                TotalFidiyaMoney = Convertion.ConvertToRupiahTwoZeros(g.Sum(h => h.FidiyaMoney))??"-",
                TotalFidiyaRice = g.Sum(h => h.FidiyaRice)??0,
                TotalMaalMoney = Convertion.ConvertToRupiahTwoZeros(g.Sum(h => h.MaalMoney))??"-"
        });
        return new RecapIndexViewModel(){
            Recaps = model.ToList(),
            Year = year??"",
            Years = GetYears(),
            OverallRecap = GetOverallTotal(year)
        };
    }
    public RecapUpsertViewModel GetRecap(DateTime date)
    {
        var model = _recapRepository.Get(date);
        return new RecapUpsertViewModel(){
            Date = model.Date
        };
    }
    public RecapUpsertViewModel Get(DateTime date)
    {
        var model = _recapRepository.Get(date);
        return new RecapUpsertViewModel(){
            Date = model.Date,
            ImageName = model.Image 
        };
    }
    public string Upload(RecapUpsertViewModel ViewModel)
    { 
        var model = _recapRepository.Get(ViewModel.Date);
        model.Image = AddImage(ViewModel.Image);
        if (model.Image==null){
            return "Extension is not valid, please reupload with .jpg/.jpeg/.png file";
        } else{
            _recapRepository.Update(model);
            return "Image Uploaded/Updated";
        }
    }
    private string AddImage(IFormFile file)
    {
        List<string> validExtensions = new List<string>(){".jpg",".png",".jpeg"};
        string extension = Path.GetExtension(file.FileName);
        if(!validExtensions.Contains(extension)){
            return null;
            // return $"Extension is not valid ({string.Join(',',validExtensions)})";
        };
        // long size = file.Length;
        // if(size > (5*10000*10000)) return "Maximum size can be 10mb";
        string fileName = Guid.NewGuid().ToString()+extension;
        string path = Path.Combine(Directory.GetCurrentDirectory(),"Images");
        using FileStream stream = new FileStream(Path.Combine(path,fileName), FileMode.Create);
        file.CopyTo(stream);

        return fileName;
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
    private OverallRecapViewModel GetOverallTotal(string? year)
    {
        var histories = _historyRepository.GetByYear(year);
        var result = from history in histories
                 group history by history.Date into g
                 select new {
                    Date = g.Key,
                    TotalQuantity = g.Sum(h => h.Quantity),
                    TotalFitrahMoney = g.Sum(h => h.FitrahMoney),
                    TotalFitrahRice = g.Sum(h => h.FitrahRice),
                    TotalInfaqMoney = g.Sum(h => h.InfaqMoney),
                    TotalInfaqRice = g.Sum(h => h.InfaqRice),
                    TotalFidiyaMoney = g.Sum(h => h.FidiyaMoney),
                    TotalFidiyaRice = g.Sum(h => h.FidiyaRice),
                    TotalMaalMoney = g.Sum(h => h.MaalMoney)
            };
        var recaps = result.ToList();
        var sumModel = new {
            overallQuantity = recaps.Sum(r=>r.TotalQuantity),
            overallFitrahMoney = recaps.Sum(r=>r.TotalFitrahMoney),
            overalFitrahRice = recaps.Sum(r=>r.TotalFitrahRice),
            overallFidiyaMoney = recaps.Sum(r=>r.TotalFidiyaMoney),
            overallFidiyaRice = recaps.Sum(r=>r.TotalFidiyaRice),
            overallInfaqMoney = recaps.Sum(r=>r.TotalInfaqMoney),
            overallInfaqRice = recaps.Sum(r=>r.TotalInfaqRice),
            overallMaalMoney = recaps.Sum(r=>r.TotalMaalMoney)
        };
        var overall = new OverallRecapViewModel(){
            OverallQuantity = sumModel.overallQuantity,
            OverallFitrahMoney = Convertion.ConvertToRupiahTwoZeros(sumModel.overallFitrahMoney)??"-",
            OverallFitrahRice = sumModel.overalFitrahRice??0,
            OverallFidiyaMoney = Convertion.ConvertToRupiahTwoZeros(sumModel.overallFidiyaMoney)??"-",
            OverallFidiyaRice = sumModel.overallFidiyaRice??0,
            OverallInfaqMoney = Convertion.ConvertToRupiahTwoZeros(sumModel.overallInfaqMoney)??"-",
            OverallInfaqRice = sumModel.overallInfaqRice??0,
            OverallMaalMoney = Convertion.ConvertToRupiahTwoZeros(sumModel.overallMaalMoney)??"-"
        };
        return overall;
    }
}
