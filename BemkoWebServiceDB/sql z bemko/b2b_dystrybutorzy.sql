CREATE FUNCTION [dbo].[b2b_dystrybutorzy] ()
RETURNS TABLE
AS
RETURN 
select 
Knt_GIDNumer
,Knt_Akronim
,Knt_Nazwa1
,Knt_Nazwa2
,Knt_Nazwa3
,Knt_Nip
,Knt_KodP
,Knt_Miasto
,Knt_Adres
,Knt_Ulica
,Knt_Wojewodztwo
,Knt_Telefon1
,Knt_EMail
,Knt_URL
,'Dystrybutor' Znacznik
,0 Dab_Id
from cdn.KntKarty
where 
exists (select top 1 1 from cdn.Atrybuty where Atr_ObiNumer=knt_gidnumer and Atr_ObiTyp=32 and Atr_AtkId=86 and Atr_Wartosc='tak')

union all

select DAO_ObiNumer,Knt_Akronim,'','','','','','','','','','',Knt_EMail,Knt_URL,case when DAB_DBGId = 4 then 'e-partner' else 'grupa zakupowa' end,DAB_ID
from cdn.DaneBinarne 
join cdn.DaneObiekty on DAO_DABId=DAB_Id
join cdn.KntKarty on Knt_GIDNumer=DAO_ObiNumer and Knt_GIDTyp=DAO_ObiTyp
where DAB_DBGId in (3,4)
