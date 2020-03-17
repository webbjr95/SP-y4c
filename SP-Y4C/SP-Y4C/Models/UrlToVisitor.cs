using SP_Y4C.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace SP_Y4C.Models
{
    public class UrlToVisitor 
    {

        [Key]
        [Display(Name = "ID")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Visitor Type")]
        public UserType UserType { get; set; }

        [Required]
        [MaxLength(500)]
        public string Url { get; set; }

        [Required]
        public SurveyBranch Category { get; set; }

        [Required]
        public int Weight { get; set; }

        [Display(Name = "Created At")]
        public DateTimeOffset CreatedAtUtc { get; set; }

        [Display(Name = "Last Modified At")]
        public DateTimeOffset LastModifiedAtUtc { get; set; }
    }
}
