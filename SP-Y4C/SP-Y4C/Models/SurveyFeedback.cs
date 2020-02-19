using SP_Y4C.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SP_Y4C.Models
{
    public class SurveyFeedback
    {
        public SurveyFeedback()
        {
            SubmittedAtUtc = DateTime.UtcNow;
        }

        [Key]
        [Display(Name = "ID")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "URL")]
        public string Url { get; set; }

        [Required]
        [Display(Name = "Rating")]
        public int Rating { get; set; }

        [Required]
        [Display(Name = "User ID")]
        public Guid UserId { get; set; }

        [Display(Name = "Submission Date")]
        public DateTime SubmittedAtUtc { get; set; }
    }
}
