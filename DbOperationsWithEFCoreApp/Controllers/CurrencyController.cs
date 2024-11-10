﻿using DbOperationsWithEFCoreApp.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbOperationsWithEFCoreApp.Controllers
{
    [Route("api/currencies")]
    [ApiController]
    public class CurrencyController(AppDbContext appDbContext) : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> GetAllCurrencies()
        {
            //var result = _appDbContext.Currencies.ToList();
            //var result = (from currencies in _appDbContext.Currencies
            //select currencies).ToList();

            //var result = await _appDbContext.Currencies.ToListAsync();
            var result = await (from currencies in appDbContext.Currencies
                                select currencies).AsNoTracking().ToListAsync();

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCurrencyByIdAsync([FromRoute] int id)
        {
            var result = await appDbContext.Currencies.FindAsync(id);
            return Ok(result);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCurrencyByNameAsync([FromRoute] string name, [FromQuery] string? description)
        {
            //var result = await _appDbContext.Currencies
            //    .FirstOrDefaultAsync(x => 
            //    x.Title == name 
            //    && (string.IsNullOrEmpty(description) || x.Description == description)
            //    );

            var result = await appDbContext.Currencies
                .Where(x =>
                x.Title == name
                && (string.IsNullOrEmpty(description) || x.Description == description)
                ).ToListAsync();

            return Ok(result);
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetCurrenciesByIdsAsync([FromBody] List<int> ids)
        {
            //var ids = new List<int> { 1 };
            var result = await appDbContext.Currencies
              .Where(x=> ids.Contains(x.Id))
              .ToListAsync();

            return Ok(result);
        }
    }
}
