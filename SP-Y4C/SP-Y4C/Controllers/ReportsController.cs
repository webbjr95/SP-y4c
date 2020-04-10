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
        public async Task<ActionResult> Index()
        {
<<<<<<< Updated upstream
            return View(await _dbContext.SurveyAnswers.ToListAsync());
=======
            var answers = _dbContext.SurveyAnswers.Include(q => q.Question);
            return View(answers);

>>>>>>> Stashed changes
        }

        // GET: GenerateReport/Create
        public ActionResult Generate()
        {
            return View();
        }

        //// POST: GenerateReport/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}