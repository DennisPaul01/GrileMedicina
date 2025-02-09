using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using GrileMedicinaDev.Entities;

namespace GrileMedicinaDev.Entities
{
    public class Chapter
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("isUserContent")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool IsUserContent { get; set; }

        [BsonElement("categories")]
        [BsonRepresentation(BsonType.String)]
        public List<string> Categories { get; set; }

        [BsonElement("quantity")]
        [BsonRepresentation(BsonType.Int32)]
        public int Quantity { get; set; }

        [BsonElement("name")]
        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonElement("createdBy")]
        [BsonRepresentation(BsonType.String)]
        public string CreatedBy { get; set; }

        [BsonElement("pages")]
        public Pages Pages { get; set; }

        [BsonElement("explanationsGenerating")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool ExplanationsGenerating { get; set; }
    }
}
