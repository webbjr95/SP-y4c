using SP_Y4C.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP_Y4C.Models
{
    public class ArchivedSurveyQuestion 
    {
        public ArchivedSurveyQuestion()
        {
            ArchivedAtUtc = DateTime.UtcNow;
        }

        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Question Number")]
        public int QuestionNumber { get; set; }

        [Display(Name = "Question Type")]
        [Column("QuestionTypeId")]
        public QuestionType QuestionType { get; set; }

        [MaxLength(500)]
        public string Text { get; set; }

        [Display(Name = "Archived At")]
        public DateTimeOffset ArchivedAtUtc { get; set; }
    }
}
