namespace SP_Y4C.Models
{
    public class QuestionModalViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string PrimaryButtonText { get; set; }

        public string SecondaryButtonText { get; set; }

        public Question Question { get; set; }
    }
}
