CREATE FUNCTION [dbo].[b2b_towary] ()
RETURNS TABLE
AS
RETURN 

select 
Twr_GIDNumer
,Twr_Kod
,Twr_Nazwa
,Twr_Jm
,'' as Twr_Symbol
,Twr_Ean
,TwO_Opis Twr_Opis
,(select TLM_Tekst from CDN.Tlumaczenia where TLM_Pole=2 and TLM_Jezyk=739 and TLM_Numer=Twr_GIDNumer and TLM_Typ=Twr_GIDTyp) Twr_NazwaENG
,(select TwO_Opis from  CDN.TwrOpisy where TwO_Jezyk=739 and TwO_TwrNumer=Twr_GIDNumer) Twr_OpisENG
,(select dbo.b2b_Cyrillic2Unicode(TLM_Tekst) from CDN.Tlumaczenia where TLM_Pole=2 and TLM_Jezyk=950 and TLM_Numer=Twr_GIDNumer and TLM_Typ=Twr_GIDTyp) Twr_NazwaRUS
,(select dbo.b2b_Cyrillic2Unicode(TwO_Opis) from  CDN.TwrOpisy where TwO_Jezyk=950 and TwO_TwrNumer=Twr_GIDNumer) Twr_OpisRUS
,[dbo].[bmp_b2bGrupyTowaru](Twr_GIDNumer,1,1) Twr_GrupaP1id
,[dbo].[bmp_b2bGrupyTowaru](Twr_GIDNumer,2,1) Twr_GrupaP2id
,[dbo].[bmp_b2bGrupyTowaru](Twr_GIDNumer,3,1) Twr_GrupaP3id
from cdn.TwrKarty
left join cdn.TwrOpisy on TwO_TwrNumer=Twr_GIDNumer and TwO_TwrLp=1
where Twr_GIDNumer in (select * from dbo.bmp_TowaryGrupy(2527,''))
and Twr_Archiwalny=0