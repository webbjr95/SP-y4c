using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SP_Y4C.Models
{
    public class QuestionsTable 
    {
        [Required]
        [Key]
        [Display(Name = "Question Text")]
        public int QuestionID { get; set; }
        public string Text { get; set; }
        //public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDateTime { get; set; }
        public Boolean Status { get; set; }
    }
}
