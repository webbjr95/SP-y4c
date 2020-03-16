using SP_Y4C.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SP_Y4C.ViewModels
{
    public class SurveyEngagementViewModel
    {
        public IEnumerable<SurveyQuestion> Question { get; set; }
        public IEnumerable<SurveyChoice> Choice { get; set; }
    }
}
