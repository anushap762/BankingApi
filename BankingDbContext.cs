using BankingApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingApi
{
    public class BankingDbContext:DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<InterestRule> InterestRules { get; set; }

        public BankingDbContext(DbContextOptions<BankingDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasKey(a => a.AccountId);
            modelBuilder.Entity<Transaction>().HasKey(t => t.TransactionId);
            modelBuilder.Entity<InterestRule>().HasKey(ir => ir.RuleId);
        }

    }
}
