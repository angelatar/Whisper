CREATE PROCEDURE [dbo].[sp_Create_User]
	@name NVARCHAR(MAX),
	@lastname NVARCHAR(MAX),
	@username NVARCHAR(MAX),
	@passwordhash NVARCHAR(MAX),
	@email NVARCHAR(MAX)
AS
		INSERT INTO Users VALUES (@name,@lastname,@username,@passwordhash,@email,GETDATE(),GETDATE())
RETURN 0
