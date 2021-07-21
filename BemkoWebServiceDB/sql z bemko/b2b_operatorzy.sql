CREATE FUNCTION [dbo].[b2b_operatorzy] ()
RETURNS TABLE
AS
RETURN

select 
KnS_Nazwa
,KnS_EMail
,KnS_HasloOsoby
,knd.Knt_GIDNumer Knd_Gidnumer
,knd.Knt_Akronim Knd_Akronim
,knd.Knt_Nazwa1 Knd_Nazwa1
,knd.Knt_Nazwa2 Knd_Nazwa2
,knd.Knt_Nazwa3 Knd_Nazwa3
,knd.Knt_Nip Knd_NIP
,knd.Knt_Miasto Knd_Miasto
,knd.Knt_KodP Knd_KodP
,knd.Knt_Ulica Knd_Ulica
,knd.Knt_Adres Knd_Adres
,isnull(knp.Knt_GIDNumer,knd.Knt_GIDNumer) Knp_Gidnumer
,isnull(knp.Knt_Akronim,knd.Knt_Akronim) Knp_Akronim
,isnull(knp.Knt_Nazwa1,knd.Knt_Nazwa1) Knp_Nazwa1
,isnull(knp.Knt_Nazwa2,knd.Knt_Nazwa2) Knp_Nazwa2
,isnull(knp.Knt_Nazwa3,knd.Knt_Nazwa3) Knp_Nazwa3
,isnull(knp.Knt_Nip,knd.Knt_Nip) Knp_NIP
,isnull(knp.Knt_Miasto,knd.Knt_Miasto) Knp_Miasto
,isnull(knp.Knt_KodP,knd.Knt_KodP) Knp_KodP
,isnull(knp.Knt_Ulica,knd.Knt_Ulica) Knp_Ulica
,isnull(knp.Knt_Adres,knd.Knt_Adres) Knp_Adres
,ISNULL(knp.knt_rabat,knd.Knt_rabat) Knp_Rabat
from cdn.KntOsoby
join cdn.KntKarty  knd on knd.Knt_GIDNumer=KnS_KntNumer
left join cdn.KntKarty knp on knd.Knt_KnCNumer=knp.Knt_GIDNumer and knp.Knt_GIDNumer<>0
where exists (select top 1 1 from cdn.Atrybuty where Atr_ObiNumer=KnS_KntNumer and Atr_ObiTyp=32 and Atr_Wartosc='tak' and Atr_AtkId=11)
and KnS_UpowaznionaZam=1
