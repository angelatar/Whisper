CREATE PROCEDURE [dbo].[sp_Get_User_By_Username]
	@username NVARCHAR(MAX)
AS
	SELECT * FROM Users WHERE Username=@username
RETURN 0
