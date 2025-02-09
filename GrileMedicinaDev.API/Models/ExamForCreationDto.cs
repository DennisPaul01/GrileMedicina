using System.Collections.Generic;

namespace GrileMedicinaDev.Models
{
    public class ExamForCreationDto
    {
        public string ExamType { get; set; }
        public List<ChapterDto> Chapters { get; set; }
        public bool RandomizeQuestions { get; set; }
        public bool RandomizeAnswers { get; set; }
        public int Quantity { get; set; }
        public bool Timed { get; set; }
        public int Timer { get; set; }
        public bool ShowAnswers { get; set; }
    }

    public class ChapterDto
    {
        public string _id { get; set; }
    }
}
