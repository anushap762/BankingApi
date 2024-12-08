using BankingApi.Domain.Models;
using BankingApi.Repository;
using BankingApi.Services.Interfaces;

namespace BankingApi.Services.Concrete
{
    public class StatementService : IStatementService
    {
        private readonly IStatementRepository _statementRepository;

        public StatementService(IStatementRepository statementRepository)
        {
            _statementRepository = statementRepository;
        }

        public async Task<StatementModel> GenerateStatementAsync(string account, string month)
        {
            var statement = await _statementRepository.GetStatementAsync(account, month);
            return statement;
        }
    }
}
