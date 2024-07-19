using FitrahDataAccess.Models;

namespace FitrahBusiness.Interfaces;

public interface IRecapRepository
{
    Recap Get(DateTime date);
    void Insert(Recap model);
    Recap Update(Recap model);
}
