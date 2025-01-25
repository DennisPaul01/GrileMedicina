using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace GrileMedicinaDev.Entities
{
    public class Question
    {
        [BsonId]
        [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }  // ID-ul întrebării în MongoDB

        // Modificăm câmpul 'ChapterId' să fie stocat ca șir
        [BsonElement("chapterId")]
        [BsonRepresentation(BsonType.String)]  // Salvează 'chapterId' ca șir, nu ObjectId
        public string ChapterId { get; set; }

        // Modificăm 'Pages' pentru a salva fiecare element ca șir (dacă e nevoie)
        [BsonElement("pages")]
        [BsonRepresentation(BsonType.Int32)]  // În mod implicit, List<int> va fi tratată corect
        public List<int> Pages { get; set; }

        // Rămâne ca 'List<string>' pentru că este o listă de stringuri care pot fi corect salvate de MongoDB
        [BsonElement("correct")]
        [BsonRepresentation(BsonType.String)]  // Liste de stringuri sunt deja tratate corect
        public List<string> CorrectAnswers { get; set; }

        [BsonElement("answersCount")]
        [BsonRepresentation(BsonType.Int32)]
        public int AnswersCount { get; set; }

        [BsonElement("correctAnswersCount")]
        [BsonRepresentation(BsonType.Int32)]
        public int CorrectAnswersCount { get; set; }

        [BsonElement("explanationExists")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool ExplanationExists { get; set; }

        [BsonElement("questionIndex")]
        [BsonRepresentation(BsonType.Int32)]
        public int QuestionIndex { get; set; }

        [BsonElement("answersOrder")]
        [BsonRepresentation(BsonType.String)]  // Răspunsurile pot fi reprezentate și ca șir
        public string AnswersOrder { get; set; }

        // 'Text' rămâne ca string
        [BsonElement("text")]
        [BsonRepresentation(BsonType.String)]  // Asigură că este salvat corect ca șir
        public string Text { get; set; }
       
        // Change the Answers property to be a list of Answer objects
        [BsonElement("answers")]
        public List<Answer> Answers { get; set; }
    }
}
