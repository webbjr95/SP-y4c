using SP_Y4C.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP_Y4C.Models
{
    public class SurveyQuestion 
    {
        public SurveyQuestion()
        {
            CreatedAtUtc = DateTime.UtcNow;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        [Key]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Question Number")]
        public int QuestionNumber { get; set; }

        [Required]
        [Display(Name ="Question Type")]
        [Column("QuestionTypeId")]
        public QuestionType QuestionType { get; set; }

        [Required]
        [MaxLength(500)]
        public string Text { get; set; }

        [Display(Name = "Created At")]
        public DateTimeOffset CreatedAtUtc { get; set; }

        [Display(Name = "Last Modified At")]
        public DateTimeOffset LastModifiedAtUtc { get; set; }
    }
}
