CREATE TABLE [dbo].[SurveyQuestions]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
    [QuestionNumber] INT NOT NULL,
    [TypeId] INT NOT NULL DEFAULT 1, -- Text by default
    [Text] VARCHAR(500) NOT NULL,
    [CreatedAtUtc] DATETIMEOFFSET NOT NULL DEFAULT GETUTCDATE(),
    [LastModifiedAtUtc] DATETIMEOFFSET NOT NULL DEFAULT GETUTCDATE(),
    [ActiveStatus] INT NOT NULL DEFAULT 0, 
    [Weight] INT NOT NULL DEFAULT 1, 
    [Category] INT NOT NULL DEFAULT 0,

    CONSTRAINT [PK_SurveyQuestions_Id] PRIMARY KEY ([Id])
)
