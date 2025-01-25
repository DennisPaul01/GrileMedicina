using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GrileMedicinaDev.Entities
{
    public class CategoryStatistics
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public List<CategoryQuantity> QuestionsCountPerCategory { get; set; }
        public int UsersJoined { get; set; }
        public int QuestionsCountPlatform { get; set; }
        public int ExamsStarted { get; set; }
        public int OrdersCompleted { get; set; }
        public DateTime ExamDate { get; set; }
        public List<CategoryQuantity> CardsCountPerCategory { get; set; }
        public int CardsCountPlatform { get; set; }
    }

    public class CategoryQuantity
    {
        public string Category { get; set; }
        public int Quantity { get; set; }
    }
}
