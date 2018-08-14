CREATE PROCEDURE [dbo].[sp_Update_Password]
	@passwordhash NVARCHAR(MAX),
	@id INT
AS
		UPDATE Users SET PasswordHash=@passwordhash WHERE Id=@id
RETURN 0
