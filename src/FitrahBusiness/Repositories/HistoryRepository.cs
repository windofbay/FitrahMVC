using FitrahBusiness.Interfaces;
using FitrahBusiness.Objects;
using FitrahDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FitrahBusiness.Repositories;

public class HistoryRepository : IHistoryRepository
{
    private readonly FitrahContext _dbContext;

    public HistoryRepository(FitrahContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Count(string name, string address, string year)
    {
        return await _dbContext.Histories
        .Where(history=> 
            history.MuzakkiName.ToLower().Contains(name??"".ToLower())&&
            history.Address.ToLower().Contains(address??"".ToLower())&& 
            history.Date.ToString().ToLower().Contains(year??"".ToLower())
        )
        .CountAsync();
    }
    public async Task<int> Count(string year)
    {
        return await _dbContext.Histories
        .Where(history=>history.Date.ToString().ToLower().Contains(year??"".ToLower()))
        .CountAsync();
    }

    public async void Delete(History model)
    {
        _dbContext.Histories.Remove(model);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<History>> Get(int page, int pageSize, string name, string address, string year)
    {
        return await _dbContext.Histories
        .Where(history=> 
            history.MuzakkiName.ToLower().Contains(name??"".ToLower())&&
            history.Address.ToLower().Contains(address??"".ToLower()) && 
            history.Date.ToString().ToLower().Contains(year??"".ToLower())
        )
        .OrderByDescending(history=>history.Date)
        .Skip((page-1)*pageSize)
        .Take(pageSize).ToListAsync();
    }
    public async Task<List<History>> GetByYear(string year)
    {
        return await _dbContext.Histories
        .Where(history=>history.Date.ToString().ToLower().Contains(year??"".ToLower()))
        .ToListAsync();
    }

    public async Task<History> Get(string code)
    {
        return await _dbContext.Histories
        .FindAsync(code)
        ??throw new NullReferenceException($"History with code={code} not found");
    }

    public async Task<History> Insert(History model)
    {
        _dbContext.Histories.Add(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<History> Update(History model)
    {
        _dbContext.Histories.Update(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }
    public async Task<List<int>> GetYears(){
       return await _dbContext.Histories
       .Select(h=>h.Date.Year)
       .Distinct()
       .ToListAsync();
    }

    public async Task<List<GroupHistoryByAddress>> CountHistoryPerAddress()
    {
        var result = await _dbContext.Histories
            .GroupBy(h => h.Address)
            .Select(g => new GroupHistoryByAddress
            {
                Address = g.Key, 
                Count = g.Count()
            })
            .ToListAsync();
        return result;
    }
    // public IQueryable<object> GetRecap()
    // {
    //     var recapitulations = 
    //         from history in  _dbContext.Histories
    //         group history by new {history.Date} into recap
    //         select new {
    //             recap.Key.Date,
    //             TotalQuantity = recap.Sum(r => r.Quantity),
    //             TotalFitrahMoney = recap.Sum(r=>r.FitrahMoney),
    //             TotalFitrahRice = recap.Sum(r=>r.FitrahRice),
    //             TotalFidiyaMoney = recap.Sum(r=>r.FidiyaMoney),
    //             TotalFidiyaRice = recap.Sum(r=>r.FidiyaRice),
    //             TotalInfaqMoney = recap.Sum(r=>r.InfaqMoney),
    //             TotalInfaqRice = recap.Sum(r=>r.InfaqRice),
    //             TotalMaalMoney = recap.Sum(r=>r.MaalMoney)
    //         };
    //     return recapitulations;
    // }
}
