-- List Of Specialities

USE [PERC_eCC_SSPx]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SSPX_ListOfSpecialty](
	[SpecialtyKey] [int] IDENTITY(1,1) NOT NULL,
	[NamespaceKey] [int] NOT NULL,
	[Specialty] [nchar](255) NOT NULL,
	[Description] [nchar](255) NULL,
	[SortOrder] [int] NULL,
	[CreatedBy] [decimal](20, 9) NOT NULL,
	[CreatedDt] [datetime] NULL,
	[LastUpdated] [decimal](20, 9) NOT NULL,
	[LastUpdatedDt] [datetime] NULL,
	[Active] [bit] NOT NULL,
	[TS] [timestamp] NOT NULL,
 CONSTRAINT [PK_SSPX_ListOfSpecialty] PRIMARY KEY CLUSTERED 
(
	[SpecialtyKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[SSPX_ListOfSpecialty] ON 

INSERT [dbo].[SSPX_ListOfSpecialty] ([SpecialtyKey], [NamespaceKey], [Specialty], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (1, 1000043,'Breast Pathology','Breast Pathology', 1, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfSpecialty] ([SpecialtyKey], [NamespaceKey], [Specialty], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (2, 1000043,'Cytopathology', 'Cytopathology', 2, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfSpecialty] ([SpecialtyKey], [NamespaceKey], [Specialty], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (3, 1000043, 'Gastrointestinal Pathology', 'Gastrointestinal Pathology', 3, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfSpecialty] ([SpecialtyKey], [NamespaceKey], [Specialty], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (4, 1000043, 'Genitourinary Pathology', 'Genitourinary Pathology', 4, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfSpecialty] ([SpecialtyKey], [NamespaceKey], [Specialty], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (5, 1000043, 'Gynecologic Pathology', 'Gynecologic Pathology', 5, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfSpecialty] ([SpecialtyKey], [NamespaceKey], [Specialty], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (6, 1000043, 'Neuropathology', 'Neuropathology', 6, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfSpecialty] ([SpecialtyKey], [NamespaceKey], [Specialty], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (7, 1000043, 'Pediatric Pathology', 'Pediatric Pathology', 7, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfSpecialty] ([SpecialtyKey], [NamespaceKey], [Specialty], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (8, 1000043, 'Perinatal Pathology', 'Perinatal Pathology', 8, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfSpecialty] ([SpecialtyKey], [NamespaceKey], [Specialty], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (9, 1000043, 'Renal Pathology', 'Renal Pathology', 9, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfSpecialty] ([SpecialtyKey], [NamespaceKey], [Specialty], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (10, 1000043, 'General Surgical Pathology', 'General Surgical Pathology', 10, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfSpecialty] ([SpecialtyKey], [NamespaceKey], [Specialty], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (11, 1000043, 'Cytogenetics', 'Cytogenetics', 11, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfSpecialty] ([SpecialtyKey], [NamespaceKey], [Specialty], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (12, 1000043, 'Hematopathology', 'Hematopathology', 12, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfSpecialty] ([SpecialtyKey], [NamespaceKey], [Specialty], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (13, 1000043, 'Molecular Pathology', 'Molecular Pathology', 13, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfSpecialty] ([SpecialtyKey], [NamespaceKey], [Specialty], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (14, 1000043, 'Dermatopathology', 'Dermatopathology', 14, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)

-- List Of Vendors
USE [PERC_eCC_SSPx]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SSPX_ListOfVendors](
	[VendorKey] [int] NOT NULL,
	[NamespaceKey] [int] NOT NULL,
	[Vendor] [nchar](255) NOT NULL,
	[Description] [nchar](255) NULL,
	[SortOrder] [int] NULL,
	[CreatedBy] [decimal](20, 9) NOT NULL,
	[CreatedDt] [datetime] NULL,
	[LastUpdated] [decimal](20, 9) NOT NULL,
	[LastUpdatedDt] [datetime] NULL,
	[Active] [bit] NOT NULL,
	[TS] [timestamp] NOT NULL,
 CONSTRAINT [PK_SSPX_ListOfVendors] PRIMARY KEY CLUSTERED 
(
	[VendorKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[SSPX_ListOfVendors] ([VendorKey], [NamespaceKey], [Vendor], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (1, 1000043, 'Cerner CoPathPlus', 'Cerner CoPathPlus', 1, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfVendors] ([VendorKey], [NamespaceKey], [Vendor], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (2, 1000043, 'Dolbey', 'Dolbey', 2, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfVendors] ([VendorKey], [NamespaceKey], [Vendor], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (3, 1000043, 'Epic Beaker', 'Epic Beaker', 3, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfVendors] ([VendorKey], [NamespaceKey], [Vendor], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (4, 1000043, 'mTuitive', 'mTuitive', 4, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfVendors] ([VendorKey], [NamespaceKey], [Vendor], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (5, 1000043, 'mTuitive-Cerner Millennium', 'mTuitive-Cerner Millennium', 5, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfVendors] ([VendorKey], [NamespaceKey], [Vendor], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (6, 1000043, 'mTuitive-Cortex', 'mTuitive-Cortex', 6, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfVendors] ([VendorKey], [NamespaceKey], [Vendor], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (7, 1000043, 'mTuitive-Meditech', 'mTuitive-Meditech', 7, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfVendors] ([VendorKey], [NamespaceKey], [Vendor], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (8, 1000043, 'mTuitive-Sunquest Copath', 'mTuitive-Sunquest Copath', 8, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfVendors] ([VendorKey], [NamespaceKey], [Vendor], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (9, 1000043, 'mTuitive-Other LIS systems', 'mTuitive-Other LIS systems', 9, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfVendors] ([VendorKey], [NamespaceKey], [Vendor], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (10, 1000043, 'Novopath', 'Novopath', 10, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfVendors] ([VendorKey], [NamespaceKey], [Vendor], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (11, 1000043, 'Psyche Systems', 'Psyche Systems', 11, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfVendors] ([VendorKey], [NamespaceKey], [Vendor], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (12, 1000043, 'Sunquest Powerpath', 'Sunquest Powerpath', 12, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
INSERT [dbo].[SSPX_ListOfVendors] ([VendorKey], [NamespaceKey], [Vendor], [Description], [SortOrder], [CreatedBy], [CreatedDt], [LastUpdated], [LastUpdatedDt], [Active]) VALUES (13, 1000043, 'Voicebrook', 'Voicebrook', 13, CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), CAST(1.100004300 AS Decimal(20, 9)), CAST(N'2019-01-10T00:30:16.680' AS DateTime), 1)
