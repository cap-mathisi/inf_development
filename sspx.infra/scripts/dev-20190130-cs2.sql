-- ***********************************************************************************************
-- 2019-01-30 CS2. this script adds VendorKey and Specialties column to SSPX_Users
--- 
-- It is safe to run more than once.
-- ***********************************************************************************************

USE PERC_eCC_SSPx

BEGIN TRAN

	IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SSPX_Users' AND COLUMN_NAME = 'VendorKey')
	BEGIN
		ALTER TABLE dbo.SSPX_Users
		ADD VendorKey INT NULL

		ALTER TABLE dbo.SSPX_Users
		ADD FOREIGN KEY (VendorKey) REFERENCES dbo.SSPx_ListOfVendors(VendorKey)

		SELECT 'added the following column:', * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SSPX_Users' AND COLUMN_NAME = 'VendorKey'
	END

	IF NOT EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SSPX_Users' AND COLUMN_NAME = 'Specialties')
	BEGIN
		ALTER TABLE dbo.SSPX_Users
		ADD Specialties NVARCHAR(MAX) NULL

		SELECT 'added the following column:', * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SSPX_Users' AND COLUMN_NAME = 'Specialties'
	END

COMMIT --ROLLBACK
