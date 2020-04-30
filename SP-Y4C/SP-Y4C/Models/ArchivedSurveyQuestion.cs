using SP_Y4C.Areas.Identity.Data;
using SP_Y4C.Models.Enums;
using System;
using System.Collections.Generic;
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
        public Guid Id { get; set; }

        [Display(Name = "Question Number")]
        public int QuestionNumber { get; set; }

        [Required]
        [Display(Name = "Question Type")]
        public QuestionType TypeId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Text { get; set; }

        [Required]
        [Display(Name = "Archived At")]
        public DateTimeOffset ArchivedAtUtc { get; set; }

        [Required]
        [Display(Name = "Status")]
        public QuestionActiveStatus ActiveStatus { get; set; }

        [Required]
        [Display(Name = "User Archived By")]
        public Guid UserArchivedBy { get; set; }
    }
}
