CREATE TABLE [dbo].[AlbumArtist]
(
  [id] int PRIMARY KEY IDENTITY,
  [albumId] int not null,
  [artistId] int not null, 
    CONSTRAINT [FK_AlbumArtist_ToTable] FOREIGN KEY ([albumId]) REFERENCES [Album]([id]), 
    CONSTRAINT [FK_AlbumArtist_ToTable_1] FOREIGN KEY ([artistId]) REFERENCES [Artist]([id])
)
