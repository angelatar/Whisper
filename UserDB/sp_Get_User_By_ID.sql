CREATE PROCEDURE [dbo].[sp_Get_User_By_ID]
	@id INT
AS
	SELECT * FROM Users WHERE Id=@id
RETURN 0
