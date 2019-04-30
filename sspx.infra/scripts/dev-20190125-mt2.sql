IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'ConvertIntToNoteNumber' AND XTYPE = 'FN')
	DROP FUNCTION dbo.ConvertIntToNoteNumber
GO

CREATE FUNCTION [dbo].[ConvertIntToNoteNumber] (@Id INT)
	RETURNS VARCHAR(2)
AS
BEGIN 
	DECLARE @NoteNumber VARCHAR(2) = ''
	DECLARE @Num1 INT = (@Id - 1)/26, @Num2 INT = (@Id - 1)%26

	IF @Num1 > 0
		SET @NoteNumber = CHAR(@Num1 + 64)

	SET @NoteNumber = @NoteNumber + CHAR(@Num2 + 65)
	
	RETURN @NoteNumber
END
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'SSPx_NoteRenumber' AND XTYPE = 'P')
	DROP PROCEDURE dbo.SSPx_NoteRenumber
GO

CREATE PROCEDURE dbo.SSPx_NoteRenumber
	@ProtocolVersionKey INT
AS
	DECLARE @NoteOrder TABLE (OrderId INT IDENTITY(1, 1), NoteKey INT)

	INSERT INTO @NoteOrder (NoteKey)
	SELECT n.ProtocolVersionNotesKey
	FROM dbo.SSPX_ProtocolVersionNotes n (NOLOCK)
	WHERE n.Active = 1 AND n.ProtocolVersionsKey = @ProtocolVersionKey
	ORDER BY LEN(n.NoteID), NoteID, ProtocolVersionNotesKey

	UPDATE n SET NoteID = dbo.ConvertIntToNoteNumber(o.OrderId)
	FROM dbo.SSPX_ProtocolVersionNotes n (NOLOCK)
	INNER JOIN @NoteOrder o ON n.ProtocolVersionNotesKey = o.NoteKey
GO

