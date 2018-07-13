CREATE TABLE [dbo].[Buffer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SenderID] INT NOT NULL, 
    [ReceiverID] INT NOT NULL, 
    [Traffic] NVARCHAR(MAX) NOT NULL
)