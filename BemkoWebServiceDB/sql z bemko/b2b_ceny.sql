CREATE FUNCTION [dbo].[b2b_ceny] (@twrid int)
RETURNS TABLE
AS
RETURN 

select 
Twr_GIDNumer
,(select TwC_Wartosc from cdn.TwrCeny where TwC_TwrNumer=Twr_GIDNumer and TwC_TwrLp=1) Twr_CenaKatalogowa
,Twr_StawkaPodSpr
from cdn.TwrKarty
left join cdn.TwrOpisy on TwO_TwrNumer=Twr_GIDNumer and TwO_TwrLp=1
where Twr_GIDNumer in (select * from dbo.bmp_TowaryGrupy(2527,''))
and case when @twrid=0 then Twr_GIDNumer else @twrid end = Twr_GIDNumer