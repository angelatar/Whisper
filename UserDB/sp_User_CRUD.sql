CREATE PROCEDURE [dbo].[sp_User_CRUD]
	@Mode NVARCHAR(MAX),
	@name NVARCHAR(MAX) = '',
	@lastname NVARCHAR(MAX) = '',
	@username NVARCHAR(MAX) = '',
	@passwordhash NVARCHAR(MAX) = '',
	@email NVARCHAR(MAX) = '',
	@id INT = 0
AS
	IF(@Mode = 'Create_User')
	BEGIN
		INSERT INTO Users VALUES (@name,@lastname,@username,@passwordhash,@email,GETDATE(),GETDATE())
	END

	IF(@Mode = 'Delete_User')
	BEGIN
		DELETE FROM Users WHERE Id=@id
	END

	IF(@Mode = 'Update_Password')
	BEGIN
		UPDATE Users SET PasswordHash=@passwordhash WHERE Id=@id
	END

	IF(@Mode = 'Update_Last_Login_Date')
	BEGIN		
		UPDATE Users SET LastLoginDate=GETDATE() WHERE Id=@id
	END

	IF(@Mode = 'Get_User_By_ID')
	BEGIN		
		SELECT * FROM Users WHERE Id=@id
	END

	IF(@Mode = 'Get_User_By_Username')
	BEGIN		
		SELECT * FROM Users WHERE Username=@username
	END
	
	IF(@Mode = 'Get_All_Users')
	BEGIN		
		SELECT * FROM Users
	END
	
	IF(@Mode = 'Get_Email')
	BEGIN		
		SELECT Email FROM Users WHERE Id=@id
	END
	
	IF(@Mode = 'Get_Last_Login')
	BEGIN		
		SELECT LastLoginDate FROM Users WHERE Id=@id
	END

RETURN 0
