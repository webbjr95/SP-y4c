using SP_Y4C.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP_Y4C.Models
{
    public class SurveyQuestion 
    {
        [Key]
        public string QuestionId { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public QuestionType Type { get; set; }

        public DateTime CreationDate { get; set; }

        public QuestionActiveStatus ActiveStatus { get; set; }
    }
}
