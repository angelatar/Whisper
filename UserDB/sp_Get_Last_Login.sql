CREATE PROCEDURE [dbo].[sp_Get_Last_Login]
	@id INT
AS
	SELECT LastLoginDate FROM Users WHERE Id=@id
RETURN 0
