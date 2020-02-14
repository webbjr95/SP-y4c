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
        private readonly Y4CDbContext _dbContext;

        public QuestionsController(Y4CDbContext dbContext)
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
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var questions = await _dbContext.SurveyQuestions.SingleOrDefaultAsync(m => m.QuestionId == id);
            if (questions == null)
            {
                return NotFound();
            }
            return View(questions);
        }

        [HttpPost]
        public IActionResult Edit(SurveyQuestion question)
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
