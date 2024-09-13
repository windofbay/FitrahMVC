using FitrahBusiness.Interfaces;
using FitrahDataAccess.Models;

namespace FitrahBusiness.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly FitrahContext _dbContext;

    public AccountRepository(FitrahContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Account> Get()
    {
        return _dbContext.Accounts.ToList();
    }
    public async Task<Account> Get(string username)
    {
        return await _dbContext.Accounts.FindAsync(username)
        ??throw new Exception("Akun tidak ditemukan");
    }
}
