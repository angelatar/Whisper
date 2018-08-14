CREATE PROCEDURE [dbo].[sp_Update_Last_Login_Date]
	@id INT
AS
		UPDATE Users SET LastLoginDate=GETDATE() WHERE Id=@id
RETURN 0
