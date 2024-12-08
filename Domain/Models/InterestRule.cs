using System.ComponentModel.DataAnnotations;

namespace BankingApi.Domain.Models
{
    public class InterestRule
    {
        [Key]
        public string RuleId { get; set; }
        public DateTime EffectiveDate { get; set; }
        public decimal Rate { get; set; }
    }
}
