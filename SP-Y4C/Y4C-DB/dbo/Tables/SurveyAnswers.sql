CREATE TABLE [dbo].[SurveyAnswers]
(
	[Id] INT NOT NULL IDENTITY(1,1),
    [QuestionId] INT NOT NULL,
    [Answer] VARCHAR(MAX) NOT NULL,
    [UserId] VARCHAR(MAX) NOT NULL,
    [UserTypeId] INT NOT NULL,
    [SubmittedAtUtc] DATETIMEOFFSET NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT [PK_SurveyAnswers_Id] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SurveyAnswers_QuestionId] FOREIGN KEY ([QuestionId]) REFERENCES [SurveyQuestions]([Id])
)
