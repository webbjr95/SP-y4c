using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP_Y4C.Data;
using SP_Y4C.Models;
using SP_Y4C.Models.Enums;
using SP_Y4C.ViewModels;

namespace SP_Y4C.Controllers
{
    public class EngagementController : Controller
    {
        private readonly Y4CDbContext _dbContext;

        public EngagementController(Y4CDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Teach()
        {
            var questions = _dbContext.SurveyQuestions
                .Where(t => t.Category == QuestionCategory.Teach || t.Category == QuestionCategory.Default)
                .Where(s => s.ActiveStatus == QuestionActiveStatus.Active)
                .Include(c => c.Choices)
                .OrderBy(q => q.QuestionNumber);

            return View(questions);
        }

        [HttpPost]
        public IActionResult Practice()
        {
            var questions = _dbContext.SurveyQuestions
                .Where(t => t.Category == QuestionCategory.Practice || t.Category == QuestionCategory.Default)
                .Where(s => s.ActiveStatus == QuestionActiveStatus.Active)
                .Include(c => c.Choices)
                .OrderBy(q => q.QuestionNumber);

            return View(questions);
        }

        [HttpPost]
        public IActionResult Other()
        {
            var questions = _dbContext.SurveyQuestions
                .Where(t => t.Category == QuestionCategory.Other || t.Category == QuestionCategory.Default)
                .Where(s => s.ActiveStatus == QuestionActiveStatus.Active)
                .Include(c => c.Choices)
                .OrderBy(q => q.QuestionNumber);

            return View(questions);
        }

        [HttpPost]
        public async Task<IActionResult> Submit(IFormCollection form)
        {
            //Perform the calculation to see what the visitor should be placed as.
            var (urlForVisitor, visitorType) = await CalculateWeightToUrlAsync(form);
            await CreateAndStoreFormAnswersAsync(form, visitorType);

            return Redirect(urlForVisitor);
        }

        //Create and store each answer from the user into the DB.
        public async Task CreateAndStoreFormAnswersAsync(IFormCollection passedForm, UserType passedVisitorType)
        {
            var userGuid = Guid.NewGuid();
            // Skip final value as it's an HTTP token.
            var formAsList = passedForm.ToList();
            for (var s = 1; s < passedForm.Count - 1; s++)
            {
                // _ is to discard the value as it is not needed. We are only curious if the value is a GUID
                if (Guid.TryParse(formAsList.ElementAt(s).Key, out _))
                {
                    var formAnswer = new SurveyAnswer
                    {
                        Id = Guid.NewGuid(),
                        QuestionId = new Guid(formAsList.ElementAt(s).Key),
                        Answer = formAsList.ElementAt(s).Value,
                        UserId = userGuid,
                        UserType = passedVisitorType
                    };

                    //Validate the model being created before adding the answers to the DB
                    if (ModelState.IsValid)
                    {
                        await _dbContext.SurveyAnswers.AddAsync(formAnswer);
                        await _dbContext.SaveChangesAsync();
                    }
                }
            }
        }


        //Calculation for when a visitor answers the survey. This will take into account
        public async Task<(string redirectUrl, UserType visitorType)> CalculateWeightToUrlAsync(IFormCollection passedForm)
        {
            //Retrieve the array of questions to see which ones are weighed for querying the DB for the related branch
            // URL. If the calculated URL is null or empty then default to the donation page.
            var weightArray = passedForm["weight"];
            var hasWeight = weightArray.Contains("yes", StringComparer.OrdinalIgnoreCase);
            var weightvalue = hasWeight ? 1 : 0;
            var branch = (SurveyBranch) Enum.Parse(typeof(SurveyBranch), passedForm["page"]);

            var allUrls = await _dbContext.UrlToVisitors.ToListAsync();
            var calculatedUrl = allUrls.Where(w => w.Weight == weightvalue).Where(b => b.Category == branch).First();
            var url = calculatedUrl.Url;
            var visitorType = calculatedUrl.UserType;

            if (string.IsNullOrEmpty(url))
            {
                url = "https://www.y4c.org/donate";
                visitorType = UserType.Donor;
            }

            return (url, visitorType);
        }
    }
}