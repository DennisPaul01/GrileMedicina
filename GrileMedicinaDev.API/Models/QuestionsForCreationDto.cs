using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GrileMedicinaDev.Entities;

namespace GrileMedicinaDev.Models
{
    public class QuestionsForCreationDto
    {
        [Required(ErrorMessage = "ChapterId este obligatoriu.")]
        public string ChapterId { get; set; }

        [Required(ErrorMessage = "Lista Pages este obligatorie.")]
        [MinLength(1, ErrorMessage = "Trebuie să existe cel puțin o pagină.")]
        public List<int> Pages { get; set; }

        public bool ExplanationExists { get; set; }

        [Required(ErrorMessage = "Textul întrebării este obligatoriu.")]
        [MaxLength(1000, ErrorMessage = "Textul întrebării nu poate depăși 1000 de caractere.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Lista Answers este obligatorie.")]
        [MinLength(1, ErrorMessage = "Lista răspunsurilor trebuie să conțină cel puțin un răspuns.")]
        public List<AnswerCreationDto> Answers { get; set; }
    }
}
