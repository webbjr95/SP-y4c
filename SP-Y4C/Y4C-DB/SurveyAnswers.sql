CREATE TABLE [dbo].[SurveyAnswers]
(
	[AnswerID] VARCHAR(255) NOT NULL PRIMARY KEY, 
    [UserID] VARCHAR(MAX) NOT NULL, 
    [QuestionID] VARCHAR(255) NOT NULL, 
    [SubmissionDate] DATETIME NOT NULL, 
    CONSTRAINT [FK_SurveyAnswers_ToTable] FOREIGN KEY ([QuestionID]) REFERENCES [SurveyQuestions]([QuestionID])
)
