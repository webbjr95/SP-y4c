CREATE TABLE [dbo].[SurveyQuestions]
(
	[QuestionId] VARCHAR(50) NOT NULL PRIMARY KEY, 
    [Title] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(MAX) NOT NULL, 
    [Type] INT NOT NULL, 
    [ActiveStatus] INT NOT NULL, 
    [Weight] VARCHAR(50) NOT NULL, 
    [CreationDate] DATETIME NOT NULL
)
