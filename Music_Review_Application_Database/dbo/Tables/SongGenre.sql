CREATE TABLE [dbo].[SongGenre]
(
  [id] int PRIMARY KEY IDENTITY,
  [songId] int not null,
  [genre] varchar(30) not null, 
    CONSTRAINT [FK_SongGenre_ToTable] FOREIGN KEY ([songId]) REFERENCES [Song]([id])
)
