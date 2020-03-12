using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP_Y4C.Data;
using SP_Y4C.Models;
using SP_Y4C.Models.Enums;
using SP_Y4C.ViewModels;

namespace SP_Y4C.Controllers
{
    public class EngagementController : Controller
    {
        private readonly Y4CDbContext _dbContext;

        public EngagementController(Y4CDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Teach()
        {
            var questions = _dbContext.SurveyQuestions.Include(c => c.Choices).OrderBy(q => q.QuestionNumber);

            return View(questions);
        }

        [HttpPost]
        public IActionResult Practice()
        {
            var questions = _dbContext.SurveyQuestions.Include(c => c.Choices).OrderBy(q => q.QuestionNumber);

            return View(questions);
        }

        [HttpPost]
        public IActionResult Other()
        {
            var questions = _dbContext.SurveyQuestions.Include(c => c.Choices).OrderBy(q => q.QuestionNumber);

            return View(questions);
        }

        [HttpPost]
        public async Task<IActionResult> Submit(IFormCollection form)
        {
            //Start at 1 so the question weight array is ignored, second to last is the hidden page value, and
            // skip final value as it's an HTTP token.
            var formAsList = form.ToList();
            for(var s = 1; s < form.Count - 1; s++)
            {
               var subAnswer = new SurveyAnswer
                {
                    //TODO: Change this to be apart of the calculation
                    Id = Guid.NewGuid(),
                    QuestionId = new Guid(formAsList.ElementAt(s).Key),
                    Answer = formAsList.ElementAt(s).Value,
                    UserId = Guid.NewGuid(),
                    UserType = UserType.Alumni
                };

                //Validate the model being created before adding the answers to the DB
                if(ModelState.IsValid)
                {
                    await _dbContext.SurveyAnswers.AddAsync(subAnswer);
                    await _dbContext.SaveChangesAsync();
                }
            }

            //Perform the calculation to see what the visitor should be placed as.
            var urlForVisitor = CalculateWeightToUrl(form);

            return Redirect(urlForVisitor);
        }


        //Calculation for when a visitor answers the survey. This will take into account
        public string CalculateWeightToUrl(IFormCollection passedForm)
        {
            var url = _dbContext.UrlToVisitors
            return "https://www.y4c.org/teacher-page";
            //DECISION: Pass in the form and then parse the keys? Will know the current category off of the hidden form value,
            //  and the remaining values for each question. 

            /*
             * Current form keys:
             * - hidden value for the current page
             * - questions and their choices
             */

            //TODO: Need to work on the naming and values within the foreach loops since those are currently all using the ID
            //  and is difficult to parse/understand from an admin's perspective.

            //TODO: Create a table to house the visitor types and the associated URLs. Allow management of them here.
            //Question has the weight and category available. UrlToVistor will have the matching category and url. 
            // Perform an include on this to have them associate and then display the relevant information.


            //Need the logic to look something like this


            //Need it to follow the below coding logic
            //Choose a starting page from three options
            //teach, practice, and volunteering
            //teach - are you interested ? teacher : donate
            //practice - are you interested ? alumni : donate
            //volunteering - are you interested ? volunteer : donate
            //switch (form["page"])
            //{
            //    case "teach":
            //        if (form["teachInterest"].Equals("yes"))
            //        {
            //            return RedirectToAction("Survey", "Feedback");
            //            return Redirect("https://www.y4c.org/teacher-page");
            //        }
            //        else
            //        {
            //            return Redirect("https://www.y4c.org/donate");
            //        }
            //    case "practice":
            //        if (form["gettingInvolved"].Equals("yes"))
            //        {
            //            return Redirect("https://www.y4c.org/new-page-4");
            //        }
            //        else
            //        {
            //            return Redirect("https://www.y4c.org/donate");
            //        }
            //    case "other":
            //        if (form["gettingInvolved"].Equals("yes"))
            //        {
            //            return RedirectToAction("Survey", "Feedback");
            //            return Redirect("https://www.y4c.org/volunteer-1");
            //        }
            //        else
            //        {
            //            return Redirect("https://www.y4c.org/donate");
            //        }
            //    default:
            //        return Redirect("https://www.y4c.org/donate");
            //}

        }
    }
}