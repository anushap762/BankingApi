using BankingApi.Domain.Models;

namespace BankingApi.Services.Interfaces
{
    public interface IStatementService
    {
        Task<StatementModel> GenerateStatementAsync(string account, string month);
    }
}
