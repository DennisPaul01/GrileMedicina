using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GrileMedicinaDev.Models
{
    public class PagesDto
    {
        [BsonElement("start")]
        [BsonRepresentation(BsonType.Int32)]
        public int Start { get; set; }

        [BsonElement("end")]
        [BsonRepresentation(BsonType.Int32)]
        public int End { get; set; }
    }
}
