CREATE TABLE [dbo].[SongGenre]
(
  [id] int PRIMARY KEY IDENTITY,
  [songId] int not null,
  [genreId] int not null, 
    CONSTRAINT [FK_SongGenre_ToTable] FOREIGN KEY ([songId]) REFERENCES [Song]([id]), 
    CONSTRAINT [FK_SongGenre_ToTableGenre] FOREIGN KEY ([genreId]) REFERENCES [Genre](id)
)
