﻿using Microsoft.AspNetCore.Mvc;
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

            var newQuestion = new SurveyQuestion
            {
                QuestionNumber = question.QuestionNumber,
                TypeId = question.TypeId,
                Text = question.Text,
                ActiveStatus = QuestionActiveStatus.Inactive,
                Weight = question.Weight,
                Category = question.Category
            };

            var choices = new List<SurveyChoice>();

            for (var i = 0; i < question.RadioOptions.Count; i++)
            {
                var choice = new SurveyChoice
                {
                    QuestionId = question.Id,
                    Text = question.RadioOptions[i],
                    OrderInQuestion = i
                };

                choices.Add(choice);
            }

            await _dbContext.SurveyQuestions.AddAsync(question);
            await _dbContext.SaveChangesAsync();

            await _dbContext.SurveyChoices.AddRangeAsync(choices);
            await _dbContext.SaveChangesAsync();

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

            var radioOptions = await _dbContext.SurveyChoices.Where(c => c.QuestionId == question.Id).ToListAsync();

            var questionWithChoices = new SurveyQuestion
            {
                Id = question.Id,
                QuestionNumber = question.QuestionNumber,
                TypeId = question.TypeId,
                Text = question.Text,
                RadioOptions = radioOptions.Select(o => o.Text).ToList(),
                Weight = question.Weight,
                Category = question.Category
            };
            
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

        //[HttpPost]
        //public async Task<IActionResult> Edit(SurveyQuestion question)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(question);
        //    }

        //    var existingQuestion = await _dbContext.SurveyQuestions.FirstOrDefaultAsync(q => q.Id == question.Id);
        //    //var existingChoices = new List<SurveyChoice>();
        //    if (existingQuestion.TypeId == QuestionType.Radio || existingQuestion.TypeId == QuestionType.Checkbox)
        //    {
        //        var existingChoices = await _dbContext.SurveyChoices.Where(c => c.QuestionId == existingQuestion.Id).ToListAsync();

        //        //Deletes the existing choices before recreating them later on.
        //        _dbContext.SurveyChoices.RemoveRange(existingChoices);
        //        await _dbContext.SaveChangesAsync();
        //    }

        //    //Update the existing question fields if they are changed within the form.
        //    existingQuestion.Text = question.Text;
        //    existingQuestion.QuestionNumber = question.QuestionNumber;
        //    existingQuestion.TypeId = question.TypeId;
        //    existingQuestion.LastModifiedAtUtc = DateTime.UtcNow;
        //    existingQuestion.Category = question.Category;

        //    _dbContext.SurveyQuestions.Update(existingQuestion);
        //    await _dbContext.SaveChangesAsync();


        //    //Check to see if there are any choices for checkboxes or radio buttons.
        //    if ((question.RadioOptions.Count > 0)  && 
        //        (question.TypeId == QuestionType.Radio || question.TypeId == QuestionType.Checkbox))
        //    {
        //        var newChoices = new List<SurveyChoice>();

        //        for (var i = 0; i < question.RadioOptions.Count; i++)
        //        {
        //            var choice = new SurveyChoice
        //            {
        //                Id = Guid.NewGuid(),
        //                QuestionId = question.Id,
        //                Text = question.RadioOptions[i],
        //                OrderInQuestion = i++
        //            };

        //            newChoices.Add(choice);

        //            ////Check if the new choice text is found within the existing choices.
        //            //if (IsChoiceNew(existingChoices, choice))
        //            //{
        //            //    newChoices.Add(choice);
        //            //}
        //        }

        //        await _dbContext.SurveyChoices.AddRangeAsync(newChoices);
        //        await _dbContext.SaveChangesAsync();
        //    }

        //    return RedirectToAction("Index");
        //}

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

        private bool IsChoiceNew(List<SurveyChoice> choices, SurveyChoice choice)
        {
            for (int i = 0; i < choices.Count; i++)
            {
                if (choices[i].Text == choice.Text)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
