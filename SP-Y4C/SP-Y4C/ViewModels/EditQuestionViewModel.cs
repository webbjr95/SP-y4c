using SP_Y4C.Models.Enums;
using System;
using System.Collections.Generic;

namespace SP_Y4C.ViewModels
{
    public class EditQuestionViewModel
    {
        public Guid Id { get; set; }

        public int QuestionNumber { get; set; }

        public QuestionType QuestionType { get; set; }

        public string Text { get; set; }

        public List<string> RadioOptions { get; set; }

        public QuestionWeight Weight { get; set; }

        public QuestionCategory Category { get; set; }
    }
}
