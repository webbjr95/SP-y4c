using SP_Y4C.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP_Y4C.Models
{
    public class SurveyFeedback
    {
        [Key]
        public string FeedbackId { get; set; }

        public string Url { get; set; }

        public int Rating { get; set; }

        public UserType UserType { get; set; }

        public DateTime SubmissionDate { get; set; }
    }
}
