CREATE TABLE [dbo].[SongArtist]
(
  [id] int PRIMARY KEY IDENTITY,
  [songId] int not null,
  [artistId] int not null, 
    CONSTRAINT [FK_SongArtist_ToTable] FOREIGN KEY ([songId]) REFERENCES [Song]([id]), 
    CONSTRAINT [FK_SongArtist_ToTable_1] FOREIGN KEY ([artistId]) REFERENCES [Artist]([id])
)
