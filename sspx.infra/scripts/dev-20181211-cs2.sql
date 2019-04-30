-- ***********************************************************************************************
-- script to populate SSPx with some test data (CS2 2018-11-14)
-- (safe to run multiple times, will not create duplicates)
--
-- SSPX_ProtocolGroupsMM: so we can see protocols in outer navigation menu, as well as Admin :: Create Protocol 
-- SSPX_ProtocolVersionUserRoles: so we can filter by roles in outer navigation menu
-- SSPX_RolePermissionsMM: so we have data for our permissions service layer
-- ***********************************************************************************************

-- Add ProtocolGroupsMMKey rows 
BEGIN TRAN

	-- SELECT * FROM [SSPX_ProtocolGroupsMM]

	-- TODO: remove [ProtocolGroupsMMKey] if we add IDENTITY to it
	IF NOT EXISTS( SELECT 1 FROM [dbo].[SSPX_ProtocolGroupsMM] WHERE ProtocolGroupKey=1 AND ProtocolsKey=5)
	BEGIN
		INSERT INTO [dbo].[SSPX_ProtocolGroupsMM] ([ProtocolGroupsMMKey], [ProtocolGroupKey], [ProtocolsKey]) VALUES ( 1000, 1, 5 )
	END
	IF NOT EXISTS( SELECT 1 FROM [dbo].[SSPX_ProtocolGroupsMM] WHERE ProtocolGroupKey=8 AND ProtocolsKey=7)
	BEGIN
		INSERT INTO [dbo].[SSPX_ProtocolGroupsMM] ([ProtocolGroupsMMKey], [ProtocolGroupKey], [ProtocolsKey]) VALUES ( 1001, 8, 7 )
	END
	IF NOT EXISTS( SELECT 1 FROM [dbo].[SSPX_ProtocolGroupsMM] WHERE ProtocolGroupKey=11 AND ProtocolsKey=52)
	BEGIN
		INSERT INTO [dbo].[SSPX_ProtocolGroupsMM] ([ProtocolGroupsMMKey], [ProtocolGroupKey], [ProtocolsKey]) VALUES ( 1002, 11, 52 )
	END
	IF NOT EXISTS( SELECT 1 FROM [dbo].[SSPX_ProtocolGroupsMM] WHERE ProtocolGroupKey=5 AND ProtocolsKey=27)
	BEGIN
		INSERT INTO [dbo].[SSPX_ProtocolGroupsMM] ([ProtocolGroupsMMKey], [ProtocolGroupKey], [ProtocolsKey]) VALUES ( 1003, 5, 27 )
	END
	IF NOT EXISTS( SELECT 1 FROM [dbo].[SSPX_ProtocolGroupsMM] WHERE ProtocolGroupKey=5 AND ProtocolsKey=36)
	BEGIN
		INSERT INTO [dbo].[SSPX_ProtocolGroupsMM] ([ProtocolGroupsMMKey], [ProtocolGroupKey], [ProtocolsKey]) VALUES ( 1004, 5, 36 )
	END
	
	SELECT * FROM [SSPX_ProtocolGroupsMM]

COMMIT -- ROLLBACK

