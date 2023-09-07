using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NetflixApi.Model
{
    public class LocalUsers
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
       
        public string Password { get; set; }
        
    }
}
