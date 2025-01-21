using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GrileMedicinaDev.Models
{
    public class QuestionsForCreationDto
    {
        [Required(ErrorMessage = "ChapterId este obligatoriu.")]
        public string ChapterId { get; set; } // ID-ul capitolului

        [Required(ErrorMessage = "Lista Pages este obligatorie.")]
        [MinLength(1, ErrorMessage = "Trebuie să existe cel puțin o pagină.")]
        public List<int> Pages { get; set; } // Lista paginilor

        [Required(ErrorMessage = "Trebuie să existe răspunsuri corecte.")]
        [MinLength(1, ErrorMessage = "Lista de răspunsuri corecte trebuie să aibă cel puțin un element.")]
        public List<string> CorrectAnswers { get; set; } // Lista răspunsurilor corecte

        [Range(1, int.MaxValue, ErrorMessage = "AnswersCount trebuie să fie cel puțin 1.")]
        public int AnswersCount { get; set; } // Numărul total de răspunsuri

        [Range(0, int.MaxValue, ErrorMessage = "CorrectAnswersCount nu poate fi negativ.")]
        public int CorrectAnswersCount { get; set; } // Numărul răspunsurilor corecte

        public bool ExplanationExists { get; set; } // Indică dacă există o explicație

        [Required(ErrorMessage = "QuestionIndex este obligatoriu.")]
        [Range(1, int.MaxValue, ErrorMessage = "QuestionIndex trebuie să fie un număr pozitiv.")]
        public int QuestionIndex { get; set; } // Indexul întrebării

        [Required(ErrorMessage = "AnswersOrder este obligatoriu.")]
        [MaxLength(50, ErrorMessage = "AnswersOrder nu poate depăși 50 de caractere.")]
        public string AnswersOrder { get; set; } // Ordinea răspunsurilor

        [Required(ErrorMessage = "Textul întrebării este obligatoriu.")]
        [MaxLength(1000, ErrorMessage = "Textul întrebării nu poate depăși 1000 de caractere.")]
        public string Text { get; set; } // Textul întrebării

        [Required(ErrorMessage = "Lista Answers este obligatorie.")]
        [MinLength(1, ErrorMessage = "Lista răspunsurilor trebuie să conțină cel puțin un răspuns.")]
        public List<string> Answers { get; set; } // Lista tuturor răspunsurilor disponibile
    }
}
