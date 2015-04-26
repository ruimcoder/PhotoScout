CREATE TABLE [dbo].[Location] (
    [Id]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50) NOT NULL,
    [UserId]       NCHAR (10)    NOT NULL,
    [DateCreated]  DATETIME      DEFAULT (getdate()) NOT NULL,
    [DateModified] DATETIME      DEFAULT (getdate()) NOT NULL,
    [CretedBy]     NCHAR (10)    NULL,
    [ModifiedBy]   NCHAR (10)    NULL,
    [Active]       BIT           DEFAULT ((1)) NOT NULL,
    [Coordinates]  NCHAR (100)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



