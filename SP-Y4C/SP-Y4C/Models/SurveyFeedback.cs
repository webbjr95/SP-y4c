using SP_Y4C.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SP_Y4C.Models
{
    public class SurveyFeedback
    {
        public SurveyFeedback()
        {
            SubmissionDate = DateTime.UtcNow;
        }

        [Key]
        [Display(Name = "ID")]
        public string FeedbackId { get; set; }

        [Required]
        [Display(Name = "URL")]
        public string Url { get; set; }

        [Required]
        [Display(Name = "Rating")]
        public int Rating { get; set; }

        [Required]
        [Display(Name = "User Type")]
        public UserType UserType { get; set; }

        [Display(Name = "Submission Date")]
        public DateTime SubmissionDate { get; set; }
    }
}
