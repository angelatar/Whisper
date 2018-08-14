CREATE PROCEDURE [dbo].[sp_Delete_Code]
	@userEmail NVARCHAR(50)
AS
		DELETE FROM ValidationCodes WHERE UserEmail = @userEmail
RETURN 0
