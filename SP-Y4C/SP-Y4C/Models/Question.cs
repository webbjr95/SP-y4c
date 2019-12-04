using SP_Y4C.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP_Y4C.Models
{
    public class Question 
    {
        public Question()
        {
            CreatedAtUtc = DateTime.UtcNow;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }

        [Display(Name = "Question Text")]
        public string Text { get; set; }

        [Display(Name = "Question Number")]
        public int QuestionNumber { get; set; }

        [Column("QuestionTypeId")]
        [Display(Name = "Question Type")]
        public QuestionType QuestionType { get; set; }

        [Display(Name = "Created At")]
        public DateTimeOffset CreatedAtUtc { get; set; }

        [Display(Name = "Last Modified At")]
        public DateTimeOffset LastModifiedAtUtc { get; set; }
        
        [Display(Name = "Retired?")]
        public bool IsRetired { get; set; }
    }
}
