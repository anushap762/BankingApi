namespace BankingApi.Domain.Models
{
    
    public class DailyBalance
    {
        public DateTime Date { get; set; }
        public decimal Balance { get; set; }
    }
    public class StatementModel
    {
        public string Account { get; set; }
        public List<StatementTransaction> Transactions { get; set; }
    }
    public class StatementTransaction
    {
        public string Date { get; set; }
        public string TxnId { get; set; }
        public char Type { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
    }

}
