/****** Object:  StoredProcedure [dbo].[SSPX_ImportCommentToCurrentVersion]    Script Date: 3/19/2019 1:27:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SSPX_ImportCommentToCurrentVersion] 
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

/****** Object:  StoredProcedure [dbo].[SSPX_GetAllVersionComments]    Script Date: 3/19/2019 1:26:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER Procedure [dbo].[SSPX_GetAllVersionComments] 
(
@ProtocolKey int,
@WorkingProtocolVersionKey int
)
As 
Begin


Select P.ProtocolsKey,
	   PV.ProtocolVersionsKey, 
       PV.ProtocolVersion,
	   PVUR.UsersKey ,
       U.FirstName + ' '+ U.LastName as [Name] ,
	   COUNT(VC.Comment) as CommentsCount
from SSPX_Protocols P
Inner Join SSPX_ProtocolVersions PV on P.ProtocolsKey = PV.Protocolskey 
left outer Join SSPX_ProtocolVersionUserRoles PVUR on PV.ProtocolVersionsKey = PVUR.ProtocolVersionsKey 
left outer Join SSPX_ListOfRoles LR on PVUR.RolesKey = LR.RolesKey and LR.RolesKey = 3
left outer Join SSPX_Users U on PVUR.UsersKey = U.UsersKey 
left outer Join SSPX_ProtocolVersionComments VC  on VC.ProtocolVersionsKey = PV.ProtocolVersionsKey and VC.UsersKey = PVUR.UsersKey  
Where P.ProtocolsKey = @ProtocolKey and PV.ProtocolVersionsKey != @WorkingProtocolVersionKey
Group by P.ProtocolsKey,
		 PV.ProtocolVersionsKey, 
		 PV.ProtocolVersion,
		 PVUR.UsersKey,
		 U.FirstName + ' '+ U.LastName

End

