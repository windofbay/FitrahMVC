using FitrahDataAccess.Models;

namespace FitrahBusiness.Interfaces;

public interface IAccountRepository
{
    List<Account> Get();
    Account Get(string username);
}
