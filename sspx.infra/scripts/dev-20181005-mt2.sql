USE [PERC_eCC]
GO

IF EXISTS (SELECT * FROM SYSOBJECTS WHERE NAME = 'StringSplit' AND XTYPE = 'TF')
BEGIN
	DROP FUNCTION StringSplit
END
GO

CREATE FUNCTION dbo.StringSplit (@string varchar(max), @splitcharacter char(1), @numofstring int)
RETURNS @splitstrings TABLE
(
	tempid int,
	variablestring varchar(1000)
)
AS
BEGIN 
	declare @start int, @end int, @tempstring varchar(1000), @len int, @i int
	set @start = 1
	set @end = 0
	set @i = 0
	set @len = len(@string) + 1
	while (@start < @len) and (@i < @numofstring)
	begin
		set @i = @i + 1
		set @end = charindex(@splitcharacter, @string, @start) + 1
		if (@end > @start)
		begin
			set @tempstring = substring(@string, @start, @end - @start - 1)
			set @start = @end
		end
		else
		begin
			set @tempstring = substring(@string, @start, @len - @start + 1)
			set @start = @len
		end
	
		if ltrim(rtrim(@tempstring)) <> ''
			insert into @splitstrings (tempid, variablestring) values (@i, @tempstring)
	end
	return
END
GO

