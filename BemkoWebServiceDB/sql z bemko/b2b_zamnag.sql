CREATE procedure [dbo].[b2b_zamnag] (@gidnumer int)
AS
select 
zan_gidnumer
,CDN.NumerDokumentuTRN ( CDN.DokMapTypDokumentu (ZaN_GIDTyp,ZaN_ZamTyp, ZaN_Rodzaj),0,0,ZaN_ZamNumer,ZaN_ZamRok,ZaN_ZamSeria) zan_numer
,convert(varchar(10),dateadd(d,ZaN_DataWystawienia,'1800-12-28T00:00:00'), 121) zan_datawystawienia
,convert(varchar(10),dateadd(d,ZaN_DataPotwierdz,'1800-12-28T00:00:00'), 121) zan_datapotwierdzenia
,convert(varchar(10),dateadd(d,ZaN_DataRealizacji,'1800-12-28T00:00:00'), 121) zan_datarealizacji
,[dbo].[bmp_zamstan] (ZaN_GIDNumer) as zan_stan
,(select cast(SUM(zae_cenauzgodniona*zae_ilosc)as decimaL(15,2)) from cdn.ZamElem where ZaE_GIDNumer=ZaN_GIDNumer) as zan_wartoscNetto
,case [dbo].[bmp_zamstan] (ZaN_GIDNumer)
when 'Inne' then 'f9ad81'
when 'Zrealizowane' then '134f84'
when 'W trakcie pakowania' then 'ffea00'
when 'Towar wysłano' then '48ff00'
when 'Niepotwierdzone' then 'ed1c24'
when 'Potwierdzone' then '005e20'
when 'Anulowane' then 'ed1c24' end Kolor
from cdn.ZamNag
where ZaN_ZamTyp=1280
and ZaN_KnDNumer=@gidnumer
order by ZaN_GIDNumer desc