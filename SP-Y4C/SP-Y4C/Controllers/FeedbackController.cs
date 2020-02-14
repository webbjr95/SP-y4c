using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SP_Y4C.Controllers
{
    public class FeedbackController : Controller
    {
        public IActionResult Survey()
        {
            return View();
        }
    }
}