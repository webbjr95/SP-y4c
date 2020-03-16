using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SP_Y4C.Controllers
{
    public class DecisionTreeController : Controller
    {
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
        public IActionResult NotSure()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NextStep(IFormCollection form)
        {
            if (form["page"].Equals("teach"))
            {
                if (form["teachInterest"].Equals("yes"))
                {
                    return Redirect("https://www.y4c.org/contact");
                }
                else
                {
                    return Redirect("https://www.y4c.org/donate");
                }
            }
            else if(form["page"].Equals("notsure"))
            {
                return Redirect("https://www.y4c.org/about");
            }
            else if(form["page"].Equals("practice"))
            {
                if (form["practiceLocal"].Equals("facility"))
                {
                    return Redirect("https://www.y4c.org/facilities");
                }
                else
                {
                    return View("Involvement");
                }
            }
            else
            {
                if (form["volunteerInterest"].Equals("yes"))
                {
                    return Redirect("https://www.y4c.org/volunteer");
                }
                else
                {
                    return Redirect("https://www.y4c.org/donate");
                }
            }
        }
    }
}