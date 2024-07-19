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

    public Recap Get(DateTime date)
    {
        return _dbContext.Recaps
        .Find(date);
        // ??throw new NullReferenceException($"Recap with date={date} not found");
    }
    public void Insert(Recap model)
    {
        _dbContext.Recaps.Add(model);
        _dbContext.SaveChanges();
    }
    public Recap Update(Recap model)
    {
        _dbContext.Recaps.Update(model);
        _dbContext.SaveChanges();
        return model;
    }
}
