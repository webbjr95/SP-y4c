CREATE TABLE [dbo].[SurveyAnswers]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
    [QuestionId] UNIQUEIDENTIFIER NOT NULL,
    [UserId] UNIQUEIDENTIFIER NOT NULL,
    [UserTypeId] INT NOT NULL,
    [Answer] VARCHAR(MAX) NOT NULL,
    [SubmittedAtUtc] DATETIMEOFFSET NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT [PK_SurveyAnswers_Id] PRIMARY KEY ([Id]),
    --CONSTRAINT [FK_SurveyAnswers_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [SurveyQuestions]([Id])
)
