CREATE TABLE [dbo].[SongReview]
(
  [id] int PRIMARY KEY IDENTITY,
  [songId] int not null,
  [username] nvarchar(30) not null,
  [songScore] decimal not null,
  [songReview] nvarchar(2500), 
    CONSTRAINT [FK_SongReview_ToSongTable] FOREIGN KEY ([songId]) REFERENCES [Song]([id])
)
