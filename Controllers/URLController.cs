using LiteDB;
using Microsoft.AspNetCore.Mvc;
using minnak.Entities;

namespace minnak.Controllers
{
    [ApiController]
    [Route("api/URL/[Action]")]
    public class URLController : ControllerBase
    {
        private  ILiteDatabase _database{get;set;}

        private ILiteCollection<ShortLink> _collection{get;set;}

        public URLController(ILiteDatabase database)
        {
            _database = database;
            _collection = _database.GetCollection<ShortLink>("ShortLinks");
  
        }

        [HttpPost("{url}")]
        public async Task<string> Shorten(string url)
        {
            var shortlink = new ShortLink(url);

            _collection.Insert(shortlink);
            

            var responseURI = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{shortlink.Id}";

            return responseURI;
        }

        
    }
    
}