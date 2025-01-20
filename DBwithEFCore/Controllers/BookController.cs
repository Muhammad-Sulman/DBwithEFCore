using DBwithEFCore.Data;
using DBwithEFCore.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> updateBookwithsinglequery([FromBody] Book model)
        {
            appDbContext.Books.Update(model); // in this way all required fields of entity must be provided to update the record 
                                              // if there is nullable field then leaving it in update sets its value to null and remove its existing value.

            //appDbContext.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified; // work same as above line 
            // Entity Framework Core to update an entity's state in the Change Tracker because entity framework core work change tracking model it hits database on savechanges method to modified it.
            await appDbContext.SaveChangesAsync();
            return Ok(model);
        }



        [HttpPut("bulk")]
        public async Task<IActionResult> updateBookinbulk()
        {
            //var book = appDbContext.Books.ToList();  // In this way we update in bulk but we have to hit database many times. so performance wise it is not good.
            //foreach (var item in book)
            //{
            //    item.Title = "updated";
            //}

            //await appDbContext.SaveChangesAsync();

            await appDbContext.Books
                .Where(x => x.NoOfPages == 140)  // conditional bulk update
                .ExecuteUpdateAsync(x => x
                .SetProperty(p => p.Description, "updated in Bulk")  // this replace the existing data
                .SetProperty(p => p.Title, p => p.Title + " updated new title") // this append the new data with existing data
                //.SetProperty(p => p.NoOfPages, 100)
                //.SetProperty(p => p.Description, p =>p.Title + "updated in Bulk") // in this we append the title to description along with new data.
                );

            // no need of savechanges method here query directly exicuted.

            return Ok();
        }



        [HttpDelete("{bookid}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int bookid)
        {
            //var book = appDbContext.Books.FirstOrDefault(x => x.Id == bookid);  // hits database 2 times 
            //if (book == null)
            //{
            //    return NotFound();
            //}
            //appDbContext.Books.Remove(book);
            //await appDbContext.SaveChangesAsync();

            var book = new Book { Id = bookid };
            appDbContext.Entry(book).State = EntityState.Deleted;  // hits database 1 time performance wise better.
            await appDbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("bulk")]
        public async Task<IActionResult> DeleteBookinbulk()
        {
            //var books = await appDbContext.Books.Where(x=>x.Id >3).ToArrayAsync(); // hits database 2 times
            //appDbContext.Books.RemoveRange(books);
            //await appDbContext.SaveChangesAsync();

            //var books = await appDbContext.Books.Where(x => x.Id > 3).ExecuteDeleteAsync(); // deletes in single database hit

            // we can also delete using EntityState method.

            return Ok();

        }

        [HttpGet("")]
        public async Task<IActionResult> Getbooks()
        {
            //var books = await appDbContext.Books.Select(x => new   // in this way using anonymous objet we can get book and required fields.
            //{
            //    x.Id,
            //    x.Title,
            //    x.Description,
            //    x.NoOfPages,

            //}).ToListAsync();


            //var books = await appDbContext.Books.Select(x => new Book()  // in this way using objet we can get book and required fields and other filds with default value if we dont assign thenm in object.
            //{                                                            // this type safty method used in complex projects. better method than anonymous object
            //   Id = x.Id,
            //   Title =  x.Title,
            //   Description =  x.Description,
            //   NoOfPages = x.NoOfPages,

            //}).ToListAsync();


            // Getting data from multiple tables using navigational properties is best method as compare to other methods. this method is most used in enityframework core
            //var books = await appDbContext.Books.Select(x => new   // in this example we getting data from 3 tables
            //{
            //    x.Id,
            //    x.Title,
            //    x.Description,
            //    x.NoOfPages,
            //    x.Language, // gives all fields of language
            //    x.Author,  //  gives all filds of author

            //}).ToListAsync();


            //var books = await appDbContext.Books.Select(x => new   // in this example we getting only requird filds from languages and author table.
            //{
            //    x.Id,
            //    x.Title,
            //    x.Description,
            //    x.NoOfPages,
            //    lantitle = x.Language.Title,  // if language table have futher navigation we can also get data from that table like (x.language.table.property)
            //    authername = x.Author.Name
            //}).ToListAsync();



            //Egar Loading
            //var books = await appDbContext.Books
            //    .Include(x => x.Language) // Egar loading gives us depth issue in one to many relationship as language have collection of books property in it so all books are loaded in result.
            //    .Include(x => x.Author)   // and cross the limit of max output which is 32bit. in (one to one) relation it works good. 
            //    .ToListAsync();


            //var books = await appDbContext.Books
            //    .Include(x => x.Author)
            //    //.Include(x => x.Author.Name)   // throws error we cant get specific property  within include. 
            //    //.ThenInclude(x=>x.table3) //using theninclude we can get data from table which is related or included within author.
            //    //.ThenInclude(x => x.table4) // agian we can get data from table4 which is within table3 of author. and so on with multiple tables.
            //    .ToListAsync();




            //Expilcit Loading
            var book = await appDbContext.Books.FirstAsync();
                await appDbContext.Entry(book).Reference(x=>x.Author).LoadAsync();   // for one to one relationship like book have only one author (not like=> author write many book) we use reference
                //await appDbContext.Entry(book).Reference(x => x.Language).LoadAsync(); // throws dept issue due to languages have collection books circular dependency of tables.
                 //await appDbContext.Entry(book).Reference(x => x.Language).Reference(x => x.Author).LoadAsync(); // throws error we cant use refernce more than one in single statement.
            return Ok(book);
        }
    }
}
