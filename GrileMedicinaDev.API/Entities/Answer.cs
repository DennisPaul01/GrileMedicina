using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace GrileMedicinaDev.Entities
{
    public class Answer
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("answer")]
        [BsonRepresentation(BsonType.String)]
        public string AnswerText { get; set; }

        [BsonElement("isCorrect")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool IsCorrect { get; set; }
    }
}