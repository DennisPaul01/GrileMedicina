using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


namespace GrileMedicinaDev.Entities
{
    public class Pages
    {
        [BsonElement("start")]
        [BsonRepresentation(BsonType.Int32)]
        public int Start { get; set; }

        [BsonElement("end")]
        [BsonRepresentation(BsonType.Int32)]
        public int End { get; set; }

        [BsonElement("id")]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }
    }
}
