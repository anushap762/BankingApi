using BankingApi.Services.Concrete;
using BankingApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankingApi.Domain.Models;

namespace BankingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // Add a transaction
        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] AddTransactionModel transaction)
        {
            try
            {
                await _transactionService.AddTransactionAsync(transaction);
                return Ok("Transaction added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Get transactions for a specific account
        [HttpGet("{accountId}")]
        public async Task<IActionResult> GetTransactionsByAccountId(int accountId)
        {
            try
            {
                var transactions = await _transactionService.GetTransactionsByAccountIdAsync(accountId);
                if (transactions == null || !transactions.Any())
                    return NotFound("No transactions found for this account.");

                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}

