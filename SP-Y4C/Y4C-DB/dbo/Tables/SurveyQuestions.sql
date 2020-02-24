CREATE TABLE [dbo].[SurveyQuestions]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
    [QuestionNumber] INT NOT NULL,
    [TypeId] INT NOT NULL DEFAULT 1, -- Text by default
    [Text] VARCHAR(500) NOT NULL,
    [CreatedAtUtc] DATETIMEOFFSET NOT NULL DEFAULT GETUTCDATE(),
    [LastModifiedAtUtc] DATETIMEOFFSET NOT NULL DEFAULT GETUTCDATE(),
    [ActiveStatus] INT NOT NULL, 
    [Weight] INT NOT NULL DEFAULT 1, 
    [Category] VARCHAR(50) NOT NULL DEFAULT 'none', 
    --[Node] HierarchyId NOT NULL, 

    CONSTRAINT [PK_SurveyQuestions_Id] PRIMARY KEY ([Id])
)
