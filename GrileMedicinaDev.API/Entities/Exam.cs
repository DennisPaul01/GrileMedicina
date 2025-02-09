using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace GrileMedicinaDev.Entities;

public class Exam
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string ExamType { get; set; }
    public List<Chapter> Chapters { get; set; }
    public bool RandomizeQuestions { get; set; }
    public bool RandomizeAnswers { get; set; }
    public int Quantity { get; set; }
    public bool Timed { get; set; }
    public int Timer { get; set; }
    public bool ShowAnswers { get; set; }
    public Question[] Questions { get; set; }
}
