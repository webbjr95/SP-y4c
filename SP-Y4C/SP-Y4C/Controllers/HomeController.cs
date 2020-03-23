using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP_Y4C.Data;
using SP_Y4C.ViewModels;
using System.Diagnostics;

namespace SP_Y4C.Controllers
{
    public class HomeController : Controller
    {
        private readonly Y4CDbContext _dbContext;

        public HomeController(Y4CDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult dt()
        {
            var questions = _dbContext.SurveyQuestions.Include(c => c.Choices);

            return View(questions);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
