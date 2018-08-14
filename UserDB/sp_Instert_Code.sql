CREATE PROCEDURE [dbo].[sp_Instert_Code]
	@userEmail NVARCHAR(50),
	@code NVARCHAR(50)
AS
		INSERT INTO ValidationCodes VALUES (@userEmail,@code)
RETURN 0
