CREATE PROCEDURE [dbo].[sp_Delete_Request]
	@senderID INT, 
    @receiverID INT
AS
	DELETE FROM Requests WHERE SenderID=@senderID AND ReceiverID=@receiverID
RETURN 0
