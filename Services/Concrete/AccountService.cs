using BankingApi.Domain.Models;
using BankingApi.Repository;
using BankingApi.Services.Interfaces;

namespace BankingApi.Services.Concrete
{
    public class AccountService: IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository) { 
        _accountRepository = accountRepository;
        }

        public async Task AddAccountAsync(Account account)
        {
            if (account.Balance < 0)
                throw new ArgumentException("Initial deposit cannot be negative.");

            await _accountRepository.AddAccountAsync(account);
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _accountRepository.GetAllAccountsAsync();
        }
    }
}
