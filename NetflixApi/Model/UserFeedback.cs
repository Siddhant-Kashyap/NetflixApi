using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace NetflixApi.Model
{
    public class UserFeedback
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("UserId")]
        public string UserId { get; set; }

        [BsonElement("Rating")]
        public int Rating { get; set; }

        [BsonElement("Comment")]
        public string Comment { get; set; }
    }
}
