Create FUNCTION [dbo].[b2b_cechy] (@twrid int)
RETURNS TABLE
AS
RETURN 

select 
Twr_GIDNumer
,AtK_ID atrKlasa
,Atr_Wartosc atrWartosc
,AtK_Nazwa atrNazwaPL
,(select TLM_Tekst from CDN.Tlumaczenia where TLM_Pole=2 and TLM_Jezyk=739 and TLM_Numer=AtK_ID and TLM_Typ=14416) atrNazwaEN
,(select dbo.b2b_Cyrillic2Unicode(TLM_Tekst) from CDN.Tlumaczenia where TLM_Pole=2 and TLM_Jezyk=950 and TLM_Numer=AtK_ID and TLM_Typ=14416) atrNazwaRU
from cdn.TwrKarty
join cdn.Atrybuty on Atr_ObiNumer=Twr_GIDNumer and Atr_ObiTyp=Twr_GIDTyp
join cdn.AtrybutyKlasy on Atr_AtkId=AtK_ID and AtK_iZam=1
where Twr_GIDNumer in (select * from dbo.bmp_TowaryGrupy(2527,''))
and case when @twrid=0 then Twr_GIDNumer else @twrid end = Twr_GIDNumer