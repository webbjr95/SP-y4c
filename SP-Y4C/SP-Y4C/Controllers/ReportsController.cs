using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP_Y4C.Data;

namespace SP_Y4C.Controllers
{
    public class ReportsController : Controller
    {
        private readonly Y4CDbContext _dbContext;

        public ReportsController(Y4CDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: GenerateReport
        public ActionResult Index()
        {
            var answers = _dbContext.SurveyAnswers.Include(q => q.Question);
            return View(answers);
        }
        public ActionResult Feedback()
        {
            var feedback = _dbContext.SurveyFeedback;
            return View(feedback);
        }
    }
}