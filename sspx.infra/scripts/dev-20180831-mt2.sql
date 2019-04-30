USE [PERC_eCC]
GO

IF NOT EXISTS (SELECT * FROM SYSINDEXES WHERE NAME = 'IX_SSP_VersionItems_work_VersionCKey')
	CREATE NONCLUSTERED INDEX IX_SSP_VersionItems_work_VersionCKey ON [dbo].SSP_VersionItems_work
	(
		[ChecklistTemplateVersionCKey] ASC
	)
GO

IF NOT EXISTS (SELECT * FROM SYSINDEXES WHERE NAME = 'IX_SSP_VersionItems_work_ParentItemCkey')
	CREATE INDEX IX_SSP_VersionItems_work_ParentItemCkey ON SSP_VersionItems_work (ParentItemCkey, SortOrder)
GO


IF NOT EXISTS (SELECT * FROM SYSINDEXES WHERE NAME = 'IX_SSP_Comments_ProtocolVersionCKey')
	CREATE INDEX IX_SSP_Comments_ProtocolVersionCKey ON dbo.SSP_Comments (ProtocolVersionCKey)
GO

IF NOT EXISTS (SELECT * FROM SYSINDEXES WHERE NAME = 'IX_SSP_VersionItems_work_ChecklistTemplateItemCkey')
	CREATE CLUSTERED INDEX IX_SSP_VersionItems_work_ChecklistTemplateItemCkey ON dbo.SSP_VersionItems_work (ChecklistTemplateItemCkey)
GO

