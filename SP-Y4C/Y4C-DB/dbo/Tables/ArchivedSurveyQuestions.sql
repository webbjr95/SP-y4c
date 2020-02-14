CREATE TABLE [dbo].[ArchivedSurveyQuestions]
(
	[Id] INT NOT NULL IDENTITY(1,1),
    [QuestionNumber] INT NOT NULL,
    [QuestionTypeId] INT NOT NULL, 
    [Text] VARCHAR(500) NOT NULL,
    [ArchivedAtUtc] DATETIMEOFFSET NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT [PK_ArchivedSurveyQuestions_Id] PRIMARY KEY ([Id])
)
