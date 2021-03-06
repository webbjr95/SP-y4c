﻿using SP_Y4C.Models.Enums;
using System;
using System.Collections.Generic;
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
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Display(Name = "Question Number")]
        public int QuestionNumber { get; set; }

        [Required]
        [Display(Name ="Question Type")]
        public QuestionType TypeId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Text { get; set; }

        [Display(Name = "Created At")]
        public DateTimeOffset CreatedAtUtc { get; set; }

        [Display(Name = "Last Modified At")]
        public DateTimeOffset LastModifiedAtUtc { get; set; }

        [Required]
        [Display(Name = "Status")]
        public QuestionActiveStatus ActiveStatus { get; set; }

        [Required]
        public QuestionWeight Weight { get; set; }

        [Required]
        public QuestionCategory Category { get; set; }

        [Display(Name = "Radio Options")]
        public ICollection<SurveyChoice> Choices { get; set; }
        public ICollection<SurveyAnswer> Answer { get; set; }

        [NotMapped]
        public List<string> RadioOptions { get; set; }
    }
}
