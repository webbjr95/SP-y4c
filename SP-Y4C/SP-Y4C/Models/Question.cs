using SP_Y4C.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP_Y4C.Models
{
    public class Question 
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Question Text")]
        public string Text { get; set; }

        public int QuestionNumber { get; set; }

        [Column("QuestionTypeId")]
        public QuestionType QuestionType { get; set; }

        public DateTimeOffset CreatedAtUtc { get; set; }

        public DateTimeOffset LastModifiedAtUtc { get; set; }

        public bool IsRetired { get; set; }
    }
}
