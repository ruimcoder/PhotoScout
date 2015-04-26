CREATE TABLE [dbo].[LocationImage] (
    [Id]               INT            NOT NULL,
    [ImageTitle]       NCHAR (10)     NULL,
    [FileName]         NCHAR (10)     NULL,
    [OriginalFileName] NCHAR (10)     NULL,
    [DateCreated]      DATETIME       DEFAULT (getdate()) NULL,
    [CreatedBy]        NCHAR (10)     NULL,
    [ModifiedBy]       NCHAR (10)     NULL,
    [Active]           BIT            DEFAULT ((1)) NULL,
    [Description]      NVARCHAR (255) NULL,
    [Notes]            NVARCHAR (MAX) NULL,
    [DateModified]     DATETIME       DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



