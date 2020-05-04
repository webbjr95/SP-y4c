using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        //httpContext-REquest-Headers-HeaderReferer
        public IActionResult Survey()
        {
            //TODO: Change the SquareSpace refer to be no-referrer-when-downgrade but will need to update our
            // application to be hosted as a HTTPS site
            var refererUrl = Microsoft.Extensions.Primitives.StringValues.Empty;
            HttpContext.Request.Headers.TryGetValue("Referer", out refererUrl);
            ViewBag.RefererUrl = refererUrl;

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

            if (ModelState.IsValid)
            {
                await _dbContext.AddAsync(feedback);
                await _dbContext.SaveChangesAsync();
            }

            return View("Completed");
        }

        public IActionResult Completed()
        {
            return View();
        }
    }
}