using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using GrileMedicinaDev.Entities;

namespace GrileMedicinaDev.Entities
{
    public class Chapter
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public bool IsUserContent { get; set; }
        public List<string> Categories { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public Pages Pages { get; set; }
        public bool ExplanationsGenerating { get; set; }
    }
}
