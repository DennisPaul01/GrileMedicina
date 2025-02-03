using System;

namespace GrileMedicinaDev.Models;

public class QuestionForUpdate
{
    public string? ChapterId { get; set; }

    public List<int>? Pages { get; set; }

    public bool? ExplanationExists { get; set; }

    public string? Text { get; set; }

    public List<AnswerCreationDto>? Answers { get; set; }
}
