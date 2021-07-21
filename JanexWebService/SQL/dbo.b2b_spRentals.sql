exec dbo.b2b_spRentals 9323
exec dbo.b2b_spRentalsElem 834678,9323
--select knt_gidnumer,Knt_Akronim from cdn.KntKarty where knt_akronim like 'g4s%'
go
alter procedure [dbo].[b2b_spRentals] (@gidnumer int)-- knd
AS
declare @typkontrahenta int
--1 p³atnik centrali
--2 oddzia³ centrali
--3 kontrahent niepowi¹zany

--p³atnik centrali
select @typkontrahenta=1 from cdn.KntKarty k where k.Knt_GIDNumer=@gidnumer and exists (select 1 from cdn.KntKarty o where o.Knt_KnCNumer=k.Knt_GIDNumer)
--oddzia³ centrali
select @typkontrahenta=2 from cdn.KntKarty k where k.Knt_GIDNumer=@gidnumer and k.Knt_KnCNumer<>0 and not exists (select 1 from cdn.KntKarty o where o.Knt_KnCNumer=k.Knt_GIDNumer)
--kontrahent niepowiazany
select @typkontrahenta=3 from cdn.KntKarty k where k.Knt_GIDNumer=@gidnumer and k.Knt_KnCNumer=0 and not exists (select 1 from cdn.KntKarty o where o.Knt_KnCNumer=k.Knt_GIDNumer)

if @typkontrahenta in (1,3)
begin 
	select 
		TrN_GIDNumer
		,CDN.NumerDokumentu(TrN_GIDTyp,TrN_SpiTyp,TrN_TrNTyp,TrN_TrNNumer,TrN_TrNRok,TrN_TrNSeria,trn_trnmiesiac) trn_numer
		,convert(varchar(10),dateadd(d,trn_Data3,'1800-12-28T00:00:00'), 121) trn_datasprzedazy
		,convert(varchar(10),dateadd(d,TrN_Data2,'1800-12-28T00:00:00'), 121) trn_datawystawienia
		,convert(varchar(10),dateadd(d,TrN_Data2+30,'1800-12-28T00:00:00'), 121) trn_terminwaznosci
		,TrN_NettoR as trn_wartoscNetto
		,TrN_Waluta
		,isnull((select MAX( convert(varchar(10),dateadd(d,TrP_Termin,'1800-12-28T00:00:00'), 121)) from cdn.TraPlat where TrP_GIDNumer=TrN_GIDNumer and TrP_GIDTyp=TrN_GIDTyp),convert(varchar(10),dateadd(d,TrN_Termin,'1800-12-28T00:00:00'), 121)) trn_terminplantosci
		,case when isnull((select SUM(trp_pozostaje) from cdn.TraPlat where TrP_GIDNumer=TrN_GIDNumer and TrP_GIDTyp=TrN_GIDTyp),0)=0 then 'Zap³acono' else 'Do op³acenia' end trn_zaplacono
	from cdn.TraNag
	where TrN_GIDTyp in (2001) and TrN_SpiNumer=0 and TrN_SpiTyp=0
		and TrN_KntNumer=@gidnumer
		and TrN_Stan > 2 and exists (select 1 from cdn.TraElem where TrN_GIDNumer=TrE_GIDNumer and TrE_GIDTyp=TrN_GIDTyp and cdn.ZwrocDaneSElementy_IloscPoKorekcie(TrE_GIDTyp,TrE_GIDNumer,TrE_GIDLp,TrE_Ilosc)<>0)
	order by TrN_GIDNumer desc
end

