CREATE TABLE [dbo].[AlbumReview]
(
  [id] int PRIMARY KEY,
  [albumId] int not null,
  [username] nvarchar(30) not null,
  [albumScore] int not null,
  [albumReview] nvarchar(2500), 
    CONSTRAINT [FK_AlbumReview_ToAlbumTable] FOREIGN KEY ([albumId]) REFERENCES [Album]([id])
)
