using Microsoft.EntityFrameworkCore;
using SP_Y4C.Models;

namespace SP_Y4C.Data
{
    public class Y4CDbContext : DbContext 
    {
        public Y4CDbContext(DbContextOptions<Y4CDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyChoice> SurveyChoices { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        public DbSet<SurveyFeedback> SurveyFeedback { get; set; }
        public DbSet<ArchivedSurveyQuestion> ArchivedSurveyQuestions { get; set; }
        public DbSet<ArchivedSurveyFeedback> ArchivedSurveyFeedback { get; set; }
        public DbSet<UrlToVisitor> UrlToVisitors { get; set; }
    }
}
