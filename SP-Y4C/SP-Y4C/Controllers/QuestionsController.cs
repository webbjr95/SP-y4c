using System;
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
            var questions = await _dbContext.Questions.OrderBy(q => q.QuestionNumber).ToListAsync();
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
            if (!ModelState.IsValid)
            {
                return View(question);
            }

            await _dbContext.Questions.AddAsync(question);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var question = await _dbContext.Questions.FirstOrDefaultAsync(q => q.Id == id);

            return View(question);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Question question)
        {
            if (!ModelState.IsValid)
            {
                return View(question);
            }

            var existingQuestion = await _dbContext.Questions.FirstOrDefaultAsync(q => q.Id == question.Id);

            existingQuestion.Text = question.Text;
            existingQuestion.QuestionNumber = question.QuestionNumber;
            existingQuestion.QuestionType = question.QuestionType;
            existingQuestion.LastModifiedAtUtc = DateTime.UtcNow;
            existingQuestion.IsRetired = question.IsRetired;

            _dbContext.Questions.Update(existingQuestion);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var question = await _dbContext.Questions.FirstOrDefaultAsync(q => q.Id == id);

            _dbContext.Questions.Remove(question);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
