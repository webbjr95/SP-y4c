CREATE TABLE [dbo].[Questions]
(
	[Id]                INT             NOT NULL    IDENTITY(1,1) PRIMARY KEY, 
    [Text]              NVARCHAR(500)   NOT NULL,
    [QuestionNumber]    INT             NOT NULL,
    [QuestionTypeId]    INT             NOT NULL,
    [CreatedAtUtc]      DATETIMEOFFSET  NOT NULL    DEFAULT GETUTCDATE(), 
    [LastModifiedAtUtc] DATETIMEOFFSET  NOT NULL    DEFAULT GETUTCDATE(), 
    [IsRetired]         BIT             NOT NULL    DEFAULT 0, 
)
