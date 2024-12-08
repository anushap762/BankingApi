using BankingApi.Domain.Models;

namespace BankingApi.Repository
{
    public interface IStatementRepository
    {
        Task<StatementModel> GetStatementAsync(string account, string month);
    }
}
