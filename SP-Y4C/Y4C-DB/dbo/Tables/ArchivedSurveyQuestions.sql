CREATE TABLE [dbo].[ArchivedSurveyQuestions]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
    [QuestionNumber] INT NOT NULL,
    [TypeId] INT NOT NULL, 
    [Text] VARCHAR(500) NOT NULL,
    [ArchivedAtUtc] DATETIMEOFFSET NOT NULL DEFAULT GETUTCDATE(),
    [ActiveStatus] INT NOT NULL, 
    [UserArchivedBy] VARCHAR(500) NOT NULL, 

    CONSTRAINT [PK_ArchivedSurveyQuestions_Id] PRIMARY KEY ([Id])
)
