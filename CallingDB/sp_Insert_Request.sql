CREATE PROCEDURE [dbo].[sp_Insert_Request]
	@senderID INT, 
    @receiverID INT, 
    @stateID INT
AS
	INSERT INTO Requests VALUES (@senderID,@receiverID,@stateID)
RETURN 0
