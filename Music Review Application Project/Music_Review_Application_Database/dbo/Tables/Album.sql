CREATE TABLE [dbo].[Album]
(
	[id] int PRIMARY KEY IDENTITY,
	[title] nvarchar(60) not null,
	[dateOfRelease] date not null,
	[score] decimal,
  	[img] varbinary(max)
)
