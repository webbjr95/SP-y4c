using Microsoft.AspNetCore.Mvc;
using SP_Y4C.Models;
using System.Threading.Tasks;

namespace SP_Y4C.Views.Shared.Components.QuestionModal
{
    [ViewComponent(Name = "QuestionModal")]
    public class QuestionModalViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(string id, string title, string primaryButtonText, string secondaryButtonText, Question question)
        {
            var viewModel = new QuestionModalViewModel
            {
                Id = id,
                Title = title,
                PrimaryButtonText = primaryButtonText,
                SecondaryButtonText = secondaryButtonText,
                Question = question
            };

            return Task.FromResult((IViewComponentResult)View("Default", viewModel));
        }
    }
}
