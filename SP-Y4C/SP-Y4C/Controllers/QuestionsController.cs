using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP_Y4C.Models;

namespace SP_Y4C.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public QuestionsController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var questions = await _dbContext.Questions.Where(q => !q.IsRetired).ToListAsync();
            var viewModel = new QuestionsViewModel
            {
                Questions = questions
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Question question)
        {
            await _dbContext.AddAsync(question);
            await _dbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Question question)
        {
            return View();
        }
    }
}
