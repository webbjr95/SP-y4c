using SP_Y4C.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SP_Y4C.Models
{
    public class SurveyAnswer
    {
        public SurveyAnswer()
        {
            SubmittedAtUtc = DateTime.UtcNow;
        }

        [Key]
        [Display(Name = "ID")]
        public Guid Id { get; set; }
        
        [Display(Name = "Question ID")]
        public Guid QuestionId { get; set; }

        public string Answer { get; set; }
        
        [Display(Name = "User ID")]
        public Guid UserId { get; set; }

        [Display(Name = "User Type")]
        [Column("UserTypeId")]
        public UserType UserType { get; set; }

        [Display(Name = "Submitted At")]
        public DateTimeOffset SubmittedAtUtc { get; set; }

        public SurveyQuestion Question { get; set; }
    }
}
