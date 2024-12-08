using BankingApi.Domain.Models;

namespace BankingApi.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId);
        Task AddTransactionAsync(AddTransactionModel transaction);
    }
}
