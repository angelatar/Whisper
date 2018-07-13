CREATE TABLE [dbo].[Requests]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SenderID] INT NOT NULL, 
    [ReceiverID] INT NOT NULL, 
    [StateID] INT NOT NULL
)