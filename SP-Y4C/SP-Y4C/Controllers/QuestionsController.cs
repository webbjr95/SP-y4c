using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP_Y4C.Data;
using SP_Y4C.Models;
using SP_Y4C.Models.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public async Task<IActionResult> Create(SurveyQuestion question)
        {
            if (!ModelState.IsValid)
            {
                return View(question);
            }

            await _dbContext.SurveyQuestions.AddAsync(question);
            await _dbContext.SaveChangesAsync();


            // Makes sure we don't experience an error message when checking if there are any choices.
            if (question.RadioOptions != null && question.TypeId != QuestionType.Text)
            {
                var choices = new List<SurveyChoice>();

                for (var i = 0; i < question.RadioOptions.Count; i++)
                {
                    // Since the choice(s) aren't valid without the text field and would cause an error we need to check
                    if (question.RadioOptions[i] != null)
                    {
                        var choice = new SurveyChoice
                        {
                            Id = Guid.NewGuid(),
                            QuestionId = question.Id,
                            Text = question.RadioOptions[i],
                            OrderInQuestion = i
                        };

                        choices.Add(choice);
                    }
                }

                await _dbContext.SurveyChoices.AddRangeAsync(choices);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var questionWithChoices = await _dbContext.SurveyQuestions.FirstOrDefaultAsync(q => q.Id == id);
            
            if (questionWithChoices == null)
            {
                return NotFound();
            }


            //Add in the choices for the questions. Sort them by ASC order.
            var radioOptions = await _dbContext.SurveyChoices.Where(c => c.QuestionId == questionWithChoices.Id).OrderBy(n => n.OrderInQuestion).Select(t => t.Text).ToListAsync();
            questionWithChoices.RadioOptions = radioOptions;
            
            return View(questionWithChoices);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SurveyQuestion question)
        {
            if (!ModelState.IsValid)
            {
                return View(question);
            }

            var existingQuestion = await _dbContext.SurveyQuestions.FirstOrDefaultAsync(q => q.Id == question.Id);

            if (existingQuestion.TypeId == QuestionType.Radio || existingQuestion.TypeId == QuestionType.Checkbox)
            {
                var existingChoices = await _dbContext.SurveyChoices.Where(c => c.QuestionId == existingQuestion.Id).ToListAsync();

                _dbContext.SurveyChoices.RemoveRange(existingChoices);
                await _dbContext.SaveChangesAsync();
            }

            existingQuestion.Text = question.Text;
            existingQuestion.QuestionNumber = question.QuestionNumber;
            existingQuestion.TypeId = question.TypeId;
            existingQuestion.LastModifiedAtUtc = DateTime.UtcNow;
            existingQuestion.Category = question.Category;

            _dbContext.SurveyQuestions.Update(existingQuestion);
            await _dbContext.SaveChangesAsync();


            // Makes sure we don't experience an error message when checking if there are any choices.
            if (question.RadioOptions != null && question.TypeId != QuestionType.Text)
            {
                var newChoices = new List<SurveyChoice>();

                for (var i = 0; i < question.RadioOptions.Count; i++)
                {
                    // Since the choice(s) aren't valid without the text field and would cause an error we need to check
                    if (question.RadioOptions[i] != null) 
                    { 
                        var choice = new SurveyChoice
                        {
                            Id = Guid.NewGuid(),
                            QuestionId = question.Id,
                            Text = question.RadioOptions[i],
                            OrderInQuestion = i
                        };

                        newChoices.Add(choice);
                    }
                }

                await _dbContext.SurveyChoices.AddRangeAsync(newChoices);
                await _dbContext.SaveChangesAsync();
            }

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
