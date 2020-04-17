using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SP_Y4C.Controllers
{
    [Authorize]
    public class AdminConsoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}