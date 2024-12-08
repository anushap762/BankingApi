using BankingApi.Repository;
using BankingApi.Domain.Models;
using BankingApi.Services.Interfaces;
using System.Xml;

namespace BankingApi.Services.Concrete
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task AddTransactionAsync(AddTransactionModel transaction)
        {
            if (transaction.Amount <= 0)
                throw new ArgumentException("Transaction amount must be greater than zero.");

            var newTransaction = new Transaction
            {
                AccountId = transaction.AccountId,
                Date = transaction.Date,
                Type = transaction.Type,
                Amount = transaction.Amount,
                TransactionId = Guid.NewGuid().ToString() // Use a GUID for unique identification
            };
            await _transactionRepository.AddTransactionAsync(newTransaction);
        }

        public Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
        {
            throw new NotImplementedException();
        }
    }
}

