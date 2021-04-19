CREATE TABLE [dbo].[Artist]
(
  [id] int PRIMARY KEY IDENTITY,
  [artistName] nvarchar(30) not null,
  [img] varbinary(max),
  [description] nvarchar(1000)
)
