USE [PERC_eCC_SSPx]
GO

/*

-- populate SSPX_TemplateVersionItems.ItemTypesKey only apply if itemtypekey is all null
UPDATE i SET ItemTypesKey = c.ItemTypeKey
FROM SSPX_TemplateVersionItems i (NOLOCK)
INNER JOIN ChecklistTemplateItems c (NOLOCK) ON i.ChecklistTemplateItemCkey = c.ChecklistTemplateItemCkey

-- populate SSPX_ProtocolVersion.CoverDocRaw
UPDATE v SET CoverDocRaw = d.ProcedureDetails
FROM dbo.SSPX_ProtocolVersions v (NOLOCK)
INNER JOIN PERC_eCC.dbo.SSP_ProtocolProcedures_work d (NOLOCK) ON v.ProtocolVersionsKey = (d.ProtocolVersionCKey - 0.100004300)

-- populate SSPX_ProtocolVersionUserRoles
INSERT INTO dbo.SSPX_ProtocolVersionUserRoles (RolesKey, UsersKey, ProtocolVersionsKey, CreatedBy, CreatedDt, LastUpdated, LastUpdatedDt)
SELECT (r.RoleCKey - 0.100004300), (a.AuthorCKey - 0.100004300), (a.ProtocolVersionCKey - 0.100004300), 
	(a.CreatedBy - 0.100004300), COALESCE(a.CreatedDt, GETDATE()), (a.LastUpdated - 0.100004300), COALESCE(a.LastUpdatedDt, GETDATE())
FROM X_SSP_ProtocolAuthors_work a (NOLOCK)
	INNER JOIN X_SSP_ProtocolAuthorRole_work r (NOLOCK) ON a.ProtocolAuthorCKey = r.X_ProtocolAuthorCKey
	INNER JOIN dbo.SSPX_ProtocolVersions v (NOLOCK) ON v.ProtocolVersionsKey = (a.ProtocolVersionCKey - 0.100004300)

-- populate SSPX_ProtocolVersionNotes
INSERT INTO dbo.SSPX_ProtocolVersionNotes (NoteID, NoteTitle, NoteRaw, NoteDetails_html, ProtocolVersionsKey, 
	UsersKey, Active, DateCreated, LastUpdated)
SELECT NoteNumber, NoteTitle, NoteDetails, NoteDetails_html, (ProtocolVersionCKey - 0.100004300), 
	(CreatedBy - 0.100004300), 1, GETDATE(), GETDATE()
FROM PERC_eCC.dbo.SSP_ProtocolExplanatoryNotes_work n (NOLOCK)
	INNER JOIN SSPX_ProtocolVersions v (NOLOCK) ON v.ProtocolVersionsKey = (n.ProtocolVersionCKey - 0.100004300)
	
-- populate SSPX_NoteReferences
INSERT INTO dbo.SSPX_NoteReferences (ProtocolVersionNotesKey, ReferenceNumber, ReferenceTitle, UsersKey, LastUpdatedDt, Active, DateCreated)
SELECT n2.ProtocolVersionNotesKey, r.ReferenceNumber, r.ReferenceTitle, (r.CreatedBy - 0.100004300), r.LastUpdatedDt, COALESCE(r.Active, 1), r.CreatedDt
FROM PERC_eCC.dbo.SSP_ProtocolNotesReferences_work r (NOLOCK)
	INNER JOIN PERC_eCC.dbo.SSP_ProtocolExplanatoryNotes_work n (NOLOCK) ON r.NoteCKey = n.NoteCKey
	INNER JOIN SSPX_ProtocolVersionNotes n2 (NOLOCK) ON (n.ProtocolVersionCKey - 0.100004300) = n2.ProtocolVersionsKey
		AND n.NoteNumber = n2.NoteID
		
-- populate SSPX_ProtocolGroupsMM
INSERT INTO dbo.SSPX_ProtocolGroupsMM (ProtocolGroupsMMKey, ProtocolsKey, ProtocolGroupKey)
SELECT g.Protocol_GroupKey, (g.ProtocolCkey - 0.100004300), (g.ProtocolGroupKey - 0.100004300)
FROM dbo.ProtocolGroup g (NOLOCK)
ORDER BY g.Protocol_GroupKey

*/

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

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'StringSplit' AND XTYPE = 'TF')
BEGIN
	DROP FUNCTION StringSplit
