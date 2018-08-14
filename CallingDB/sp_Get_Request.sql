CREATE PROCEDURE [dbo].[sp_Get_Request]
	 @receiverID INT
AS
	SELECT * FROM Requests WHERE ReceiverID=@receiverID
RETURN 0
