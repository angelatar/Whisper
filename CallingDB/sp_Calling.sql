CREATE PROCEDURE [dbo].[sp_Calling]
	@Mode NVARCHAR(MAX), 
    @senderID INT = 0, 
    @receiverID INT = 0, 
    @stateID INT = 0,
	@traffic NVARCHAR(MAX) = ''
AS
	IF(@Mode = 'Insert_Request')
	BEGIN
		INSERT INTO Requests VALUES (@senderID,@receiverID,@stateID)
	END

	IF(@Mode = 'Delete_Request')
	BEGIN
		DELETE FROM Requests WHERE SenderID=@senderID AND ReceiverID=@receiverID
	END

	IF(@Mode = 'Get_Request')
	BEGIN
		SELECT * FROM Requests WHERE ReceiverID=@receiverID
	END

	IF(@Mode = 'Insert_Buffer')
	BEGIN
		INSERT INTO Buffer VALUES (@senderID,@receiverID,@traffic)
	END

	IF(@Mode = 'Delete_Buffer')
	BEGIN
		DELETE FROM Buffer WHERE SenderID=@senderID AND ReceiverID=@receiverID
	END

	IF(@Mode = 'Get_Buffer')
	BEGIN
		SELECT * FROM Buffer WHERE ReceiverID=@receiverID
	END

RETURN 0
