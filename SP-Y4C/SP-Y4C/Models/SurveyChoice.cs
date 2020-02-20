using SP_Y4C.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SP_Y4C.Models
{
    public class SurveyChoice 
    {
        public SurveyChoice()
        {
            CreatedAtUtc = DateTime.UtcNow;
            LastModifiedAtUtc = DateTime.UtcNow;
        }

        [Key]
        [Display(Name = "ID")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name ="Question ID")]
        public Guid QuestionId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Text { get; set; }

        [Display(Name = "Created At")]
        public DateTimeOffset CreatedAtUtc { get; set; }

        [Display(Name = "Last Modified At")]
        public DateTimeOffset LastModifiedAtUtc { get; set; } 
        
        [Display(Name = "Order Within Question")]
        public int OrderInQuestion { get; set; }
    }
}
