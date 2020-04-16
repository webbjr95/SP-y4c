using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP_Y4C.Data;
using SP_Y4C.Models;

namespace SP_Y4C.Controllers
{
    public class ReportsController : Controller
    {
        private readonly Y4CDbContext _dbContext;

        public ReportsController(Y4CDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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

        public ActionResult FeedbackArchived()
        {
            var archived = _dbContext.ArchivedSurveyFeedback;
            return View(archived);
        }

        public async Task<IActionResult> Archive()
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                var allFeedback = await _dbContext.SurveyFeedback.ToListAsync();
                var toBeArchivedFeedback = new List<ArchivedSurveyFeedback>();
                foreach (var feedback in allFeedback)
                {
                    var newRecord = new ArchivedSurveyFeedback
                    {
                        UserId = feedback.UserId,
                        Id = feedback.Id,
                        Rating = feedback.Rating,
                        Text = feedback.Text,
                        Url = feedback.Url,
                        UserArchivedBy = Guid.NewGuid(),
                        //TODO: Add this back in once we have the login portion incorporated. Need to get the user ID.
                        //UserArchivedBy = User.Identity.Name,
                        ArchivedAtUtc = DateTime.UtcNow,
                    };

                    toBeArchivedFeedback.Add(newRecord);
                }


                // Update both database tables
                await _dbContext.ArchivedSurveyFeedback.AddRangeAsync(toBeArchivedFeedback);
                await _dbContext.SaveChangesAsync();

                _dbContext.SurveyFeedback.RemoveRange(allFeedback);
                await _dbContext.SaveChangesAsync();

                transaction.Commit();
            }

            return RedirectToAction("Feedback");
        }
    }
}