using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP_Y4C.Data;
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
    }
}
