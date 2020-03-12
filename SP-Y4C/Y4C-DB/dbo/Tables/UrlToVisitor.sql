CREATE TABLE [dbo].[VisitorTypes]
(
	[Id] UNIQUEIDENTIFIER NOT NULL,
    [Url] VARCHAR(500) NOT NULL,

    [UserType] INT NOT NULL, 
    [Category] VARCHAR(100) NOT NULL, 
    [Weight] INT NOT NULL, 
    [CreatedAtUtc] DATETIMEOFFSET NOT NULL, 
    [LastModifiedAtUtc] DATETIMEOFFSET NOT NULL, 
    CONSTRAINT [PK_VisitorTypes_Id] PRIMARY KEY ([Id]),
)
