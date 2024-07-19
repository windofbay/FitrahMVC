using FitrahBusiness.Interfaces;
using FitrahDataAccess.Models;

namespace FitrahBusiness.Repositories;

public class HistoryRepository : IHistoryRepository
{
    private readonly FitrahContext _dbContext;

    public HistoryRepository(FitrahContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int Count(string name, string address, string year)
    {
        return _dbContext.Histories
        .Where(history=> 
            history.MuzakkiName.ToLower().Contains(name??"".ToLower())&&
            history.Address.ToLower().Contains(address??"".ToLower())&& 
            history.Date.ToString().ToLower().Contains(year??"".ToLower())
        )
        .Count();
    }
    public int Count(string year)
    {
        return _dbContext.Histories
        .Where(history=>history.Date.ToString().ToLower().Contains(year??"".ToLower()))
        .Count();
    }

    public void Delete(History model)
    {
        _dbContext.Histories.Remove(model);
        _dbContext.SaveChanges();
    }

    public List<History> Get(int page, int pageSize, string name, string address, string year)
    {
        return _dbContext.Histories
        .Where(history=> 
            history.MuzakkiName.ToLower().Contains(name??"".ToLower())&&
            history.Address.ToLower().Contains(address??"".ToLower()) && 
            history.Date.ToString().ToLower().Contains(year??"".ToLower())
        )
        .OrderByDescending(history=>history.Date)
        .Skip((page-1)*pageSize)
        .Take(pageSize).ToList();
    }
    public List<History> GetByYear(string year)
    {
        return _dbContext.Histories
        .Where(history=>history.Date.ToString().ToLower().Contains(year??"".ToLower()))
        .ToList();
    }

    public History Get(string code)
    {
        return _dbContext.Histories
        .Find(code)
        ??throw new NullReferenceException($"History with code={code} not found");
    }

    public void Insert(History model)
    {
        _dbContext.Histories.Add(model);
        _dbContext.SaveChanges();
    }

    public History Update(History model)
    {
        _dbContext.Histories.Update(model);
        _dbContext.SaveChanges();
        return model;
    }
    public List<int> GetYears(){
       return  _dbContext.Histories
       .Select(h=>h.Date.Year)
       .Distinct()
       .ToList();
    }
    public IQueryable<object> GetRecap()
    {
        // return _dbContext.Histories
        // .GroupBy(h=>h.Date)
        // .Select(g => new{
        //         Date = g.Key,
        //         TotalQuantity = g.Sum(h => h.Quantity),
        //         TotalFitrahMoney = g.Sum(h => h.FitrahMoney),
        //         TotalFitrahRice = g.Sum(h => h.FitrahRice),
        //         TotalInfaqMoney = g.Sum(h => h.InfaqMoney),
        //         TotalInfaqRice = g.Sum(h => h.InfaqRice),
        //         TotalFidiyahMoney = g.Sum(h => h.FidiyaMoney),
        //         TotalFidiyahRice = g.Sum(h => h.FidiyaRice),
        //         TotalMaalMoney = g.Sum(h => h.MaalMoney)
        // });
        var recapitulations = 
            from history in _dbContext.Histories
            group history by new {history.Date} into recap
            select new {Date = recap.Key.Date,
                TotalQuantity = recap.Sum(r => r.Quantity),
                TotalFitrahMoney = recap.Sum(r=>r.FitrahMoney),
                TotalFitrahRice = recap.Sum(r=>r.FitrahRice),
                TotalFidiyaMoney = recap.Sum(r=>r.FidiyaMoney),
                TotalFidiyaRice = recap.Sum(r=>r.FidiyaRice),
                TotalInfaqMoney = recap.Sum(r=>r.InfaqMoney),
                TotalInfaqRice = recap.Sum(r=>r.InfaqRice),
                TotalMaalMoney = recap.Sum(r=>r.MaalMoney)
            };
        return recapitulations;
    }
}
