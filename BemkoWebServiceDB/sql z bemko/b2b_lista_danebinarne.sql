CREATE FUNCTION [dbo].[b2b_lista_danebinarne] ()
RETURNS TABLE
AS
RETURN 

select 
DAB_Id
,DAO_ObiTyp
,DAO_ObiNumer Gidnumer
,DAB_Kod
,DAB_Nazwa
,case when DAB_DBGId=1 then 'Zdjęcie' else 'Załącznik' end Typ
,DAB_Rozszerzenie
,convert(varchar(19),dateadd(second,DAB_CzasModyfikacji,'1990-01-01T00:00:00.000'),121) as "DAB_CzasModyfikacji"
from CDN.DaneObiekty
join cdn.DaneBinarne on DAO_DABId=DAB_Id
where  DAB_DBGId in (1,2,5,7)
and DAO_ObiTyp in (16)
and DAO_ObiNumer in (select * from  dbo.bmp_TowaryGrupy(2527,''))
and DAB_PKPrawa=1

union all

select 
DAB_Id
,DAO_ObiTyp
,DAO_ObiNumer Gidnumer
,DAB_Kod
,DAB_Nazwa
,case when DAB_DBGId=1 then 'Zdjęcie' else 'Załącznik' end Typ
,DAB_Rozszerzenie
,convert(varchar(19),dateadd(second,DAB_CzasModyfikacji,'1990-01-01T00:00:00.000'),121) as "DAB_CzasModyfikacji"
from CDN.DaneObiekty
join cdn.DaneBinarne on DAO_DABId=DAB_Id
where 
DAB_DBGId in (1,2,0,5,7)
and 
DAO_ObiTyp in (-16)
and DAO_ObiNumer in (select * from  dbo.bmp_GrupyGrupy(2527,''))
and DAB_PKPrawa=1

union all

select DAB_ID,DAO_ObiTyp,DAO_ObiNumer,DAB_Kod,DAB_Nazwa,'Logo',DAB_Rozszerzenie,convert(varchar(19),dateadd(second,DAB_CzasModyfikacji,'1990-01-01T00:00:00.000'),121) as "DAB_CzasModyfikacji"
from cdn.DaneBinarne 
join cdn.DaneObiekty on DAO_DABId=DAB_Id
where DAB_DBGId in (3,4)

