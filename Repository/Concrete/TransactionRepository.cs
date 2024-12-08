using BankingApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BankingApi.Repository.Concrete
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankingDbContext _dbContext;

        public TransactionRepository(BankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIdAsync(int accountId)
        {
            return await _dbContext.Transactions
                .Where(t => t.AccountId == accountId.ToString())
                .ToListAsync();
        }

        public async Task AddTransactionAsync(Transaction transaction)
        {           
            if (transaction.Amount <= 0 || Math.Round(transaction.Amount, 2) != transaction.Amount)
                throw new ArgumentException("Amount must be greater than zero with up to 2 decimal places.");
            
            var account = await _dbContext.Accounts
                                          .FirstOrDefaultAsync(a => a.AccountId == transaction.AccountId);
            if (account == null)
            {
                // If account doesn't exist, ensure the first transaction is a deposit
                if (transaction.Type == 'W' || transaction.Type == 'w')
                    throw new InvalidOperationException("The first transaction for a new account must be a deposit.");

                // Create a new account
                account = new Account
                {
                    AccountId = transaction.AccountId,
                    Balance = 0 // Initialize balance to 0
                };

                await _dbContext.Accounts.AddAsync(account);
            }
           
            if (transaction.Type == 'D' || transaction.Type == 'd')
            {
                account.Balance += transaction.Amount;
            }
            else if (transaction.Type == 'W' || transaction.Type == 'w')
            {
                if (account.Balance < transaction.Amount)
                    throw new InvalidOperationException("Account balance cannot be negative.");

                account.Balance -= transaction.Amount;
            }
           
            string datePart = transaction.Date.ToString("yyyyMMdd"); 
            int transactionCountForDate = await _dbContext.Transactions
                                                          .CountAsync(t => t.Date == transaction.Date) + 1;
            transaction.TransactionId = $"{datePart}-{transactionCountForDate:D2}";
           
            await _dbContext.Transactions.AddAsync(transaction);
          
            await _dbContext.SaveChangesAsync();
        }
    }
    }
