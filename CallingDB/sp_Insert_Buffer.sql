CREATE PROCEDURE [dbo].[sp_Insert_Buffer]
	@senderID INT, 
    @receiverID INT,
	@traffic NVARCHAR(MAX)
AS
		INSERT INTO Buffer VALUES (@senderID,@receiverID,@traffic,GETDATE())
RETURN 0
