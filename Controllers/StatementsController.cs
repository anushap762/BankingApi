using BankingApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BankingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatementsController : ControllerBase
    {
        private readonly IStatementService _statementService;

        public StatementsController(IStatementService statementService)
        {
            _statementService = statementService;
        }

        [HttpGet("{account}/{month}")]
        public async Task<IActionResult> GetStatement(string account, string month)
        {
            var statement = await _statementService.GenerateStatementAsync(account, month);

            if (statement == null)
            {
                return NotFound("Statement not found.");
            }

            return Ok(statement);  
        }
    }
}

