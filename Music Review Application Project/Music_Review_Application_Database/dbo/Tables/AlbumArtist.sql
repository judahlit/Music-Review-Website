CREATE TABLE [dbo].[AlbumArtist]
(
  [id] int PRIMARY KEY IDENTITY,
  [albumId] int not null,
  [artistId] int not null
)
