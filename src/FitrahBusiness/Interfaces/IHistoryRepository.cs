using FitrahDataAccess.Models;

namespace FitrahBusiness.Interfaces;

public interface IHistoryRepository
{
    Task<List<History>> GetByYear(string year);
    Task<List<History>> Get(int page, int pageSize , string name, string address, string year);
    Task<History> Get(string code);
    Task<History> Insert(History model);
    Task<History> Update(History model);
    void Delete(History model);
    Task<int> Count(string name, string address, string year);
    Task<int> Count(string year);
    Task<List<int>> GetYears();
    //IQueryable<object> GetRecap();
}
