namespace BankingApi.Domain.Models
{
    public class Transaction
    {
        public string TransactionId { get; set; }
        public string AccountId { get; set; }
        public DateTime Date { get; set; }
        public char Type { get; set; }
        public decimal Amount { get; set; }       

        public Account Account { get; set; }
    }
}
