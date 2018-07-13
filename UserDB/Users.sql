CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Lastname] NVARCHAR(MAX) NOT NULL, 
    [Username] NVARCHAR(MAX) NOT NULL, 
    [PasswordHash] NVARCHAR(MAX) NOT NULL, 
    [Email] NVARCHAR(MAX) NOT NULL, 
    [CreateDate] DATETIME NOT NULL, 
    [LastLoginDate] DATETIME NOT NULL
)
