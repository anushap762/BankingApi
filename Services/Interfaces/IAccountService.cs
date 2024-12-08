using BankingApi.Domain.Models;

namespace BankingApi.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        //Task<Account> GetAccountByIdAsync(int accountId);
        Task AddAccountAsync(Account account);
        //Task UpdateAccountAsync(Account account);
        //Task DeleteAccountAsync(int accountId);
    }
}
