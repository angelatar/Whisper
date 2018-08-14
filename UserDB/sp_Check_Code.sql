CREATE PROCEDURE [dbo].[sp_Check_Code]
	@userEmail NVARCHAR(50),
	@code NVARCHAR(50)
AS
		SELECT * FROM ValidationCodes WHERE UserEmail = @userEmail AND Code = @code
RETURN 0
