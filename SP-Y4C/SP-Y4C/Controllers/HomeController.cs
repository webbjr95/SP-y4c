using Microsoft.AspNetCore.Mvc;
using SP_Y4C.ViewModels;
using System.Diagnostics;

namespace SP_Y4C.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
