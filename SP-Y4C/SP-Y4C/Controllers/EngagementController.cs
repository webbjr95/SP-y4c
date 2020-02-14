using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SP_Y4C.Controllers
{
    public class EngagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Teach()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Practice()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Other()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NextStep(IFormCollection form)
        {
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