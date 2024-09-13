using FitrahDataAccess.Models;

namespace FitrahBusiness.Interfaces;

public interface IAccountRepository
{
    List<Account> Get();
    Task<Account> Get(string username);
}
