using BankingApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BankingApi.Repository.Concrete
{
    public class StatementRepository : IStatementRepository
    {
        private readonly BankingDbContext _bankingDbContext;
        public StatementRepository(BankingDbContext bankingDbContext)
        {
            _bankingDbContext = bankingDbContext;
        }


        public async Task<StatementModel> GetStatementAsync(string accountId, string month)
        {
            if (!Regex.IsMatch(month, @"^\d{6}$"))
            {
                throw new ArgumentException("Invalid month format. Use YYYYMM.");
            }

            var startDate = DateTime.ParseExact(month + "01", "yyyyMMdd", CultureInfo.InvariantCulture);
            var endDate = startDate.AddMonths(1).AddDays(-1); 
           
            var transactions = await _bankingDbContext.Transactions
                .Where(t => t.AccountId == accountId && t.Date >= startDate && t.Date <= endDate)
                .OrderBy(t => t.Date)
                .ToListAsync();
            
            var dailyBalances = CalculateEODBalances(transactions, startDate, endDate);
           
            var interestRules = await _bankingDbContext.InterestRules
                .OrderBy(r => r.EffectiveDate)
                .ToListAsync();
            var totalInterest = CalculateInterest(dailyBalances, interestRules);
           
            transactions.Add(new Transaction
            {
                TransactionId = string.Empty,
                Date = endDate,
                Type = 'I',
                Amount = totalInterest
            });

            var runningBalance = 0m;
            var statementTransactions = transactions
                .OrderBy(t => t.Date)
                .Select(t =>
                {
                    runningBalance += t.Type == 'D' ? t.Amount : -t.Amount;
                    return new StatementTransaction
                    {
                        Date = t.Date.ToString("yyyyMMdd"),
                        TxnId = t.TransactionId,
                        Type = t.Type,
                        Amount = t.Amount,
                        Balance = runningBalance
                    };
                })
                .ToList();
           
            return new StatementModel
            {
                Account = accountId,
                Transactions = statementTransactions
            };
        }


        private List<DailyBalance> CalculateEODBalances(
        List<Transaction> transactions,
        DateTime startDate,
        DateTime endDate)
        {
            var dailyBalances = new List<DailyBalance>();
            decimal runningBalance = 0m;

            var transactionsByDate = transactions
                .GroupBy(t => t.Date.Date)
                .ToDictionary(g => g.Key, g => g.ToList());

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (transactionsByDate.TryGetValue(date, out var dailyTransactions))
                {
                    foreach (var txn in dailyTransactions)
                    {
                        runningBalance += txn.Type == 'D' ? txn.Amount : -txn.Amount;
                    }
                }

                dailyBalances.Add(new DailyBalance
                {
                    Date = date,
                    Balance = runningBalance
                });
            }

            return dailyBalances;
        }



     private decimal CalculateInterest(List<DailyBalance> dailyBalances, List<InterestRule> interestRules)
      {
            decimal totalInterest = 0m;

            for (int i = 0; i < dailyBalances.Count; i++)
            {
                var currentDate = dailyBalances[i].Date;
                var applicableRule = interestRules
                    .Where(r => r.EffectiveDate <= currentDate)
                    .OrderByDescending(r => r.EffectiveDate)
                    .FirstOrDefault();

                if (applicableRule != null)
                {
                    var balance = dailyBalances[i].Balance;
                    var days = (i < dailyBalances.Count - 1)
                        ? (dailyBalances[i + 1].Date - currentDate).Days
                        : 1;

                    totalInterest += (balance * applicableRule.Rate / 100) * days;
                }
            }

            return Math.Round(totalInterest / 365m, 2); 
        }
    }
}
