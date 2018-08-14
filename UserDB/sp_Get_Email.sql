CREATE PROCEDURE [dbo].[sp_Get_Email]
	@id INT
AS
	SELECT Email FROM Users WHERE Id=@id
RETURN 0
