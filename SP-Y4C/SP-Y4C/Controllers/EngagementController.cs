using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP_Y4C.Data;
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

        public IActionResult TestCollect()
        {
            var questions = _dbContext.SurveyQuestions.Include(c => c.Choices);

            return View(questions);
        }

        [HttpPost]
        public IActionResult Teach()
        {
            var questions = _dbContext.SurveyQuestions.Include(c => c.Choices);

            return View(questions);
        }

        [HttpPost]
        public IActionResult Practice()
        {
            var questions = _dbContext.SurveyQuestions.Include(c => c.Choices);

            return View(questions);
        }

        [HttpPost]
        public IActionResult Other()
        {
            var questions = _dbContext.SurveyQuestions.Include(c => c.Choices);

            return View(questions);
        }

        [HttpPost]
        public IActionResult NextStep(IFormCollection form)
        {
            //Perform the calculation to see what the visitor should be placed as.



            //Need it to follow the below coding logic
            //Choose a starting page from three options
            //teach, practice, and volunteering
            //teach - are you interested ? teacher : donate
            //practice - are you interested ? alumni : donate
            //volunteering - are you interested ? volunteer : donate
            switch (form["page"])
            {
                case "teach":
                    if (form["teachInterest"].Equals("yes")){
                        return RedirectToAction("Survey", "Feedback");
                        //return Redirect("https://www.y4c.org/teacher-page");
                    }
                    else{
                        return Redirect("https://www.y4c.org/donate");
                    }
                case "practice":
                    if (form["gettingInvolved"].Equals("yes")){
                        return Redirect("https://www.y4c.org/new-page-4");
                    }else{
                        return Redirect("https://www.y4c.org/donate");
                    }
                case "other": 
                    if (form["gettingInvolved"].Equals("yes")){
                        return RedirectToAction("Survey", "Feedback");
                        //return Redirect("https://www.y4c.org/volunteer-1");
                    }else{
                        return Redirect("https://www.y4c.org/donate");
                    }
                default:
                    return Redirect("https://www.y4c.org/donate");
            }
        }
    }
}