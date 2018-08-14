CREATE PROCEDURE [dbo].[sp_Delete_User]
	@id INT = 0
AS
	DELETE FROM Users WHERE Id=@id
RETURN 0
