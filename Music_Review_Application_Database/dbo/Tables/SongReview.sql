CREATE TABLE [dbo].[SongReview] (
    [id]         INT             PRIMARY KEY IDENTITY NOT NULL,
    [songId]     INT             NOT NULL,
    [username]   NVARCHAR (30)   NOT NULL,
    [songScore]  FLOAT        NOT NULL,
    [songReview] NVARCHAR (2500) NULL,
    CONSTRAINT [FK_SongReview_ToSongTable] FOREIGN KEY ([songId]) REFERENCES [dbo].[Song] ([id])
);

