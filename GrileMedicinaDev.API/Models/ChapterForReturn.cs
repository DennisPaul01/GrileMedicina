using System;

namespace GrileMedicinaDev.Models;

public class ChapterForReturn
{
    public bool IsUserContent { get; set; }
    public List<string> Categories { get; set; }
    public string Name { get; set; }
    public string CreatedBy { get; set; }
    public PagesDto Pages { get; set; }
    public bool ExplanationsGenerating { get; set; }
    public int Quantity { get; set; } // Added Quantity property
}
