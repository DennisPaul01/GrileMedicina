
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GrileMedicinaDev.Models;

public class AnswerCreationDto
{
    [BsonElement("answer")]
    [BsonRepresentation(BsonType.String)]
    public string AnswerText { get; set; }

    [BsonElement("isCorrect")]
    [BsonRepresentation(BsonType.Boolean)]
    public bool IsCorrect { get; set; }
}
