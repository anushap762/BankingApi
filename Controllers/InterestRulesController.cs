using BankingApi.Domain.dtos;
using BankingApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestRulesController : ControllerBase
    {
        private readonly IInterestRuleService _service;

        public InterestRulesController(IInterestRuleService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetInterestRules()
        {
            var rules = await _service.GetAllInterestRulesAsync();
            var result = rules.Select(r => new
            {
                Date = r.EffectiveDate.ToString("yyyyMMdd"),
                r.RuleId,
                r.Rate
            });
            return Ok(result);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddInterestRule([FromBody] InterestRuleDto ruleDto)
        {
            try
            {
                await _service.AddOrUpdateInterestRuleAsync(ruleDto);
                return Ok("Interest rule added successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