/****** Object:  UserDefinedFunction [dbo].[ItemChildrenId]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'ItemChildrenId' AND XTYPE = 'TF')
BEGIN
	DROP FUNCTION ItemChildrenId
END
GO

CREATE FUNCTION dbo.ItemChildrenId (
	@ItemId DECIMAL(20, 9),
	@ItemKey BIT
) 
RETURNS @NodeId TABLE (
	TempId INT IDENTITY(1, 1),
	ItemId DECIMAL(20,9),
	ParentId DECIMAL(20,9),
	LoopCount INT,
	SortOrder INT
)
AS 
BEGIN
	DECLARE @ItemCount INT = 0, @RowCount INT = 1, @LoopCount INT = 1
	IF (@ItemKey = 1)
		INSERT INTO @NodeId (ItemId, ParentId, SortOrder, LoopCount)
		SELECT ChecklistTemplateItemCkey, ParentItemCkey, SortOrder, 0
		FROM dbo.SSP_VersionItems_work (NOLOCK)
		WHERE ChecklistTemplateItemCkey = @ItemId AND Active = 1 AND VisibleText <> '' 
	ELSE
		INSERT INTO @NodeId (ItemId, ParentId, SortOrder, LoopCount)
		SELECT ChecklistTemplateItemCkey, ParentItemCkey, SortOrder, 0
		FROM dbo.SSP_VersionItems_work (NOLOCK)
		WHERE ChecklistTemplateVersionCkey = @ItemId AND Active = 1 AND VisibleText <> '' AND ParentItemCKey IS NULL

	SET @RowCount = (SELECT MAX(TempId) FROM @NodeId)
	WHILE (@ItemCount < @RowCount AND @LoopCount < 15)
	BEGIN
		INSERT INTO @NodeId (ItemId, ParentId, SortOrder, LoopCount)
		SELECT i.ChecklistTemplateItemCkey, i.ParentItemCkey, i.SortOrder, @LoopCount
		FROM dbo.SSP_VersionItems_work i (NOLOCK)
			INNER JOIN @NodeId n ON i.ParentItemCKey = n.ItemId AND n.TempId > @ItemCount
		WHERE i.Active = 1 AND i.VisibleText <> ''

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
	@CopyItemNodeId DECIMAL(20, 9),
	@PasteItemNodeId DECIMAL(20, 9)
AS
BEGIN
	DECLARE @NewVersionCKey DECIMAL(20, 9), @SortOrder INT, @ParentItemId DECIMAL(20, 9), @NextSortOrder INT
	DECLARE @NodeId TABLE (
		ItemId DECIMAL(20,9),
		ParentId DECIMAL(20,9),
		SortOrder INT
	)

	SELECT @NewVersionCKey = ChecklistTemplateVersionCkey, @ParentItemId = ParentItemCkey, @SortOrder = SortOrder
		FROM dbo.SSP_VersionItems_work (NOLOCK) WHERE ChecklistTemplateItemCkey = @PasteItemNodeId

	SET @NextSortOrder = (SELECT TOP 1 SortOrder FROM SSP_VersionItems_work (NOLOCK) 
		WHERE ChecklistTemplateVersionCkey = @NewVersionCKey AND SortOrder > @SortOrder
		ORDER BY SortOrder)

	INSERT INTO @NodeId (ItemId, ParentId, SortOrder)
	SELECT ItemId, ParentId, SortOrder
	FROM dbo.ItemChildrenId(@CopyItemNodeId, 1) 
	ORDER BY SortOrder

	INSERT INTO dbo.SSP_VersionItems_work ([DraftVersion],[BaseVersion],[ReleaseCkey],[ChecklistTemplateItemCkey],[Namespace],[VisibleText],[Condition],[ItemTypeKey],
	[SortOrder],[AuthorityRequired],[DeprecatedFlag],[DeprecatedDate],[CreatedBy],[CreatedDt],[LastUpdated],[LastUpdatedDt],[Active],[MinRepetitions],
	[MaxRepetitions],[Hidden],[ReportText],[DefaultValue],[AuthorityValue],[SelectionDisablesChildren],[LongText],[SelectionDisablesSiblings],
	[DefaultDisabled],[Locked],[AnswerDataTypeKey],[AnswerMaxChars],[AnswerMaxDecimals],[AnswerMaxValue],[AnswerMinValue],[AnswerUnits],[AnswerUnits_Id],
	[PreviousItemCKey],[ParentItemCKey],[SourceCKey],[PrevChecklistTemplateItemCkey],[OrigChecklistTemplateItemCkey],[BaseCkey],[PopUpNoteHTML],
	[TextAfterConcept],[MetaData],[FSN],[ConceptID],[GID],[GenericConceptID],[LegacyCode],[SNOMED_MatchTypeKey],[SNOMED_NeedsReview],[TemplateInjectionCkey],
	[SubTreeTemplateInjectionCkey],[URI_Namespace],[ExternalKey],[ChecklistTemplateVersionCKey],[LocalKey],[LocalDisplayHTMLText],[PrintOutHTMLText],
	[VisibleRTFText],[EditorComment],[ReportTypeKey],[FormatTypeKey],[DescriptionText],[SNOMEDinjectionCID],[KeepWithNext],[SkipConcept],[ControlTypeKey],
	[ControlTip],[ItemDependencyType],[ItemXValues],[ItemYKeys],[ValidationRule],[ValidationText],[ValidationFunction],[NAACCR_ItemNum],[NAACCR_ItemName],
	[NAACCR_DisplayName],[NAACCR_AnswerValue],[NAACCR_AnswerKey],[NAACCR_AnswerText],[NAACCR_TempKey],[ExtensionGroup],[ComboBoundColumn],[ComboLimitToList],
	[ComboColumnCount],[ComboHeader],[ComboHeaderText],[ComboResizableCols],[ComboResizableRows],[ComboListRows],[ComboListWidth],[ComboAutoExpand],[ComboCol1],
	[ComboCol2],[ComboCol3],[ComboCol4],[ComboCol1Width],[ComboCol2Width],[ComboCol3Width],[ComboCol4Width],[LabelHeight],[LabelWidth],[LabelLeft],[LabelTop],
	[LabelBackColor],[LabelBackStyle],[LabelBorderStyle],[LabelBorderColor],[LabelBorderWidth],[LabelFont],[LabelFontBold],[LabelFontColor],[LabelFontItalic],
	[LabelFontSize],[LabelFontUnderline],[LabelTextAlign],[ControlDataSource],[ControlRowSource],[ControlRowSourceType],[ControlFormat],[ControlName],[ControlHeight],
	[ControlWidth],[ControlLeft],[ControlTop],[ControlBackColor],[ControlBackStyle],[ControlBorderStyle],[ControlBorderColor],[ControlBorderWidth],[ControlFont],
	[ControlFontBold],[ControlFontColor],[ControlFontItalic],[ControlFontSize],[ControlFontUnderline],[ControlHelpContextID],[ControlRightClickMenu],[ControlTag],
	[ControlTabStop],[ControlTabIndex],[ControlTextAlign],[HL7_MSH],[HL7_ORC],[HL7_OBR],[HL7_OBX_3_Qcode],[HL7_OBX_5_ACode],[HL7_OBX_5_1_Text],[LOINC],[ICDO3Morph],
	[ICDO3Topo],[ICDO3Grade],[ICDO3Behav],[ICDO3MatchTypeKey],[ICDO3FullTerm],[ICDO3NeedsReview],[ICD9],[ICD10],[CPT],[HCPCS],[ATC],[AHFS],[USP],[BeforeUpdate],
	[AfterUpdate],[OnChange],[OnNotInList],[OnExit],[OnEnter],[OnGotFocus],[OnLostFocus],[OnClick],[OnDblClick],[OnMouseDown],[OnMouseMove],[OnMouseUp],[OnKeyDown],
	[OnKeyUp],[OnKeyPress],[HelpFile],[HTMLHelp],[ShortName],[Status],[mustImplement],[mustAnswer],[showInReport],[Comments])
	SELECT [DraftVersion],[BaseVersion],[ReleaseCkey],[ChecklistTemplateItemCkey]  ,[Namespace],[VisibleText],[Condition],[ItemTypeKey],
	i.[SortOrder],[AuthorityRequired],[DeprecatedFlag],[DeprecatedDate],[CreatedBy],[CreatedDt],[LastUpdated],[LastUpdatedDt],[Active],[MinRepetitions],
	[MaxRepetitions],[Hidden],[ReportText],[DefaultValue],[AuthorityValue],[SelectionDisablesChildren],[LongText],[SelectionDisablesSiblings],
	[DefaultDisabled],[Locked],[AnswerDataTypeKey],[AnswerMaxChars],[AnswerMaxDecimals],[AnswerMaxValue],[AnswerMinValue],[AnswerUnits],[AnswerUnits_Id],
	[PreviousItemCKey],[ParentItemCKey],[SourceCKey],[PrevChecklistTemplateItemCkey],[OrigChecklistTemplateItemCkey],[BaseCkey],[PopUpNoteHTML],
	[TextAfterConcept],[MetaData],[FSN],[ConceptID],[GID],[GenericConceptID],[LegacyCode],[SNOMED_MatchTypeKey],[SNOMED_NeedsReview],[TemplateInjectionCkey],
	[SubTreeTemplateInjectionCkey],[URI_Namespace],[ExternalKey],@NewVersionCKey,[LocalKey],[LocalDisplayHTMLText],[PrintOutHTMLText],
	[VisibleRTFText],[EditorComment],[ReportTypeKey],[FormatTypeKey],[DescriptionText],[SNOMEDinjectionCID],[KeepWithNext],[SkipConcept],[ControlTypeKey],
	[ControlTip],[ItemDependencyType],[ItemXValues],[ItemYKeys],[ValidationRule],[ValidationText],[ValidationFunction],[NAACCR_ItemNum],[NAACCR_ItemName],
	[NAACCR_DisplayName],[NAACCR_AnswerValue],[NAACCR_AnswerKey],[NAACCR_AnswerText],[NAACCR_TempKey],[ExtensionGroup],[ComboBoundColumn],[ComboLimitToList],
	[ComboColumnCount],[ComboHeader],[ComboHeaderText],[ComboResizableCols],[ComboResizableRows],[ComboListRows],[ComboListWidth],[ComboAutoExpand],[ComboCol1],
	[ComboCol2],[ComboCol3],[ComboCol4],[ComboCol1Width],[ComboCol2Width],[ComboCol3Width],[ComboCol4Width],[LabelHeight],[LabelWidth],[LabelLeft],[LabelTop],
	[LabelBackColor],[LabelBackStyle],[LabelBorderStyle],[LabelBorderColor],[LabelBorderWidth],[LabelFont],[LabelFontBold],[LabelFontColor],[LabelFontItalic],
	[LabelFontSize],[LabelFontUnderline],[LabelTextAlign],[ControlDataSource],[ControlRowSource],[ControlRowSourceType],[ControlFormat],[ControlName],[ControlHeight],
	[ControlWidth],[ControlLeft],[ControlTop],[ControlBackColor],[ControlBackStyle],[ControlBorderStyle],[ControlBorderColor],[ControlBorderWidth],[ControlFont],
	[ControlFontBold],[ControlFontColor],[ControlFontItalic],[ControlFontSize],[ControlFontUnderline],[ControlHelpContextID],[ControlRightClickMenu],[ControlTag],
	[ControlTabStop],[ControlTabIndex],[ControlTextAlign],[HL7_MSH],[HL7_ORC],[HL7_OBR],[HL7_OBX_3_Qcode],[HL7_OBX_5_ACode],[HL7_OBX_5_1_Text],[LOINC],[ICDO3Morph],
	[ICDO3Topo],[ICDO3Grade],[ICDO3Behav],[ICDO3MatchTypeKey],[ICDO3FullTerm],[ICDO3NeedsReview],[ICD9],[ICD10],[CPT],[HCPCS],[ATC],[AHFS],[USP],[BeforeUpdate],
	[AfterUpdate],[OnChange],[OnNotInList],[OnExit],[OnEnter],[OnGotFocus],[OnLostFocus],[OnClick],[OnDblClick],[OnMouseDown],[OnMouseMove],[OnMouseUp],[OnKeyDown],
	[OnKeyUp],[OnKeyPress],[HelpFile],[HTMLHelp],[ShortName],101,[mustImplement],[mustAnswer],[showInReport],[Comments]
	FROM dbo.SSP_VersionItems_work i (NOLOCK) 
	INNER JOIN @NodeId n ON i.ChecklistTemplateItemCkey = n.ItemId

	UPDATE dbo.SSP_VersionItems_work SET ParentItemCKey = @ParentItemId, SortOrder = (@SortOrder + (@NextSortOrder - @SortOrder) / 2)
	WHERE ChecklistTemplateItemCkey = @CopyItemNodeId AND [status] = 101

	UPDATE i SET ParentItemCKey = p.ChecklistTemplateItemKey + CONVERT(DECIMAL(20,9),'.100004300') 
	FROM dbo.SSP_VersionItems_work i (NOLOCK)
	INNER JOIN @NodeId n ON i.ChecklistTemplateItemCkey = n.ItemId
	INNER JOIN dbo.SSP_VersionItems_work p (NOLOCK) ON p.ChecklistTemplateItemCkey = n.ParentId
	WHERE i.[Status] = 101 AND p.[Status] = 101

	UPDATE i SET [ChecklistTemplateItemCkey] = [ChecklistTemplateItemkey] + CONVERT(DECIMAL(20,9),'.100004300'), [Status] = 1
	FROM dbo.SSP_VersionItems_work i (NOLOCK)
	INNER JOIN @NodeId n ON i.ChecklistTemplateItemCkey = n.ItemId AND i.[status] = 101
END


