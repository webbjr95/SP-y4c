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

        // Create and store each answer from the user into the DB.
        public async Task CreateAndStoreFormAnswersAsync(IFormCollection passedForm, UserType passedVisitorType)
        {
            var userGuid = Guid.NewGuid();

            // Gather all the choices so we can get the text for their answer later on. Create a list of Answers for 
            // iteration later on.
            var listOfChoices = _dbContext.SurveyChoices.AsQueryable();
            var listOfAnswers = new List<SurveyAnswer>();

            // Answer is going to be a key,value pair. Key will be the QuestionId while the value is the ChoiceId. 
            foreach (var formChoice in passedForm.ToList())
            {
                // _ is to discard the value as it is not needed. We are only curious if the value is a GUID which would
                // correspond a question being present at that enumeration.
                if (Guid.TryParse(formChoice.Key, out _))
                {
                    // Create the answer objects after parsing the form submission.
                    var userAnswer = "";
                    if (formChoice.Value.Count == 1)
                    {
                        //Only need to check if this is a text input field since the value can only of a count of one.
                        if (Guid.TryParse(formChoice.Value, out _))
                        {
                            userAnswer = listOfChoices.Where(x => x.Id == new Guid(formChoice.Value)).First().Text;
                        } else {
                            userAnswer = formChoice.Value;
                        }

                        var formAnswer = new SurveyAnswer
                        {
                            Id = Guid.NewGuid(),
                            QuestionId = new Guid(formChoice.Key),
                            Answer = userAnswer,
                            UserId = userGuid,
                            UserType = passedVisitorType
                        };

                        listOfAnswers.Add(formAnswer);
                    } 
                    else if (formChoice.Value.Count > 1)
                    {

                        foreach (var choice in formChoice.Value)
                        {
                            userAnswer = listOfChoices.Where(x => x.Id == new Guid(choice)).First().Text;
                            var formAnswer = new SurveyAnswer
                            {
                                Id = Guid.NewGuid(),
                                QuestionId = new Guid(formChoice.Key),
                                Answer = userAnswer,
                                UserId = userGuid,
                                UserType = passedVisitorType
                            };

                            listOfAnswers.Add(formAnswer);
                        }
                    }
                }
            }

            // Validate the model being created before adding the answers to the DB
            foreach (var answer in listOfAnswers)
            {
                if (ModelState.IsValid)
                {
                    await _dbContext.SurveyAnswers.AddAsync(answer);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }


        // Calculation for when a visitor answers the survey. This will take into account weighted a question.
        public async Task<(string redirectUrl, UserType visitorType)> CalculateWeightToUrlAsync(IFormCollection passedForm)
        {
            // Retrieve the array of questions to see which ones are weighed for querying the DB for the related branch
            // URL.
            var weightArray = passedForm["weight"].ToArray();
            var hasWeight = weightArray.Contains("yes", StringComparer.OrdinalIgnoreCase);
            var weightedQuestionIndex = 0;
            var weightvalue = 0;
            if (hasWeight)
            {
                weightedQuestionIndex = Array.FindIndex(weightArray, v => v.Equals("Yes")) + 1;
                var userChoiceId = Guid.Parse(passedForm.ElementAt(weightedQuestionIndex).Value);
                var userChoiceText = _dbContext.SurveyChoices.Where(c => c.Id == userChoiceId).First().Text;
                weightvalue = userChoiceText.Equals("Yes", StringComparison.OrdinalIgnoreCase) ? 1 : 0;
            }

            var branch = (SurveyBranch) Enum.Parse(typeof(SurveyBranch), passedForm["page"]);

            var allUrls = await _dbContext.UrlToVisitors.ToListAsync();
            var calculatedUrl = allUrls.Where(w => w.Weight == weightvalue).Where(b => b.Category == branch).First();
            var url = calculatedUrl.Url;
            var visitorType = calculatedUrl.UserType;

            if (string.IsNullOrEmpty(url))
            {
                url = "N/A";
                visitorType = UserType.NotFound;
            }

            return (url, visitorType);
        }
    }
}