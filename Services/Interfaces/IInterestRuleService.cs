using BankingApi.Domain.dtos;
using BankingApi.Domain.Models;

namespace BankingApi.Services.Interfaces
{
    public interface IInterestRuleService
    {
        Task<IEnumerable<InterestRule>> GetAllInterestRulesAsync();
        Task AddOrUpdateInterestRuleAsync(InterestRuleDto ruleDto);
    }
}