if @typkontrahenta in (2)
begin
	select 
		TrN_GIDNumer
		,CDN.NumerDokumentu(TrN_GIDTyp,TrN_SpiTyp,TrN_TrNTyp,TrN_TrNNumer,TrN_TrNRok,TrN_TrNSeria,TrN_TrNMiesiac) trn_numer
		,convert(varchar(10),dateadd(d,trn_Data3,'1800-12-28T00:00:00'), 121) trn_datasprzedazy
		,convert(varchar(10),dateadd(d,TrN_Data2,'1800-12-28T00:00:00'), 121) trn_datawystawienia
		,convert(varchar(10),dateadd(d,TrN_Data2+30,'1800-12-28T00:00:00'), 121) trn_terminwaznosci
		,TrN_NettoR as trn_wartoscNetto
		,TrN_Waluta
		,isnull((select MAX( convert(varchar(10),dateadd(d,TrP_Termin,'1800-12-28T00:00:00'), 121)) from cdn.TraPlat where TrP_GIDNumer=TrN_GIDNumer and TrP_GIDTyp=TrN_GIDTyp),convert(varchar(10),dateadd(d,TrN_Termin,'1800-12-28T00:00:00'), 121)) trn_terminplantosci
		,case when isnull((select SUM(trp_pozostaje) from cdn.TraPlat where TrP_GIDNumer=TrN_GIDNumer and TrP_GIDTyp=TrN_GIDTyp),0)=0 then 'Zap³acono' else 'Do op³acenia' end trn_zaplacono
	from cdn.TraNag
		join CDN.KntKarty on Knt_GIDNumer=TrN_KnDNumer and Knt_KnPNumer=TrN_KnPNumer
	where TrN_GIDTyp in (2001)
		and TrN_KnDNumer=@gidnumer
		and TrN_Stan > 2 and exists (select 1 from cdn.TraElem where TrN_GIDNumer=TrE_GIDNumer and TrE_GIDTyp=TrN_GIDTyp and cdn.ZwrocDaneSElementy_IloscPoKorekcie(TrE_GIDTyp,TrE_GIDNumer,TrE_GIDLp,TrE_Ilosc)<>0)
	order by TrN_GIDNumer desc
end


go
alter procedure [dbo].[b2b_spRentalsElem] (@trnnumer int,@kndnumer int)
as
begin

declare @typ tinyint=0 --1 centrala 0 oddzial
declare @knp int

select @knp=Knt_KnPNumer from cdn.kntkarty where @kndnumer=knt_gidnumer

if @knp=@kndnumer set @typ=1

select
wz.trn_gidnumer
,CDN.NumerDokumentu(wz.trn_GIDTyp,wz.trn_SpiTyp,wz.trn_TrNTyp,wz.trn_TrNNumer,wz.trn_TrNRok,wz.trn_TrNSeria,wz.TrN_TrNMiesiac)  trnnumer
,convert(varchar(10),dateadd(d,wz.trn_Data3,'1800-12-28T00:00:00'), 121) trndatasprzedazy
,convert(varchar(10),dateadd(d,wz.trn_Data2,'1800-12-28T00:00:00'), 121) trndatawystawienia
,wz.trn_NettoR as trnwartoscNetto
,(select sum(trp_pozostaje) from cdn.TraPlat where TrP_GIDNumer=wz.trn_GIDNumer and TrP_Rozliczona<>2) trnpozostajedozaplaty
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
,isnull((select twc_wartosc from cdn.twrceny where TwC_TwrNumer=TrE_TwrNumer and TwC_TwrLp=case when knp.Knt_Cena=0 then 1 else knp.Knt_Cena end and TwC_DataOd<>0),0) tre_CenaKatalogowa
,wz.trn_gidtyp trn_GidTyp
,TrE_Waluta
,isnull((select MAX( convert(varchar(10),dateadd(d,TrP_Termin,'1800-12-28T00:00:00'), 121)) from cdn.TraPlat where TrP_GIDNumer=wz.TrN_GIDNumer and TrP_GIDTyp=wz.TrN_GIDTyp),convert(varchar(10),dateadd(d,wz.TrN_Termin,'1800-12-28T00:00:00'), 121)) trn_terminplantosci
,case when isnull((select SUM(trp_pozostaje) from cdn.TraPlat where TrP_GIDNumer=wz.TrN_GIDNumer and TrP_GIDTyp=wz.TrN_GIDTyp),0)=0 then 'Zap³acono' else 'Do op³acenia' end trn_zaplacono
,wz.TrN_FormaNazwa FormaPlatnosci
,wz.TrN_SposobDostawy SposobDostawy
,cdn.ZwrocDaneSElementy_IloscPoKorekcie(TrE_GIDTyp,TrE_GIDNumer,TrE_GIDLp,TrE_Ilosc) tre_iloscPozostala
from cdn.TraElem
join cdn.TraNag wz on tre_GIDNumer=wz.trn_GIDNumer
join cdn.KntKarty  knd on knd.Knt_GIDNumer=wz.trn_KndNumer
left join cdn.KntKarty knp on wz.trn_KnPNumer=knp.Knt_GIDNumer 
where 
wz.trn_GIDNumer=@trnnumer 
--and wz.trn_KnDNumer=@kndnumer
--and case when @typ=1 then wz.trn_KnpNumer else wz.TrN_KnDNumer end =@kndnumer
and wz.TrN_KntNumer=@knp
and wz.trn_GIDTyp in (2001)
and wz.trn_stan > 2
end
