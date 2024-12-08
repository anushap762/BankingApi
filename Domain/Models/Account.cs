using System.Transactions;

namespace BankingApi.Domain.Models
{
    public class Account
    {
        public string AccountId { get; set; }
        public decimal Balance { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
