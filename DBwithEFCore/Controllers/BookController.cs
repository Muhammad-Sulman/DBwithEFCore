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


        [HttpPut("{bookid}")]
        public async Task<IActionResult> updateBook([FromRoute] int bookid, [FromBody] Book model) // bookid to find book from database, and user data come in book model that to be updated.
        {
            var book = appDbContext.Books.FirstOrDefault(x => x.Id == bookid);  // this is basic way to update data in which we hit database 2 times
            if (book == null)
            {
                return NotFound();
            }

            book.Title = model.Title;
            book.Description = model.Description;
            book.NoOfPages = model.NoOfPages;

            await appDbContext.SaveChangesAsync();
            return Ok(model);
        }




        [HttpPut("")]
        public async Task<IActionResult> updateBookwithsinglequery( [FromBody] Book model) 
        {
            appDbContext.Books.Update(model); // in this way all required fields of entity must be provided to update the record 
                                              // if there is nullable field then leaving it in update sets its value to null and remove its existing value.
            
            //appDbContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified; // work same as above line 
           // Entity Framework Core to update an entity's state in the Change Tracker because entity framework core work change tracking model it hits database on savechanges method to modified it.
            await appDbContext.SaveChangesAsync();
            return Ok(model);
        }
    }
}
