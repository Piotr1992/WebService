CREATE procedure [dbo].[b2b_traelem] (@trnnumer int,@kndnumer int)
as
begin

select
fs.trn_gidnumer
,CDN.NumerDokumentuTRN(fs.trn_GIDTyp,fs.trn_SpiTyp,fs.trn_TrNTyp,fs.trn_TrNNumer,fs.trn_TrNRok,fs.trn_TrNSeria)  trnnumer
,convert(varchar(10),dateadd(d,fs.trn_Data3,'1800-12-28T00:00:00'), 121) trndatasprzedazy
,convert(varchar(10),dateadd(d,fs.trn_Data2,'1800-12-28T00:00:00'), 121) trndatawystawienia
,fs.trn_NettoR as trnwartoscNetto
,(select sum(trp_pozostaje) from cdn.TraPlat where TrP_GIDNumer=fs.trn_GIDNumer and TrP_Rozliczona<>2) trnpozostajedozaplaty
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
,tre_TwrNumer 
,tre_TwrKod
,tre_TwrNazwa
,tre_Ilosc
,tre_JmZ
,tre_cena tre_CenaNetto
,tre_ksiegowanetto tre_WartoscNetto
,tre_StawkaPod
,tre_Rabat
,TrE_CenaSpr tre_CenaKatalogowa
,fs.trn_gidtyp trn_GidTyp
from cdn.TraElem
join cdn.TraNag wz on tre_GIDNumer=wz.trn_GIDNumer
join cdn.tranag fs on fs.trn_gidnumer=wz.trn_spinumer and fs.trn_gidtyp=wz.trn_spityp
join cdn.KntKarty  knd on knd.Knt_GIDNumer=fs.trn_KndNumer
left join cdn.KntKarty knp on fs.trn_KnPNumer=knp.Knt_GIDNumer 
where 
fs.trn_GIDNumer=@trnnumer and fs.trn_KnDNumer=@kndnumer
and fs.trn_GIDTyp in (2033,2041)
and fs.trn_stan > 2
end



