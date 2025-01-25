using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GrileMedicinaDev.Entities
{
    public class CategoryEntity
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("category")]
        [BsonRepresentation(BsonType.String)]
        public string Category { get; set; }

        [BsonElement("quantity")]
        [BsonRepresentation(BsonType.Int32)]
        public int Quantity { get; set; }
    }
}
