using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP_Y4C.Areas.Identity.Data;
using SP_Y4C.Data;
using SP_Y4C.Models;

namespace SP_Y4C.Controllers
{
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly Y4CDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReportsController(Y4CDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
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
                ApplicationUser applicationUser = await _userManager.GetUserAsync(User);
                foreach (var feedback in allFeedback)
                {
                    var newRecord = new ArchivedSurveyFeedback
                    {
                        UserId = feedback.UserId,
                        Id = feedback.Id,
                        Rating = feedback.Rating,
                        Text = feedback.Text,
                        Url = feedback.Url,
                        UserArchivedBy = Guid.Parse(applicationUser.Id),
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

        [HttpPost]
        public async Task<ActionResult> Archive(Guid id)
        {
            var feedback = await _dbContext.SurveyFeedback.FindAsync(id);
            
            if(feedback == null)
            {
                return NotFound();
            }

            ApplicationUser applicationUser = await _userManager.GetUserAsync(User);
            var archivedFeedback = new ArchivedSurveyFeedback
            {
                UserId = feedback.UserId,
                Id = feedback.Id,
                Rating = feedback.Rating,
                Text = feedback.Text,
                Url = feedback.Url,
                UserArchivedBy = Guid.Parse(applicationUser.Id),
                ArchivedAtUtc = DateTime.UtcNow,
            };

            
            _dbContext.SurveyFeedback.Remove(feedback);
            await _dbContext.SaveChangesAsync();

            await _dbContext.ArchivedSurveyFeedback.AddAsync(archivedFeedback);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Feedback");
        }
    }
}