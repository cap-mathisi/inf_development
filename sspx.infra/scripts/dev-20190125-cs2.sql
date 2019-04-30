-- ***********************************************************************************************
-- 2019-01-14 CS2. this script will:
--- 
-- 1) remove old SSPX_ProtocolGroupsMM testing rows (from before we had real data)
-- 2) add a unique index to SSPX_ProtocolGroupsMM to prevent duplicates
--
-- It is safe to run more than once.
-- ***********************************************************************************************

USE [PERC_eCC_SSPx]
GO

BEGIN TRAN

	-- BEFORE: IDs of duplicate rows
	SELECT
		DuplicateProtocolGroupsMMKey = MAX(ProtocolGroupsMMKey)
	FROM
		[SSPX_ProtocolGroupsMM]
	GROUP BY
		ProtocolsKey, ProtocolGroupKey
	HAVING
		COUNT(ProtocolGroupsMMKey) > 1
	ORDER BY ProtocolsKey


	-- delete any duplicates from [SSPX_ProtocolGroupsMM]
	DELETE FROM [SSPX_ProtocolGroupsMM]
	WHERE ProtocolGroupsMMKey IN(
		-- IDs of duplicate rows
		SELECT
			DuplicateProtocolGroupsMMKey = MAX(ProtocolGroupsMMKey)
		FROM
			[SSPX_ProtocolGroupsMM]
		GROUP BY
			ProtocolsKey, ProtocolGroupKey
		HAVING
			COUNT(ProtocolGroupsMMKey) > 1
	)

	-- prevent duplicates from being added in the future
	IF NOT EXISTS (
		SELECT 1
        FROM sys.indexes I
            INNER JOIN sys.tables T
                ON I.object_id = T.object_id
            INNER JOIN sys.schemas S
                ON S.schema_id = T.schema_id
        WHERE I.Name = 'IX_ProtocolGroupsMM_Unique' -- Index name
            AND T.Name = 'SSPX_ProtocolGroupsMM' -- Table name
            AND S.Name = 'dbo' --Schema Name
	)
	BEGIN
		SELECT 'creating IX_ProtocolGroupsMM_Unique index'
		CREATE UNIQUE NONCLUSTERED INDEX IX_ProtocolGroupsMM_Unique
			ON dbo.[SSPX_ProtocolGroupsMM] (ProtocolsKey, ProtocolGroupKey)
	END


	-- AFTER: confirm all duplicates are gone
	SELECT
		DuplicateProtocolGroupsMMKey = MAX(ProtocolGroupsMMKey)
	FROM
		[SSPX_ProtocolGroupsMM]
	GROUP BY
		ProtocolsKey, ProtocolGroupKey
	HAVING
		COUNT(ProtocolGroupsMMKey) > 1
	ORDER BY ProtocolsKey

COMMIT -- ROLLBACK