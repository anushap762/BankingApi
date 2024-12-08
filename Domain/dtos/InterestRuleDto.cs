namespace BankingApi.Domain.dtos
{
    public class InterestRuleDto
    {       
        public string RuleId { get; set; }
            public string EffectiveDate { get; set; }
            public decimal Rate { get; set; }
        
    }
}
