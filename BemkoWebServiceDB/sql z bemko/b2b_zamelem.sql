CREATE FUNCTION [dbo].[b2b_zamelem] (@zannumer int,@kndnumer int)
RETURNS TABLE
AS
RETURN 
select
zan_gidnumer
,CDN.NumerDokumentuTRN ( CDN.DokMapTypDokumentu (ZaN_GIDTyp,ZaN_ZamTyp, ZaN_Rodzaj),0,0,ZaN_ZamNumer,ZaN_ZamRok,ZaN_ZamSeria) zan_numer
,convert(varchar(10),dateadd(d,ZaN_DataWystawienia,'1800-12-28T00:00:00'), 121) zan_datawystawienia
,convert(varchar(10),dateadd(d,ZaN_DataPotwierdz,'1800-12-28T00:00:00'), 121) zan_datapotwierdzenia
,convert(varchar(10),dateadd(d,ZaN_DataRealizacji,'1800-12-28T00:00:00'), 121) zan_datarealizacji
,[dbo].[bmp_zamstan] (ZaN_GIDNumer) as zan_stan
,(select cast(SUM(zae_cenauzgodniona*zae_ilosc)as decimaL(15,2)) from cdn.ZamElem where ZaE_GIDNumer=ZaN_GIDNumer) as zan_wartoscNetto
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
,knp.Knt_GIDNumer Knp_Gidnumer
,knp.Knt_Akronim Knp_Akronim
,knp.Knt_Nazwa1 Knp_Nazwa1
,knp.Knt_Nazwa2 Knp_Nazwa2
,knp.Knt_Nazwa3 Knp_Nazwa3
,knp.Knt_Nip Knp_NIP
,knp.Knt_Miasto Knp_Miasto
,knp.Knt_KodP Knp_KodP
,knp.Knt_Ulica Knp_Ulica
,knp.Knt_Adres Knp_Adres
,ZaE_TwrNumer 
,ZaE_TwrKod
,ZaE_TwrNazwa
,ZaE_Ilosc
,ZaE_JmZ
,ZaE_CenaUzgodniona ZaE_CenaNetto
,cast(ZaE_CenaUzgodniona*ZaE_Ilosc as decimal(15,2)) ZaE_WartoscNetto
,ZaE_StawkaPod
,ZaE_Rabat
,ZaE_CenaKatalogowa
,ISNULL(zno_opis,'') ZnO_Opis
from cdn.ZamElem
join cdn.ZamNag on ZaE_GIDNumer=ZaN_GIDNumer
join cdn.KntKarty  knd on knd.Knt_GIDNumer=zan_KndNumer
left join cdn.KntKarty knp on ZaN_KnPNumer=knp.Knt_GIDNumer and knp.Knt_GIDNumer<>0
left join cdn.ZaNOpisy on ZaN_GIDNumer=ZnO_ZamNumer
where ZaE_GIDNumer=@zannumer and ZaN_KnDNumer=@kndnumer
and ZaN_ZamTyp=1280 and ZaN_Rodzaj =4

