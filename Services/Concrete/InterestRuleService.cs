using BankingApi.Domain.dtos;
using BankingApi.Domain.Models;
using BankingApi.Repository;
using BankingApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankingApi.Services.Concrete
{
    public class InterestRuleService:IInterestRuleService
    {
        private readonly IInterestRuleRepository _repository;

        public InterestRuleService(IInterestRuleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<InterestRule>> GetAllInterestRulesAsync()
        {
            return await _repository.GetAllInterestRulesAsync();
        }

        public async Task AddOrUpdateInterestRuleAsync(InterestRuleDto ruleDto)
        {
            // Validate input
            if (!DateTime.TryParseExact(ruleDto.EffectiveDate, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                throw new ArgumentException("Invalid date format. Use YYYYMMdd.");
            }

            if (ruleDto.Rate <= 0 || ruleDto.Rate >= 100)
            {
                throw new ArgumentException("Rate must be greater than 0 and less than 100.");
            }
            // Map DTO to entity
            var interestRule = new InterestRule
            {
                RuleId = ruleDto.RuleId,
                EffectiveDate = date,
                Rate = ruleDto.Rate
            };

            await _repository.AddOrUpdateInterestRuleAsync(interestRule);
        }

        private string GenerateRuleId(List<InterestRule> existingRules)
        {
            // Generate a new sequential number based on existing rules
            int nextNumber = existingRules.Count + 1;

            // Create RuleId in sequential format (e.g., 01, 02, 03)
            return $"RULE+{nextNumber}";
        }
    }
}
