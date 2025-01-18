using DBwithEFCore.Data;
using DBwithEFCore.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DBwithEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController(AppDbContext appDbContext) : ControllerBase   // using primary construct a shortcut way for dependency injection
    {
        [HttpPost("")]
        public async Task<IActionResult> AddNewBook([FromBody] Book model)
        {
            //var author = new Author() // hard coded data insertion
            //{
            //    Name = "author 1",
            //    Email= "test@gmail.com"
            //};

            //model.Author = author;
            appDbContext.Books.Add(model);
            await appDbContext.SaveChangesAsync();
            return Ok(model);
        }
        
        [HttpPost("bulk")]
        public async Task<IActionResult> AddBooks([FromBody] List<Book> model)
        {
            appDbContext.Books.AddRange(model);
            await appDbContext.SaveChangesAsync();
            return Ok(model);
        }
    }
}
