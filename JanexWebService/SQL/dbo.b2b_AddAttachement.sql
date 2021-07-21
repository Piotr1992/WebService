
create proc dbo.b2b_AddAttachement @pdf image,@size int,@ext varchar(10),@name Varchar(255),@code varchar(100), @obilp int, @obinumer int, @obityp int,@sys int=0,@dabid int output
as
begin
set nocount on
declare @documentnumber varchar(50)
declare @obigidfirma int
declare @filename varchar(255)=@name
declare @daoid int
declare @timestamp int=datediff(s,'1990-01-01T00:00:00.000',getdate())
declare @daopoz int
declare @openumer int=149
declare @type int=0
set @dabid=0

if @ext like 'jpg' set @type=342
if @ext like 'png' set @type=342
if @ext like 'bmp' set @type=342
if @ext like 'pdf' set @type=1119
if @ext like 'zip' set @type=345

if @size is not null and @type<>0
begin tran
    INSERT INTO CDN.DaneBinarne (DAB_Nazwa,DAB_TypId,DAB_Rozszerzenie,DAB_Rozmiar,DAB_Kod,DAB_Usuwac,DAB_CzasModyfikacji,DAB_OpeNumer,DAB_Archiwalny,DAB_Tlumaczenie,DAB_Jezyk,DAB_Aktywny,DAB_CzasArchiwizacji
			 , DAB_OpisArchiwizacji,DAB_PPPrawa,DAB_PKPrawa,DAB_eSklep,DAB_iMall,DAB_MobSpr,DAB_BI,DAB_Retail,DAB_DBGId,DAB_Systemowa,DAB_ProcID,DAB_ZewnetrznySys,DAB_ZewnetrznyId,DAB_CzasDodania,DAB_OpeNumerD,DAB_Dane) 
			 values (@filename,@type,@ext,@size,@filename,0,@timestamp,@openumer,0,-2985819,0,0,2000000000
			 ,'',0,0,0,0,0,0,1,0,@sys,0,0,0,@timestamp,@openumer,@pdf)  
			IF (@@ERROR=0) select @dabid=SCOPE_IDENTITY() 
    update cdn.DaneBinarne set dab_tlumaczenie=dab_id where dab_id=@dabid
    select @daopoz=isNull(MAX( DAO_pozycja ),0)+ 1 from CDN.DaneObiekty where DAO_ObiNumer = @obinumer and  DAO_ObiTyp = @obityp

    INSERT INTO CDN.DaneObiekty (DAO_DABId,DAO_ObiTyp,DAO_ObiNumer,DAO_ObiLp,DAO_ObiSubLp,DAO_Domyslna,DAO_Blokada,DAO_PPPrawa,DAO_PKPrawa
						  ,DAO_eSklep,DAO_iMall,DAO_MobSpr,DAO_BI,DAO_Systemowa,DAO_Pozycja,DAO_Retail,DAO_WMSZarzadzanie,DAO_WMSMagazynier) 
						  VALUES (@dabid,@obityp,@obinumer,0,0,0,1,0,0,0,0,0,0,@sys,@daopoz,1,0,0)
    IF (@@ERROR=0) SELECT @daoid=SCOPE_IDENTITY()
commit tran
--select @dabid
set nocount off
end
go
return


