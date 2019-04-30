-- ***********************************************************************************************
-- 2019-01-29 CS2. this script updates identity users created before today to:
--- 
-- 1) enable Lockout functionality
-- 2) mark their emails as "confirmed" (a pre-req for lockout, doing this to make testing easier)
--
-- It is safe to run more than once.
-- ***********************************************************************************************

USE [aspnet-sspx-user-53bc9b9d-9d6a-45d4-8429-2a2761773502]

BEGIN TRAN
	
	UPDATE [aspnet-sspx-user-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].AspNetUsers
	SET
		EmailConfirmed = 1,
		LockoutEnabled = 1		
	WHERE UserName IN(
		-- identity users linked to SSPX_Users created before 2019-01-29
		SELECT iu.UserName FROM [PERC_eCC_SSPx].[dbo].[SSPX_Users] u
		INNER JOIN [aspnet-sspx-user-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].AspNetUsers iu
			ON u.UsersKey = iu.SSPxUserKey
		WHERE COALESCE(u.CreatedDt, '20190128') < '20190129'
	)

	SELECT EmailConfirmed, LockoutEnabled, UserName
	FROM [aspnet-sspx-user-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].AspNetUsers
	ORDER BY EmailConfirmed, LockoutEnabled, UserName

COMMIT -- ROLLBACK