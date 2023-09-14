using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace NetflixApi.Model
{
    public class Video
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string name { get; set; }

       
        [BsonElement("data")]
        public BsonBinaryData Data { get; set; }
    }

   
}
