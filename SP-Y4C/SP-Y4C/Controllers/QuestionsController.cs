using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP_Y4C.Data;
using SP_Y4C.Models;
using SP_Y4C.Models.Enums;
using SP_Y4C.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

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
        public IActionResult Index()
        {
            var questions = _dbContext.SurveyQuestions.Include(c => c.Choices).OrderBy(q => q.QuestionNumber);

            return View(questions);
        }

        public async Task<IActionResult> Archived()
        {
            var questions = await _dbContext.ArchivedSurveyQuestions.ToListAsync();

            return View(questions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateQuestionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var question = new SurveyQuestion
            {
                QuestionNumber = viewModel.QuestionNumber,
                TypeId = viewModel.QuestionType,
                Text = viewModel.Text,
                ActiveStatus = QuestionActiveStatus.Inactive,
                Weight = viewModel.Weight,
                Category = viewModel.Category
            };

            using (var trans = new TransactionScope())
            {
                var choices = new List<SurveyChoice>();

                for (var i = 0; i < viewModel.RadioOptions.Count; i++)
                {
                    var choice = new SurveyChoice
                    {
                        QuestionId = question.Id,
                        Text = viewModel.RadioOptions[i],
                        OrderInQuestion = i
                    };

                    choices.Add(choice);
                }

                await _dbContext.SurveyQuestions.AddAsync(question);
                await _dbContext.SaveChangesAsync();

                await _dbContext.SurveyChoices.AddRangeAsync(choices);
                await _dbContext.SaveChangesAsync();

                trans.Complete();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var question = await _dbContext.SurveyQuestions.FirstOrDefaultAsync(q => q.Id == id);
            
            if (question == null)
            {
                return NotFound();
            }
            
            return View(question);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SurveyQuestion question)
        {
            if (!ModelState.IsValid)
            {
                return View(question);
            }

            var existingQuestion = await _dbContext.SurveyQuestions.FirstOrDefaultAsync(q => q.Id == question.Id);

            existingQuestion.Text = question.Text;
            existingQuestion.QuestionNumber = question.QuestionNumber;
            existingQuestion.TypeId = question.TypeId;
            existingQuestion.LastModifiedAtUtc = DateTime.UtcNow;

            _dbContext.SurveyQuestions.Update(existingQuestion);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Archive(Guid id)
        {
            var question = await _dbContext.SurveyQuestions.FirstOrDefaultAsync(q => q.Id == id);

            if (question == null)
            {
                return NotFound();
            }

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var archivedQuestion = new ArchivedSurveyQuestion
                {
                    Id = question.Id,
                    QuestionNumber = question.QuestionNumber,
                    TypeId = question.TypeId,
                    Text = question.Text,
                    UserArchivedBy = Guid.NewGuid(),
                    //TODO: Add this back in once we have the login portion incorporated. Need to get the user ID.
                    //UserArchivedBy = User.Identity.Name,
                    ArchivedAtUtc = DateTime.UtcNow,
                    ActiveStatus = question.ActiveStatus
                };

                await _dbContext.ArchivedSurveyQuestions.AddAsync(archivedQuestion);
                await _dbContext.SaveChangesAsync();

                _dbContext.SurveyQuestions.Remove(question);
                await _dbContext.SaveChangesAsync();

                transaction.Commit();
            }

            return RedirectToAction("Index");
        }
    }
}
