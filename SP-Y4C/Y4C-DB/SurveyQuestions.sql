CREATE TABLE [dbo].[SurveyQuestions]
(
	[QuestionId] VARCHAR(255) NOT NULL PRIMARY KEY, 
    [Title] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(MAX) NOT NULL, 
    [Type] VARCHAR(50) NOT NULL, 
    [ActiveStatus] TINYINT NOT NULL, 
    [Weight] VARCHAR(50) NOT NULL, 
    [CreationDate] DATETIME NOT NULL
)
