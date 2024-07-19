using FitrahDataAccess.Models;

namespace FitrahBusiness.Interfaces;

public interface IHistoryRepository
{
    List<History> GetByYear(string year);
    List<History> Get(int page, int pageSize , string name, string address, string year);
    History Get(string code);
    void Insert(History model);
    History Update(History model);
    void Delete(History model);
    int Count(string name, string address, string year);
    int Count(string year);
    List<int> GetYears();
    IQueryable<object> GetRecap();
}
