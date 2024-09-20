using FitrahBusiness.Interfaces;
using FitrahDataAccess.Models;

namespace FitrahBusiness.Repositories;

public class RecapRepository : IRecapRepository
{
    private readonly FitrahContext _dbContext;

    public RecapRepository(FitrahContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Recap> Get(DateTime date)
    {
        return await _dbContext.Recaps
        .FindAsync(date);
        //??throw new NullReferenceException($"Recap with date={date} not found");
    }
    public async Task<Recap> Insert(Recap model)
    {
        _dbContext.Recaps.Add(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }
    public async Task<Recap> Update(Recap model)
    {
        _dbContext.Recaps.Update(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }
}
