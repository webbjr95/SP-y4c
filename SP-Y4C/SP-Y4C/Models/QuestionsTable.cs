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
        [DataType(DataType.Text)]
        [Display(Name = "Question Text")]
        public string text { get; set; }

        [Required]
        [Key]
        [DataType(DataType.Text)]
        [Display(Name = "Question ID")]
        public int questionID { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Parent ID")]
        public string parentID { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Children IDs")]
        public string childIDs { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Question Type")]
        public string questionType { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Current Status")]
        public Boolean status { get; set; }
    }
}
