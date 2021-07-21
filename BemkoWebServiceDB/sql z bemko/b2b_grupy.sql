CREATE FUNCTION [dbo].[b2b_grupy] ()
RETURNS TABLE
AS
RETURN 

select distinct 
case when p3.twg_gidtyp=16 then null else p3.TwG_Nazwa end as p1NazwaPL
,case when p3.twg_gidtyp=16 then null else p3.twg_kod end as p1KodPL
,(select TLM_Tekst from CDN.Tlumaczenia where TLM_Pole=2 and TLM_Jezyk=739 and TLM_Numer=p3.TwG_GIDNumer and TLM_Typ=-16) "p1NazwaEN"
,(select TLM_Tekst from CDN.Tlumaczenia where TLM_Pole=1 and TLM_Jezyk=739 and TLM_Numer=p3.TwG_GIDNumer and TLM_Typ=-16) "p1KodEN"
,(select dbo.b2b_Cyrillic2Unicode(TLM_Tekst) from CDN.Tlumaczenia where TLM_Pole=2 and TLM_Jezyk=950 and TLM_Numer=p3.TwG_GIDNumer and TLM_Typ=-16) "p1NazwaRU"
,(select dbo.b2b_Cyrillic2Unicode(TLM_Tekst) from CDN.Tlumaczenia where TLM_Pole=1 and TLM_Jezyk=950 and TLM_Numer=p3.TwG_GIDNumer and TLM_Typ=-16) "p1KodRU"
,case when p3.twg_gidtyp=16 then null else p3.TwG_GIDNumer end as "p1Id"
, case when p4.twg_gidtyp=16 then null else p4.TwG_Nazwa end as p2NazwaPL
, case when p4.twg_gidtyp=16 then null else p4.twg_kod end as p2KodPL
,(select TLM_Tekst from CDN.Tlumaczenia where TLM_Pole=2 and TLM_Jezyk=739 and TLM_Numer=p4.TwG_GIDNumer and TLM_Typ=-16) "p2NazwaEN"
,(select TLM_Tekst from CDN.Tlumaczenia where TLM_Pole=1 and TLM_Jezyk=739 and TLM_Numer=p4.TwG_GIDNumer and TLM_Typ=-16) "p2KodEN"
,(select dbo.b2b_Cyrillic2Unicode(TLM_Tekst) from CDN.Tlumaczenia where TLM_Pole=2 and TLM_Jezyk=950 and TLM_Numer=p4.TwG_GIDNumer and TLM_Typ=-16) "p2NazwaRU"
,(select dbo.b2b_Cyrillic2Unicode(TLM_Tekst) from CDN.Tlumaczenia where TLM_Pole=1 and TLM_Jezyk=950 and TLM_Numer=p4.TwG_GIDNumer and TLM_Typ=-16) "p2KodRU"
, case when p4.twg_gidtyp=16 then null else p4.TwG_GIDNumer end as "p2Id"
, case when p5.twg_gidtyp=16 then null else p5.twg_nazwa end as p3NazwaPL
, case when p5.twg_gidtyp=16 then null else p5.twg_kod end as p3KodPL
,(select TLM_Tekst from CDN.Tlumaczenia where TLM_Pole=2 and TLM_Jezyk=739 and TLM_Numer=p5.TwG_GIDNumer and TLM_Typ=-16) "p3NazwaEN"
,(select TLM_Tekst from CDN.Tlumaczenia where TLM_Pole=1 and TLM_Jezyk=739 and TLM_Numer=p5.TwG_GIDNumer and TLM_Typ=-16) "p3KodEN"
,(select dbo.b2b_Cyrillic2Unicode(TLM_Tekst) from CDN.Tlumaczenia where TLM_Pole=2 and TLM_Jezyk=950 and TLM_Numer=p5.TwG_GIDNumer and TLM_Typ=-16) "p3NazwaRU"
,(select dbo.b2b_Cyrillic2Unicode(TLM_Tekst) from CDN.Tlumaczenia where TLM_Pole=1 and TLM_Jezyk=950 and TLM_Numer=p5.TwG_GIDNumer and TLM_Typ=-16) "p3KodU"
, case when p5.twg_gidtyp=16 then null else p5.TwG_GIDNumer end as "p3Id"
from CDN.TwrGrupy as p1
left join CDN.TwrGrupy as p2 on p2.TwG_GrONumer=p1.TwG_GIDNumer
left join CDN.TwrGrupy as p3 on p3.twg_gronumer = case when p2.TwG_GIDTyp=16 then 0 else p2.TwG_GIDNumer end and p3.twg_grotyp=p2.twg_gidtyp
left join CDN.TwrGrupy as p4 on p4.twg_gronumer = case when p3.TwG_GIDTyp=16 then 0 else p3.TwG_GIDNumer end and p4.twg_grotyp=p3.twg_gidtyp
left join CDN.TwrGrupy as p5 on p5.twg_gronumer = case when p4.TwG_GIDTyp=16 then 0 else p4.TwG_GIDNumer end and p5.twg_grotyp=p4.twg_gidtyp

where p1.TwG_GrONumer=-1
and p2.TwG_GIDNumer=2527