USE [PERC_eCC]
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'GetNextNoteNumber' AND XTYPE = 'FN')
BEGIN
	DROP FUNCTION GetNextNoteNumber
END
GO

CREATE FUNCTION dbo.GetNextNoteNumber (@CurrentNoteNumber VARCHAR(2))
RETURNS VARCHAR(2)
AS
BEGIN 
	DECLARE @NextNumber VARCHAR(2)

	IF @CurrentNoteNumber IS NULL OR @CurrentNoteNumber = ''
		SET @NextNumber = 'A'
	ELSE IF LEN(@CurrentNoteNumber) = 1 AND @CurrentNoteNumber = 'Z'
		SET @NextNumber = 'AA'
	ELSE IF LEN(@CurrentNoteNumber) = 1
		SET @NextNumber = CHAR(ASCII(@CurrentNoteNumber) + 1)
	ELSE IF LEN(@CurrentNoteNumber) = 2
	BEGIN
		DECLARE @N1 CHAR = SUBSTRING(@CurrentNoteNumber, 1, 1)
		DECLARE @N2 CHAR = SUBSTRING(@CurrentNoteNumber, 2, 1)
		IF @N2 = 'Z'
		BEGIN
			SET @N1 = CHAR(ASCII(@N1) + 1)
			SET @N2 = 'A'
		END
		ELSE 
			SET @N2 = CHAR(ASCII(@N2) + 1)
		SET @NextNumber = @N1 + @N2
	END
	RETURN @NextNumber
END
GO

