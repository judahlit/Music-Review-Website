CREATE TABLE [dbo].[SongArtist]
(
  [id] int PRIMARY KEY IDENTITY,
  [songId] int not null,
  [artistId] int not null
)
