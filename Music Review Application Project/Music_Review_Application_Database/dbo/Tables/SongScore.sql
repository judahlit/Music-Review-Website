CREATE TABLE [dbo].[SongScore]
(
  [id] int PRIMARY KEY IDENTITY,
  [songId] int not null,
  [username] nvarchar(30) not null,
  [score] decimal not null,
  [review] nvarchar(2500), 
    CONSTRAINT [FK_SongScore_ToTable] FOREIGN KEY ([songId]) REFERENCES [Song]([id])
)
