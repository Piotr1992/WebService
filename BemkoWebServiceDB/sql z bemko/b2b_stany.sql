CREATE FUNCTION [dbo].[b2b_stany] (@twrid int)
RETURNS TABLE
AS
RETURN 

select
Twr_GIDNumer
,case when Twr_stan - ZSbufor <= 0 then 0 else Twr_stan - ZSbufor end as Twr_Stan
,case when Twr_stan - ZSbufor <= 0 then (select min(convert(varchar(10), dateadd(d,ZaN_DataRealizacji,'1800-12-28T00:00:00'), 121))
from cdn.ZamNag
join cdn.ZamElem on ZaE_GIDNumer=ZaN_GIDNumer
where ZaN_ZamTyp=1152 and ZaN_Rodzaj =4 
and ZaE_MagNumer In (12,44)
and ZaN_Stan in (3,5)
and ZaE_TwrNumer=Twr_GIDNumer
and ZaN_DataRealizacji >= datediff(d,'1800-12-28T00:00:00',getdate())) else 'brak' end  DataDostawy
from(
select 
Twr_GIDNumer
,case when CDN.bmp_DokSumaStanowTowaru(Twr_GIDTyp,Twr_GIDFirma,Twr_GIDNumer,Twr_Typ,1,1,1,Twr_GIDFirma,12,0,0,0, DATEDIFF(d,'1800-12-28T00:00:00',getdate()), 0,3, 0,0,0,0,0,0,0,0) <0 then 0 else
CDN.bmp_DokSumaStanowTowaru(Twr_GIDTyp,Twr_GIDFirma,Twr_GIDNumer,Twr_Typ,1,1,1,Twr_GIDFirma,12,0,0,0, DATEDIFF(d,'1800-12-28T00:00:00',getdate()), 0,3, 0,0,0,0,0,0,0,0) end Twr_Stan
,0 as ZSBufor
from cdn.TwrKarty
where Twr_GIDNumer in (select * from dbo.bmp_TowaryGrupy(2527,''))
and case when @twrid=0 then Twr_GIDNumer else @twrid end = Twr_GIDNumer
)a