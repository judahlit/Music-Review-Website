CREATE TABLE [dbo].[SongGenre]
(
  [id] int PRIMARY KEY IDENTITY,
  [songId] int not null,
  [genre] varchar(30) not null
)
