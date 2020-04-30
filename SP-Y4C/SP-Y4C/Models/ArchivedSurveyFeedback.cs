using SP_Y4C.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP_Y4C.Models
{
    public class ArchivedSurveyFeedback
    {
        public ArchivedSurveyFeedback()
        {
            ArchivedAtUtc = DateTime.UtcNow;
        }

        [Key]
        [Display(Name = "ID")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Feedback")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "URL")]
        public string Url { get; set; }

        [Required]
        [Display(Name = "Rating")]
        public int Rating { get; set; }

        [Required]
        [Display(Name = "User ID")]
        public Guid UserId { get; set; }

        [Required]
        [Display(Name = "Archived At")]
        public DateTimeOffset ArchivedAtUtc { get; set; }

        [Required]
        [Display(Name = "User Archived By")]
        public Guid UserArchivedBy { get; set; }
    }
}
