using System.Collections.Generic;

namespace GrileMedicinaDev.Models
{
    public class ChapterForCreationDto
    {
        public bool IsUserContent { get; set; }
        public List<string> Categories { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public PagesDto Pages { get; set; }
        public bool ExplanationsGenerating { get; set; }
    }
}