END
GO

CREATE FUNCTION dbo.StringSplit (@string varchar(max), @splitcharacter char(1), @numofstring int)
RETURNS @splitstrings TABLE
(
	tempid int,
	variablestring varchar(1000)
)
AS
BEGIN 
	declare @start int, @end int, @tempstring varchar(1000), @len int, @i int
	set @start = 1
	set @end = 0
	set @i = 0
	set @len = len(@string) + 1
	while (@start < @len) and (@i < @numofstring)
	begin
		set @i = @i + 1
		set @end = charindex(@splitcharacter, @string, @start) + 1
		if (@end > @start)
		begin
			set @tempstring = substring(@string, @start, @end - @start - 1)
			set @start = @end
		end
		else
		begin
			set @tempstring = substring(@string, @start, @len - @start + 1)
			set @start = @len
		end
	
		if ltrim(rtrim(@tempstring)) <> ''
			insert into @splitstrings (tempid, variablestring) values (@i, @tempstring)
	end
	return
END
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

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'SSPx_ResortTemplateVersionItems' AND XTYPE = 'P')
	DROP PROCEDURE SSPx_ResortTemplateVersionItems
GO

CREATE PROCEDURE [dbo].[SSPx_ResortTemplateVersionItems]
	@TemplateVersionskey DECIMAL(20, 9)
AS
BEGIN
	DECLARE @NodeItem TABLE (
		TempId INT IDENTITY(100, 100),
		ItemId DECIMAL(20,9)
	)

	INSERT INTO @NodeItem (ItemId)
	SELECT TemplateVersionItemsKey 
	FROM dbo.SSPX_TemplateVersionItems (NOLOCK) 
	WHERE TemplateVersionsKey = @TemplateVersionskey 
	ORDER BY SortOrder

	UPDATE i SET SortOrder = n.TempId
	FROM dbo.SSPX_TemplateVersionItems i (NOLOCK)
		INNER JOIN @NodeItem n ON i.TemplateVersionItemsKey = n.ItemId
END
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'ItemChildrenId' AND XTYPE = 'TF')
BEGIN
	DROP FUNCTION ItemChildrenId
END
GO

CREATE FUNCTION dbo.ItemChildrenId (
	@ItemId INT,
	@ItemKey BIT
) 
RETURNS @NodeId TABLE (
	TempId INT IDENTITY(1, 1),
	ItemId INT,
	ParentId INT,
	LoopCount INT,
	SortOrder INT
)
AS 
BEGIN
	DECLARE @ItemCount INT = 0, @RowCount INT = 1, @LoopCount INT = 1
	IF (@ItemKey = 1)
		INSERT INTO @NodeId (ItemId, ParentId, SortOrder, LoopCount)
		SELECT TemplateVersionItemsKey, ParentTemplateVersionItemsKey, SortOrder, 0
		FROM dbo.SSPX_TemplateVersionItems (NOLOCK)
		WHERE TemplateVersionItemsKey = @ItemId AND Active = 1
	ELSE
		INSERT INTO @NodeId (ItemId, ParentId, SortOrder, LoopCount)
		SELECT TemplateVersionItemsKey, ParentTemplateVersionItemsKey, SortOrder, 0
		FROM dbo.SSPX_TemplateVersionItems (NOLOCK)
		WHERE TemplateVersionsKey = @ItemId AND Active = 1 AND ParentTemplateVersionItemsKey IS NULL

	SET @RowCount = (SELECT MAX(TempId) FROM @NodeId)
	WHILE (@ItemCount < @RowCount AND @LoopCount < 15)
	BEGIN
		INSERT INTO @NodeId (ItemId, ParentId, SortOrder, LoopCount)
		SELECT i.TemplateVersionItemsKey, i.ParentTemplateVersionItemsKey, i.SortOrder, @LoopCount
		FROM dbo.SSPX_TemplateVersionItems i (NOLOCK)
			INNER JOIN @NodeId n ON i.ParentTemplateVersionItemsKey = n.ItemId AND n.TempId > @ItemCount
		WHERE i.Active = 1

		SET @ItemCount = @RowCount
		SET @RowCount = (SELECT MAX(TempId) FROM @NodeId)
		SET @LoopCount = @LoopCount + 1
	END

	RETURN
