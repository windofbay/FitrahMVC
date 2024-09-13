using FitrahDataAccess.Models;

namespace FitrahBusiness.Interfaces;

public interface IRecapRepository
{
    Task<Recap> Get(DateTime date);
    Task<Recap> Insert(Recap model);
    Task<Recap> Update(Recap model);
}
