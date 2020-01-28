using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SP_Y4C.Models
{
    public class QuestionsTable 
    {

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Question Text")]
        public string QuestionText { get; set; }
        public string QuestionID { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionDescription { get; set; }
        public string QuestionCreationDateTime { get; set; }
        public string QuestionStatus { get; set; }
        public string DemographicAge { get; set; }
        public string DemographicRace { get; set; }
        public string DemographicIncomeRange { get; set; }
        public string DemographicSex { get; set; }
    }
}