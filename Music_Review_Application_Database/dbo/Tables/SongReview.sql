CREATE TABLE [dbo].[SongReview] (
    [id]         INT             IDENTITY (1, 1) NOT NULL,
    [songId]     INT             NOT NULL,
    [username]   NVARCHAR (30)   NOT NULL,
    [songScore]  INT    NOT NULL,
    [songReview] NVARCHAR (2500) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_SongReview_ToSongTable] FOREIGN KEY ([songId]) REFERENCES [dbo].[Song] ([id])
);

