CREATE TABLE [dbo].[Song]
(
  [id] int PRIMARY KEY IDENTITY,
  [title] nvarchar(60) not null,
  [dateOfRelease] date not null,
  [trackId] int,
  [score] decimal,
  [albumId] int, 
    CONSTRAINT [FK_Song_ToTable] FOREIGN KEY ([albumId]) REFERENCES [Album]([id])
)
