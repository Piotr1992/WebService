Create FUNCTION [dbo].[b2b_kontrahenci] ()
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
from cdn.KntKarty
where 
exists (select top 1 1 from cdn.Atrybuty where Atr_ObiNumer=knt_gidnumer and Atr_ObiTyp=32 and Atr_Wartosc='tak' and Atr_AtkId=11)