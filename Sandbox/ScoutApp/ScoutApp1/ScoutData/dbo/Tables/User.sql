CREATE TABLE [dbo].[User] (
    [Id]           BIGINT          NOT NULL IDENTITY,
    [UserEmail]    NVARCHAR(255)      NULL,
    [UserAlias]    NVARCHAR(50)      NULL,
    [UserPassword] NVARCHAR(2048) NULL,
    [DateCreated]   DATETIME        DEFAULT (getdate()) NULL,
    [DateModified] DATETIME        DEFAULT (getdate()) NULL,
    [CreatedBy]    NVARCHAR(255)      NULL,
    [ModifiedBy]   NVARCHAR(255)      NULL,
    [Country]      NVARCHAR(128)      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



