CREATE TABLE [dbo].[SurveyAnswers]
(
	[AnswerId] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [UserId] VARCHAR(50) NOT NULL, 
    [QuestionId] VARCHAR(50) NOT NULL, 
    [SubmissionDate] DATETIME NOT NULL, 
    [Answer] VARCHAR(MAX) NOT NULL, 
    [UserType] INT NOT NULL, 
    CONSTRAINT [FK_SurveyAnswers_ToTable] FOREIGN KEY ([QuestionId]) REFERENCES [SurveyQuestions]([QuestionID])
)
