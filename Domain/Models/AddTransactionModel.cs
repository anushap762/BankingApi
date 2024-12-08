namespace BankingApi.Domain.Models
{
    public class AddTransactionModel
    {
        public string AccountId { get; set; }
        public DateTime Date { get; set; }
        public char Type { get; set; }
        public decimal Amount { get; set; }
    }
}
