-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
 Alter TABLE SSPX_ProtocolVersions
ADD ReviewStartDate datetime null;
go
 Alter TABLE SSPX_ProtocolVersions
ADD ReviewEndDate datetime null;
go
 Alter TABLE SSPX_ProtocolVersions
ADD CustomMessage nvarchar(
500) null;
go
 Alter TABLE SSPX_ProtocolVersionUserRoles 
ADD Assignreviewerflag BIT default 'FALSE' ;
go

CREATE FUNCTION [dbo].[fn_split_string_to_column] (
    @string NVARCHAR(MAX),
    @delimiter CHAR(1)
    )
RETURNS @out_put TABLE (
    [column_id] INT IDENTITY(1, 1) NOT NULL,
    [value] NVARCHAR(MAX)
    )
AS
BEGIN
    DECLARE @value NVARCHAR(MAX),
        @pos INT = 0,
        @len INT = 0

    SET @string = CASE 
            WHEN RIGHT(@string, 1) != @delimiter
                THEN @string + @delimiter
            ELSE @string
            END

    WHILE CHARINDEX(@delimiter, @string, @pos + 1) > 0
    BEGIN
        SET @len = CHARINDEX(@delimiter, @string, @pos + 1) - @pos
        SET @value = SUBSTRING(@string, @pos, @len)

        INSERT INTO @out_put ([value])
        SELECT LTRIM(RTRIM(@value)) AS [column]

        SET @pos = CHARINDEX(@delimiter, @string, @pos + @len) + 1
    END

    RETURN
END

Go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:           Mohanakrishnan A
-- Create date: 3/14/2019
-- Description:      <Description,,>
-- =============================================
Create PROCEDURE SSPX_ImportCommentToCurrentVersion 
       -- Add the parameters for the stored procedure here
       @FromProtocolVersionKey INT,
       @ToProtocolVersionKey INT,
       @ReviwerList nvarchar(100) = null,
       @ErrorMsg nvarchar(500) = null
AS
BEGIN

          If(@ReviwerList is not null)
          Begin

DECLARE @ReviewerTable TABLE (ID INT, ReviewerID INT)
       Insert INTO @ReviewerTable (ID, ReviewerID)  
          select * FROM [dbo].[fn_split_string_to_column](@ReviwerList,',')

DECLARE @LoopCounter INT = 0, @TotalRecords INT;
SET @TotalRecords = (Select MAX(ID) from @ReviewerTable) 
 
WHILE(@LoopCounter < @TotalRecords)
BEGIN
SET @LoopCounter  = @LoopCounter  + 1
DECLARE @RID int
   SELECT @RID =  ReviewerID
   FROM @ReviewerTable WHERE Id = @LoopCounter

       If (( @ToProtocolVersionKey is not null) and (@FromProtocolVersionKey is not null))
       Begin
              Insert into [dbo].[SSPX_ProtocolVersionComments]
              ( ProtocolVersionsKey, Comment, Active, CommentType, UsersKey, LastUpdated, DateCreated )
              select  @ToProtocolVersionKey as ProtocolVersionsKey, Comment, Active, CommentType, UsersKey, LastUpdated, DateCreated
              from [dbo].[SSPX_ProtocolVersionComments]
              where ProtocolVersionsKey = @FromProtocolVersionKey and UsersKey = @RID

              Insert into dbo.SSPX_TemplateVersionItemsReviewComments        
              (TemplateVersionItemsKey, Comment, UsersKey, DateCreated, Active)
              SELECT @ToProtocolVersionKey as TemplateVersionItemsKey, Comment, UsersKey, DateCreated, Active
              FROM            dbo.SSPX_TemplateVersionItemsReviewComments
              where TemplateVersionItemsKey = @FromProtocolVersionKey and UsersKey = @RID
       End
          END
END
END
GO





-------------------------------------------------------------------------


USE [PERC_eCC_SSPx_new]
GO

/****** Object:  StoredProcedure [dbo].[SSPX_GetAllVersionComments]    Script Date: 3/15/2019 6:37:50 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[SSPX_GetAllVersionComments] 
(
@ProtocolKey int
)
As 
Begin

Select P.ProtocolsKey, PV.ProtocolVersionsKey, PV.ProtocolVersion, PVUR.UsersKey, 
       U.FirstName + ' '+ U.LastName as [Name], 
	   COUNT(VC.Comment) as CommentsCount
from SSPX_Protocols P
Inner Join SSPX_ProtocolVersions PV on P.ProtocolsKey = PV.Protocolskey 
inner Join SSPX_ProtocolVersionUserRoles PVUR on PV.ProtocolVersionsKey = PVUR.ProtocolVersionsKey
Inner Join SSPX_ListOfRoles LR on PVUR.RolesKey = LR.RolesKey
Inner Join SSPX_Users U on PVUR.UsersKey = U.UsersKey 
left outer Join SSPX_ProtocolVersionComments VC  on VC.ProtocolVersionsKey = PV.ProtocolVersionsKey and VC.UsersKey = PVUR.UsersKey  
Where P.ProtocolsKey = @ProtocolKey and LR.RolesKey = 3
Group by P.ProtocolsKey,PV.ProtocolVersionsKey, PV.ProtocolVersion, PVUR.UsersKey, U.FirstName + ' '+ U.LastName

End
GO

