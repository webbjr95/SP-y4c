using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP_Y4C.Models;
using SP_Y4C.Models.Enums;
using SP_Y4C.Data;

namespace SP_Y4C.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly QuestionsDbContext _dbContext;

        public QuestionsController(QuestionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.SurveyQuestions.Where(q => q.ActiveStatus.Equals(QuestionActiveStatus.Active)).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Inactive()
        {
            return View(await _dbContext.SurveyQuestions.Where(q => q.ActiveStatus.Equals(QuestionActiveStatus.Inactive)).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Archived()
        {
            return View(await _dbContext.SurveyQuestions.Where(q => q.ActiveStatus.Equals(QuestionActiveStatus.Archived)).ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SurveyQuestion question)
        {
            await _dbContext.AddAsync(question);
            await _dbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SurveyQuestion question)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Archive(SurveyQuestion question)
        {
            return View();
        }
    }
}
