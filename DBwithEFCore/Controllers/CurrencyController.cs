using DBwithEFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

namespace DBwithEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CurrencyController(AppDbContext appDbContext)  // Dependency Injection
        {
            _context = appDbContext;
        }


        //[HttpGet("")]
        //public IActionResult GetAllCurrencies() 
        //{
        //    //var currencies = _context.Currencies.ToList(); // Getting all curriences using LINQ
        //    var currencies = (from cur in _context.Currencies select cur).ToList(); // LINQ like SQL functions same as above line
        //    return Ok(currencies);
        //}


        // Asynchronous call best method to call api becuase increses performance
        [HttpGet("")]
        public async Task<IActionResult> GetAllCurrencies()
        {
            //var currencies = await _context.Currencies.ToListAsync(); // Getting all curriences using LINQ
            var currencies = await (from cur in _context.Currencies select cur).ToListAsync(); // LINQ like SQL functions same as above line
            return Ok(currencies);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllCurrenciesAsync([FromRoute] int id)
        {
            var currencies = await _context.Currencies.FindAsync(id); // Getting  curriences using LINQ based on id or primary key
            
            return Ok(currencies);
        }

    }
}
