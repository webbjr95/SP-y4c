using SP_Y4C.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP_Y4C.Models
{
    public class SurveyAnswers 
    {
        [Key]
        public string AnswerId { get; set; }

        public string UserId { get; set; }

        public string QuestionId { get; set; }

        public string Answer { get; set; }

        public UserType UserType { get; set; }

        public DateTime SubmissionDate { get; set; }
    }
}
