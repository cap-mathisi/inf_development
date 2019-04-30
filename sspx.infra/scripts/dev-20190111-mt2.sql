USE [PERC_eCC_SSPx]
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'SSPx_CopyItemNode' AND XTYPE = 'P')
	DROP PROCEDURE SSPx_CopyItemNode
GO

CREATE PROCEDURE [dbo].[SSPx_CopyItemNode] 
	@CopyItemNodeId INT,
	@PasteItemNodeId INT,
	@IsCut BIT = 0
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
	
	IF @IsCut = 1
		UPDATE i SET Active = 0	FROM dbo.SSPX_TemplateVersionItems i (NOLOCK)
		INNER JOIN @NodeId n ON i.TemplateVersionItemsKey = n.ItemId	
END
GO
