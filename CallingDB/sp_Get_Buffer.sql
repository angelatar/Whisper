CREATE PROCEDURE [dbo].[sp_Get_Buffer]
	@senderID INT, 
    @receiverID INT
AS
		SELECT TOP(1) * FROM Buffer WHERE ReceiverID=@receiverID AND SenderID=@senderID  ORDER BY CreateDate DESC 
RETURN 0
