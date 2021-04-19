CREATE TABLE [dbo].[Single]
(
	[id] int PRIMARY KEY IDENTITY,
	[songId] int not null,
	[img] varbinary(max), 
    CONSTRAINT [FK_Single_ToTable] FOREIGN KEY ([songId]) REFERENCES [Song]([id])
)
