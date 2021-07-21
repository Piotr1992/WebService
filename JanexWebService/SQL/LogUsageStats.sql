
create table dbo.bmp_LogUsage (
Id BIGINT NOT NULL IDENTITY(1,1)
,UsageTime datetime default getdate()
,ApplicationName varchar(50)
,TypeName varchar(200)
,MethodName varchar(200)
,Date as cast(UsageTime as Date)
,Hour as datepart(hh,UsageTime)
,Elapsed bigint
,Parameters nvarchar(max)

	CONSTRAINT PK_bmp_LogUsage_Id PRIMARY KEY CLUSTERED (Id DESC)
)
go
create procedure dbo.bmp_spLogUsage (@appName varchar(50),@typeName varchar(200),@methodName varchar(200),@elapsed bigint,@parameters nvarchar(max))
as
begin
insert into dbo.bmp_LogUsage (UsageTime,ApplicationName,TypeName,MethodName,Elapsed,Parameters)
values (getdate(),@appName,@typeName,@methodName,@elapsed,@parameters)
end
go
create view dbo.bmp_vLogUsageStatsHourly
as
select Date,Hour,TypeName,MethodName,Count(*) UsageCount,avg(Elapsed) AverageElapsed,min(Elapsed) MinElapsed,max(Elapsed) MaxElapsed
from dbo.bmp_LogUsage
group by Date,Hour,TypeName,MethodName
go
create view dbo.bmp_vLogUsageStatsDaily
as
select Date,TypeName,MethodName,Count(*) UsageCount,avg(Elapsed) AverageElapsed,min(Elapsed) MinElapsed,max(Elapsed) MaxElapsed
from dbo.bmp_LogUsage
group by Date,TypeName,MethodName
go
alter procedure dbo.bmp_spLogUsageClearParameters
as
begin
	declare @maxDateTime datetime = dateadd(HH,-120,getdate())
	declare @maxWZDateTime datetime = dateadd(HH,-48,getdate())
--declare @maxDateTime datetime = dateadd(HH,-10,getdate())
--declare @maxWZDateTime datetime = dateadd(HH,-2,getdate())


if exists (select 1 from dbo.bmp_LogUsage where UsageTime<@maxWZDateTime and Parameters like 'WZ%')
	update dbo.bmp_LogUsage set Parameters='' where UsageTime<@maxWZDateTime and Parameters like 'WZ%'
if exists (select 1 from dbo.bmp_LogUsage where UsageTime<@maxDateTime and parameters<>'') 
	update dbo.bmp_LogUsage set Parameters='' where UsageTime<@maxDateTime and parameters<>''
end