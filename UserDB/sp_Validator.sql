CREATE PROCEDURE [dbo].[sp_Validator]
	@Mode NVARCHAR(MAX),
	@userID INT = 0,
	@code NVARCHAR(50) = ''

AS

	IF(@Mode = 'Instert_Code')
	BEGIN
		INSERT INTO ValidationCodes VALUES (@userID,@code)
	END

	IF(@Mode = 'Check_Code')
	BEGIN
		SELECT * FROM ValidationCodes WHERE UserID = @userID AND Code = @code
	END

	IF(@Mode = 'Delete_Code')
	BEGIN
		DELETE FROM ValidationCodes WHERE UserID = @userID
	END

RETURN 0
