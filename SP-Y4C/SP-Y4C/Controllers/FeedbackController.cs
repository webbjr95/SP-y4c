using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SP_Y4C.Data;
using SP_Y4C.Models;

namespace SP_Y4C.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly Y4CDbContext _dbContext;
        public FeedbackController(Y4CDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Survey()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Survey(IFormCollection form)
        {
            var feedback = new SurveyFeedback
            {
                Id = Guid.NewGuid(),
                Url = form["currentUrl"],
                Text = form["text"],
                Rating = int.Parse(form["rating"]),
                UserId = Guid.NewGuid(),
                SubmittedAtUtc = DateTime.UtcNow
            };

            await _dbContext.AddAsync(feedback);
            await _dbContext.SaveChangesAsync();

            return View();
        }
    }
}