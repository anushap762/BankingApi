using BankingApi.Domain.Models;

namespace BankingApi.Repository
{
    public interface IInterestRuleRepository
    {
        Task<IEnumerable<InterestRule>> GetAllInterestRulesAsync();
        Task AddOrUpdateInterestRuleAsync(InterestRule interestRule);
    }
}
