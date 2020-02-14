CREATE TABLE [dbo].[SurveyQuestions]
(
	[Id] INT NOT NULL IDENTITY(1,1),
    [QuestionNumber] INT NOT NULL,
    [QuestionTypeId] INT NOT NULL DEFAULT 1, -- Text by default
    [Text] VARCHAR(500) NOT NULL,
    [CreatedAtUtc] DATETIMEOFFSET NOT NULL DEFAULT GETUTCDATE(),
    [LastModifiedAtUtc] DATETIMEOFFSET NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT [PK_SurveyQuestions_Id] PRIMARY KEY ([Id])
)
