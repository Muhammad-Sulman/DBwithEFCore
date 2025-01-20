using DBwithEFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DBwithEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public LanguageController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet("")]
        public ActionResult GetAllLanguages()
        {
            var languages = _appDbContext.Languages.ToList();   
            //Exipilcit Loading.
            //foreach (var language in languages)
            //{
            //    _appDbContext.Entry(language).Collection(x=>x.Books).Load(); // one to many relation like=> language related to many books. we use collection.
            //}                                                               //throws depth error because in book table we have language property.
            return Ok(languages);
        }
    }
}
