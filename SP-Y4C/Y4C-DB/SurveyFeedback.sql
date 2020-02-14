CREATE TABLE [dbo].[SurveyFeedback]
(
	[FeedbackId] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [SubmissionDate] DATETIME NOT NULL, 
    [Rating] INT NOT NULL, 
    [UserType] INT NOT NULL, 
    [Url] VARCHAR(255) NOT NULL,
)
