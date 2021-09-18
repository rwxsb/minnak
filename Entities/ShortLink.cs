using LiteDB;
using shortid;
using shortid.Configuration;

namespace minnak.Entities
{
    public class ShortLink
    {
        public ShortLink()
        {
            
        }
        public ShortLink(string url)
        {
            Id = ShortId.Generate(options: new GenerationOptions{
                UseNumbers = false,
                UseSpecialCharacters = false,
                Length = 8
            });
            Url = url;
        }

        [BsonId]
        public string  Id {get;set;} 

        public string Url {get;set;}
    }
}