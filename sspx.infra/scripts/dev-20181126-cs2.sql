-- ***********************************************************************************************
-- script to add a few test users (CS2 2018-11-20)
--
-- Steps:
-- 
-- 1) in src/sspx.web run: dotnet ef database update
-- 2) if necessary, find/replace [aspnet-sspx-user-53bc9b9d-9d6a-45d4-8429-2a2761773502] below with name of Identity database on your machine (it contains AspNetUsers table)
-- 3) run the below
-- ***********************************************************************************************

DECLARE @SSPxUsersKey INT

BEGIN TRAN

	SELECT * FROM [PERC_eCC_SSPx].[dbo].[SSPX_Users] WHERE UserID IN ('admin', 'staff', 'test')
	SELECT SSPxUserKey, * FROM [aspnet-sspx-user-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].AspNetUsers

	IF NOT EXISTS( SELECT 1 FROM SSPX_Users WHERE UserID = 'admin')
	BEGIN
  		INSERT INTO SSPX_Users (DefaultNamespaceKey, userid, firstname, middlename, lastname, email, usertypekey, Qualifications)
		VALUES (1000043, 'admin', 'Test Admin', '', 'User', 'test_admin@cap.org', 6, 'MD')
	END
	
	SELECT @SSPxUsersKey = UsersKey FROM SSPX_Users WHERE UserID = 'admin'
	UPDATE [aspnet-sspx-user-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].AspNetUsers
	SET SSPxUserKey = @SSPxUsersKey
	WHERE UserName = 'admin'
	
	IF NOT EXISTS( SELECT 1 FROM SSPX_Users WHERE UserID = 'test')
	BEGIN
  		INSERT INTO SSPX_Users (DefaultNamespaceKey, userid, firstname, middlename, lastname, email, usertypekey, Qualifications)
		VALUES (1000043, 'test', 'Test', '', 'User', 'test@cap.org', 5, 'Engineer')
	END

	SELECT @SSPxUsersKey = UsersKey FROM SSPX_Users WHERE UserID = 'test'
	UPDATE [aspnet-sspx-user-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].AspNetUsers
	SET SSPxUserKey = @SSPxUsersKey
	WHERE UserName = 'test'

	IF NOT EXISTS( SELECT 1 FROM SSPX_Users WHERE UserID = 'staff')
	BEGIN
  		INSERT INTO SSPX_Users (DefaultNamespaceKey, userid, firstname, middlename, lastname, email, usertypekey, Qualifications)
		VALUES (1000043, 'staff', 'Test Staff', '', 'User', 'test_staff@cap.org', 2, 'CTR CCRP')
	END

	SELECT @SSPxUsersKey = UsersKey FROM SSPX_Users WHERE UserID = 'staff'
	UPDATE [aspnet-sspx-user-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].AspNetUsers
	SET SSPxUserKey = @SSPxUsersKey
	WHERE UserName = 'staff'

	SELECT * FROM [PERC_eCC_SSPx].[dbo].[SSPX_Users] WHERE UserID IN ('admin', 'staff', 'test')
	SELECT SSPxUserKey, * FROM [aspnet-sspx-user-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].AspNetUsers

COMMIT -- ROLLBACK
