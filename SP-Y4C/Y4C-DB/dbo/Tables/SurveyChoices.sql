CREATE TABLE [dbo].[SurveyChoices]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
    -- Text by default
    [Text] VARCHAR(500) NOT NULL,
    [CreatedAtUtc] DATETIMEOFFSET NOT NULL DEFAULT GETUTCDATE(),
    [LastModifiedAtUtc] DATETIMEOFFSET NOT NULL DEFAULT GETUTCDATE(),
    [QuestionId] UNIQUEIDENTIFIER NOT NULL, 
    [OrderInQuestion] INT NOT NULL DEFAULT 1,
    
    CONSTRAINT [PK_SurveyChoices_Id] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SurveyChoices_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [SurveyQuestions]([Id])
)
