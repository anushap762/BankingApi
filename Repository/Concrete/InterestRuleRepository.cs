using BankingApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApi.Repository.Concrete
{
    public class InterestRuleRepository: IInterestRuleRepository
    {
        private readonly BankingDbContext _dbContext;

        public InterestRuleRepository(BankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<InterestRule>> GetAllInterestRulesAsync()
        {
            return await _dbContext.InterestRules
                .OrderBy(r => r.EffectiveDate)
                .ToListAsync();
        }

        public async Task AddOrUpdateInterestRuleAsync(InterestRule interestRule)
        {
            var existingRule = await _dbContext.InterestRules
                .FirstOrDefaultAsync(r => r.EffectiveDate == interestRule.EffectiveDate);

            if (existingRule != null)
            {
                _dbContext.InterestRules.Remove(existingRule);
            }

            await _dbContext.InterestRules.AddAsync(interestRule);
            await _dbContext.SaveChangesAsync();
        }
    }
}
