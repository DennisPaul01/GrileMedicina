using System;
using System.Collections.Generic;

namespace GrileMedicinaDev.Models
{
    public class CategoryStatisticsDto
    {
        public List<CategoryQuantityDto> QuestionsCountPerCategory { get; set; }
        public int UsersJoined { get; set; }
        public int QuestionsCountPlatform { get; set; }
        public int ExamsStarted { get; set; }
        public int OrdersCompleted { get; set; }
        public DateTime ExamDate { get; set; }
        public List<CategoryQuantityDto> CardsCountPerCategory { get; set; }
        public int CardsCountPlatform { get; set; }
    }

    public class CategoryQuantityDto
    {
        public string Category { get; set; }
        public int Quantity { get; set; }
    }
}
