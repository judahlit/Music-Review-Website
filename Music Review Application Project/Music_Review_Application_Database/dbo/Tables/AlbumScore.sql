CREATE TABLE [dbo].[AlbumScore]
(
  [id] int PRIMARY KEY,
  [albumId] int not null,
  [username] nvarchar(30) not null,
  [score] decimal not null,
  [review] nvarchar(2500), 
    CONSTRAINT [FK_AlbumScore_ToTable] FOREIGN KEY ([albumId]) REFERENCES [Album]([id])
)