-- add SSPX_ProtocolVersionUserRoles rows
BEGIN TRAN

	-- SELECT * FROM [SSPX_ProtocolVersionUserRoles]

	DECLARE @AdminUserKey INT
	SELECT @AdminUserKey = UsersKey
	FROM [dbo].[SSPX_Users] WHERE UserID = 'admin'

	IF NOT EXISTS( SELECT 1 FROM [dbo].[SSPX_ProtocolVersionUserRoles] WHERE RolesKey=1 AND UsersKey=@AdminUserKey AND ProtocolVersionsKey=205)
	BEGIN
		INSERT INTO [dbo].[SSPX_ProtocolVersionUserRoles] (RolesKey, UsersKey, ProtocolVersionsKey, CreatedBy)
		VALUES( 1, @AdminUserKey, 205, 1.100004300)
		-- Breast Invasive 3.3.0.0
	END	
	IF NOT EXISTS( SELECT 1 FROM [dbo].[SSPX_ProtocolVersionUserRoles] WHERE RolesKey=3 AND UsersKey=@AdminUserKey AND ProtocolVersionsKey=154)
	BEGIN
		INSERT INTO [dbo].[SSPX_ProtocolVersionUserRoles] (RolesKey, UsersKey, ProtocolVersionsKey, CreatedBy)
		VALUES( 3, @AdminUserKey, 154, 1.100004300)
	END	
	IF NOT EXISTS( SELECT 1 FROM [dbo].[SSPX_ProtocolVersionUserRoles] WHERE RolesKey=1 AND UsersKey=@AdminUserKey AND ProtocolVersionsKey=207)
	BEGIN
		INSERT INTO [dbo].[SSPX_ProtocolVersionUserRoles] (RolesKey, UsersKey, ProtocolVersionsKey, CreatedBy)
		VALUES( 1, @AdminUserKey, 207, 1.100004300)
	END	
	IF NOT EXISTS( SELECT 1 FROM [dbo].[SSPX_ProtocolVersionUserRoles] WHERE RolesKey=3 AND UsersKey=@AdminUserKey AND ProtocolVersionsKey=124)
	BEGIN
		INSERT INTO [dbo].[SSPX_ProtocolVersionUserRoles] (RolesKey, UsersKey, ProtocolVersionsKey, CreatedBy)
		VALUES( 3, @AdminUserKey, 124, 1.100004300)
	END	
	IF NOT EXISTS( SELECT 1 FROM [dbo].[SSPX_ProtocolVersionUserRoles] WHERE RolesKey=1 AND UsersKey=@AdminUserKey AND ProtocolVersionsKey=107)
	BEGIN
		INSERT INTO [dbo].[SSPX_ProtocolVersionUserRoles] (RolesKey, UsersKey, ProtocolVersionsKey, CreatedBy)
		VALUES( 1, @AdminUserKey, 107, 1.100004300)
	END

	DECLARE @TestUserKey_AlexGoel INT
	SELECT @TestUserKey_AlexGoel = UsersKey
	FROM [dbo].[SSPX_Users] WHERE UserID = 'test' and FirstName = 'Alex' and LastName = 'Goel'

	IF NOT EXISTS( SELECT 1 FROM [dbo].[SSPX_ProtocolVersionUserRoles] WHERE RolesKey=1 AND UsersKey=@TestUserKey_AlexGoel AND ProtocolVersionsKey=205)
	BEGIN
		-- Author for Breast Invasive 3.3.0.0
		INSERT INTO [dbo].[SSPX_ProtocolVersionUserRoles] (RolesKey, UsersKey, ProtocolVersionsKey, CreatedBy)
		VALUES( 1, @TestUserKey_AlexGoel, 205, 1.100004300)
	END

	IF NOT EXISTS( SELECT 1 FROM [dbo].[SSPX_ProtocolVersionUserRoles] WHERE RolesKey=3 AND UsersKey=@TestUserKey_AlexGoel AND ProtocolVersionsKey=124)
	BEGIN
		-- Reviewer for Urethra 3.2.1.0
		INSERT INTO [dbo].[SSPX_ProtocolVersionUserRoles] (RolesKey, UsersKey, ProtocolVersionsKey, CreatedBy)
		VALUES( 3, @TestUserKey_AlexGoel, 124, 1.100004300)
	END	

	SELECT * FROM [SSPX_ProtocolVersionUserRoles]

COMMIT -- ROLLBACK

-- add SSPX_RolePermissionsMM rows
BEGIN TRAN

	-- SELECT * FROM [SSPX_RolePermissionsMM]

	-- author
	IF NOT EXISTS( SELECT 1 FROM [dbo].[SSPX_RolePermissionsMM] WHERE RolesKey=1 AND PermissionsKey=7)
	BEGIN
		INSERT INTO [dbo].[SSPX_RolePermissionsMM] (RolesKey, PermissionsKey) VALUES (1, 7)
	END
	IF NOT EXISTS( SELECT 1 FROM [dbo].[SSPX_RolePermissionsMM] WHERE RolesKey=1 AND PermissionsKey=4)
	BEGIN
		INSERT INTO [dbo].[SSPX_RolePermissionsMM] (RolesKey, PermissionsKey) VALUES (1, 4)
	END
	IF NOT EXISTS( SELECT 1 FROM [dbo].[SSPX_RolePermissionsMM] WHERE RolesKey=1 AND PermissionsKey=8)
	BEGIN
		INSERT INTO [dbo].[SSPX_RolePermissionsMM] (RolesKey, PermissionsKey) VALUES (1, 8)
	END
	IF NOT EXISTS( SELECT 1 FROM [dbo].[SSPX_RolePermissionsMM] WHERE RolesKey=1 AND PermissionsKey=9)
	BEGIN
		INSERT INTO [dbo].[SSPX_RolePermissionsMM] (RolesKey, PermissionsKey) VALUES (1, 9)
	END
	IF NOT EXISTS( SELECT 1 FROM [dbo].[SSPX_RolePermissionsMM] WHERE RolesKey=1 AND PermissionsKey=10)
	BEGIN
		INSERT INTO [dbo].[SSPX_RolePermissionsMM] (RolesKey, PermissionsKey) VALUES (1, 10)
	END

	-- reviewer
	IF NOT EXISTS( SELECT 1 FROM [dbo].[SSPX_RolePermissionsMM] WHERE RolesKey=3 AND PermissionsKey=8)
	BEGIN
		INSERT INTO [dbo].[SSPX_RolePermissionsMM] (RolesKey, PermissionsKey) VALUES (3, 8)
	END

	SELECT * FROM [SSPX_RolePermissionsMM]

COMMIT -- ROLLBACK