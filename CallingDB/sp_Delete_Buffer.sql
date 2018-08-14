CREATE PROCEDURE [dbo].[sp_Delete_Buffer]
	@senderID INT, 
    @receiverID INT
AS
		DELETE FROM Buffer WHERE SenderID=@senderID AND ReceiverID=@receiverID
RETURN 0
