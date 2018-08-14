CREATE PROCEDURE [dbo].[sp_Get_Request_By_Sender_And_Receiver]
	@senderID INT, 
    @receiverID INT
AS
	SELECT TOP(1) * FROM Requests WHERE ReceiverID=@receiverID AND SenderID=@senderID ORDER BY ID DESC
RETURN 0
