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
        public async Task<ActionResult<string>> Shorten(string url,string? alias = null)
        {
            // if(!Uri.IsWellFormedUriString(url,UriKind.Absolute))
            //     return BadRequest("Invalid URL");

            var existingURL =_collection.Find(sl => sl.Url == url).FirstOrDefault();
            if(existingURL != null)
                return $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{existingURL.Id}";



            ShortLink shortLink;
            if(String.IsNullOrWhiteSpace(alias))
                shortLink = new ShortLink(url);
            else
                shortLink = new ShortLink{
                    Id = alias,
                    Url = url
                };
            
            _collection.Insert(shortLink);
            
            var responseURI = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{shortLink.Id}";

            return responseURI;
        }

        
    }
    
}