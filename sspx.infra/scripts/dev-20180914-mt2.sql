USE [PERC_eCC]
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'GenerateNumber' AND XTYPE = 'TF')
BEGIN
	DROP FUNCTION GenerateNumber
END
GO

CREATE FUNCTION dbo.GenerateNumber (
	@EndId INT
) 
RETURNS @IntList TABLE (
	TempId INT IDENTITY(1, 1),
	ListId INT
)
AS 
BEGIN
	DECLARE @StartId INT = 0
	WHILE (@StartId < @EndId)
	BEGIN
		SET @StartId = @StartId + 1
		INSERT INTO @IntList (ListId) VALUES (@StartId)
	END

	RETURN
END
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'SSPx_ResortChecklistVersionItems' AND XTYPE = 'P')
	DROP PROCEDURE SSPx_ResortChecklistVersionItems
GO

CREATE PROCEDURE [dbo].[SSPx_ResortChecklistVersionItems]
	@ChecklistVersionCkey DECIMAL(20, 9)
AS
BEGIN
	DECLARE @NodeItem TABLE (
		TempId INT IDENTITY(100, 100),
		ItemId DECIMAL(20,9)
	)

	INSERT INTO @NodeItem (ItemId)
	SELECT ChecklistTemplateItemCkey 
	FROM dbo.SSP_VersionItems_work (NOLOCK) 
	WHERE ChecklistTemplateVersionCKey = @ChecklistVersionCkey 
	ORDER BY SortOrder

	UPDATE i SET SortOrder = n.TempId
	FROM dbo.SSP_VersionItems_work i (NOLOCK)
		INNER JOIN @NodeItem n ON i.ChecklistTemplateItemCkey = n.ItemId
END