END
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'SSPx_CopyItemNode' AND XTYPE = 'P')
	DROP PROCEDURE SSPx_CopyItemNode
GO

CREATE PROCEDURE [dbo].[SSPx_CopyItemNode] 
	@CopyItemNodeId INT,
	@PasteItemNodeId INT
AS
BEGIN
	DECLARE @NewVersionCKey INT, @SortOrder INT, @ParentItemId INT, @NextSortOrder INT
	DECLARE @NodeId TABLE (
		ItemId INT,
		ParentId INT,
		SortOrder INT
	)

	SELECT @NewVersionCKey = TemplateVersionsKey, @ParentItemId = ParentTemplateVersionItemsKey, @SortOrder = SortOrder
		FROM dbo.SSPX_TemplateVersionItems (NOLOCK) WHERE TemplateVersionItemsKey = @PasteItemNodeId

	SET @NextSortOrder = (SELECT TOP 1 SortOrder FROM SSPX_TemplateVersionItems (NOLOCK) 
		WHERE TemplateVersionsKey = @NewVersionCKey AND SortOrder > @SortOrder
		ORDER BY SortOrder)

	INSERT INTO @NodeId (ItemId, ParentId, SortOrder)
	SELECT ItemId, ParentId, SortOrder
	FROM dbo.ItemChildrenId(@CopyItemNodeId, 1) 
	ORDER BY SortOrder

	INSERT INTO dbo.SSPX_TemplateVersionItems (TemplateVersionsKey, TemplateVersionItemID, PrevTemplateVersionItemsKey, ChecklistTemplateItemCkey, ChecklistTemplateVersionCkey, U_ChecklistTemplateItemKey,
		NamespaceKey, VisibleText, RequiredLegacy, Condition, ItemTypesKey, SortOrder, DeprecatedFlag, DeprecatedDate, CreatedBy, CreatedDt, LastUpdated, LastUpdatedDt,
		Active, MinRepetitions, MaxRepetitions, Hidden, ReportText, DefaultValue, AuthorityValue, SelectionDisablesChildren, LongText, SelectionDisablesSiblings, 
		U_DefaultDisabled, Locked, DataTypeKey, AnswerMaxChars, AnswerMaxDecimals, AnswerMaxValue, AnswerMinValue, AnswerUnits, AnswerUnitsKey, U_PreviousItemCKey,
		U_ParentItemCKey, U_PrevChecklistTemplateItemCkey, U_OrigChecklistTemplateItemCkey, U_BaseCkey, PopUpNoteHTML, TextAfterAnswer, ControlTypeKey, Metadata,
		FSN, ConceptID, X_GID, GenericConceptID, X_LegacyCode, SNOMED_MatchTypeKey, SNOMED_NeedsReview, TemplateInjectionKey, SubTreeTemplateInjectionKey, X_ExternalKey,
		X_ChecklistTemplateVersionCKey, LocalKey, LocalDisplayHTMLText, ReportHTMLText, X_VisibleRTFText, EditorComment, ReportTypeKey, FormatTypeKey, DescriptionText, 
		X_SNOMEDinjectionCID, KeepWithNext, SkipConcept, X_ItemDependencyType, X_ItemXValues, X_ItemYKeys, X_ValidationRule, X_ValidationText, X_ValidationFunction, 
		X_NAACCR_ItemNum, X_NAACCR_ItemName, X_NAACCR_DisplayName, X_NAACCR_AnswerValue, X_NAACCR_AnswerKey, X_NAACCR_AnswerText, X_NAACCR_TempKey, ExtensionGroup, 
		ICDO3Morph, ICDO3Topo, ICDO3Grade, ICDO3Behav, ICDO3MatchTypeKey, ICDO3FullTerm, ICDO3NeedsReview, X_ICD9, X_ICD10, X_CPT, X_HCPCS, X_ATC, X_AHFS, X_USP, BeforeUpdate,
		AfterUpdate, OnChange, OnNotInList, OnExit, OnEnter, OnClick, HelpFile, HTMLHelp, ShortName, Status, mustImplement, X_mustAnswer, showInReport, U_Comments, type, 
		styleClass, responseRequired, omitWhenSelected, colTextDelimiter, numCols, storedCol, minSelections, maxSelections, ordered, selected, popUpText, linkText, L_linkText2,
		enabled, visible, ParentTemplateVersionItemsKey)
	SELECT @NewVersionCKey, TemplateVersionItemID, n.ItemId, ChecklistTemplateItemCkey, ChecklistTemplateVersionCkey, U_ChecklistTemplateItemKey,
		NamespaceKey, VisibleText, RequiredLegacy, Condition, ItemTypesKey, i.SortOrder, DeprecatedFlag, DeprecatedDate, CreatedBy, CreatedDt, LastUpdated, LastUpdatedDt,
		Active, MinRepetitions, MaxRepetitions, Hidden, ReportText, DefaultValue, AuthorityValue, SelectionDisablesChildren, LongText, SelectionDisablesSiblings, 
		U_DefaultDisabled, Locked, DataTypeKey, AnswerMaxChars, AnswerMaxDecimals, AnswerMaxValue, AnswerMinValue, AnswerUnits, AnswerUnitsKey, U_PreviousItemCKey,
		U_ParentItemCKey, U_PrevChecklistTemplateItemCkey, U_OrigChecklistTemplateItemCkey, U_BaseCkey, PopUpNoteHTML, TextAfterAnswer, ControlTypeKey, Metadata,
		FSN, ConceptID, X_GID, GenericConceptID, X_LegacyCode, SNOMED_MatchTypeKey, SNOMED_NeedsReview, TemplateInjectionKey, SubTreeTemplateInjectionKey, X_ExternalKey,
		X_ChecklistTemplateVersionCKey, LocalKey, LocalDisplayHTMLText, ReportHTMLText, X_VisibleRTFText, EditorComment, ReportTypeKey, FormatTypeKey, DescriptionText, 
		X_SNOMEDinjectionCID, KeepWithNext, SkipConcept, X_ItemDependencyType, X_ItemXValues, X_ItemYKeys, X_ValidationRule, X_ValidationText, X_ValidationFunction, 
		X_NAACCR_ItemNum, X_NAACCR_ItemName, X_NAACCR_DisplayName, X_NAACCR_AnswerValue, X_NAACCR_AnswerKey, X_NAACCR_AnswerText, X_NAACCR_TempKey, ExtensionGroup, 
		ICDO3Morph, ICDO3Topo, ICDO3Grade, ICDO3Behav, ICDO3MatchTypeKey, ICDO3FullTerm, ICDO3NeedsReview, X_ICD9, X_ICD10, X_CPT, X_HCPCS, X_ATC, X_AHFS, X_USP, BeforeUpdate,
		AfterUpdate, OnChange, OnNotInList, OnExit, OnEnter, OnClick, HelpFile, HTMLHelp, ShortName, 101, mustImplement, X_mustAnswer, showInReport, U_Comments, type, 
		styleClass, responseRequired, omitWhenSelected, colTextDelimiter, numCols, storedCol, minSelections, maxSelections, ordered, selected, popUpText, linkText, L_linkText2,
		enabled, visible, ParentTemplateVersionItemsKey
	FROM dbo.SSPX_TemplateVersionItems i (NOLOCK) 
	INNER JOIN @NodeId n ON i.TemplateVersionItemsKey = n.ItemId

	UPDATE i SET ParentTemplateVersionItemsKey = p.TemplateVersionItemsKey, [Status] = 1
	FROM dbo.SSPX_TemplateVersionItems i (NOLOCK)
	INNER JOIN @NodeId n ON i.PrevTemplateVersionItemsKey = n.ItemId
	INNER JOIN dbo.SSPX_TemplateVersionItems p (NOLOCK) ON p.PrevTemplateVersionItemsKey = n.ParentId
	WHERE i.TemplateVersionsKey = @NewVersionCKey AND i.[Status] = 101 AND p.TemplateVersionsKey = @NewVersionCKey AND p.[Status] = 101

	UPDATE dbo.SSPX_TemplateVersionItems SET ParentTemplateVersionItemsKey = @ParentItemId, [Status] = 1, SortOrder = (@SortOrder + (@NextSortOrder - @SortOrder) / 2)
	WHERE PrevTemplateVersionItemsKey = @CopyItemNodeId AND [status] = 101 AND TemplateVersionsKey = @NewVersionCKey
END
GO
