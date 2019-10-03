CREATE TABLE [dbo].[QuestionsTable]
(
	[QuestionID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Text] NVARCHAR(500) NULL, 
    [Description] NVARCHAR(500) NULL, 
    [CreationDateTime] DATETIME NULL, 
    [Status] BIT NOT NULL DEFAULT 0, 
)
