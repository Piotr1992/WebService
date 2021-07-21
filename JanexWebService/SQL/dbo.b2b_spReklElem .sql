
alter procedure [dbo].[b2b_spReklElem] (@gidnumer int,@kndnumer int)

AS
;with ostatniarealizacjaId as
(select max(rlr_id) ost_rlrid,rlr_rleid ost_rlrrleid from cdn.ReklRealizacja where RLR_Opublikowana=1 group by RLR_RLEId) 
,ostatnistatusId as
(select max(rlr_id) oss_rlrid,RLR_RLEId oss_rlrrleid 
	from cdn.ReklRealizacja sr join ostatniarealizacjaId on ost_rlrrleid=RLR_RLEId  
	where RLR_status<>3 and rlr_id<=isnull(ost_rlrid,rlr_id) 
	group by RLR_RLEId) 
,realizacje as (
select isnull(nullif(r.rlr_status,3),sr.rlr_status) rlr_status
,case isnull(nullif(r.rlr_status,3),sr.rlr_status) 
	when 0 then 'Rozpatrywana'
	when 1 then 'Uznana'
	when 2 then 'Odrzucona'
end rlr_statusslownie
,r.RLR_StanPo,case r.rlr_stanpo
when 1 then 'Niepotwierdzona'
when 2 then 'Niepotwierdzona'
when 6 then 'Anulowana'
when 10 then 'Potwierdzona'
when 20 then 'W realizacji'
when 21 then 'W realizacji'
when 30 then 'Rozpatrzona'
when 31 then 'Rozpatrzona'
when 40 then 'Zamkniêta'
end rlr_stanslownie 
,r.RLR_Opis
,r.rlr_id,r.RLR_Opublikowana,r.RLR_RLEId
from cdn.ReklRealizacja r 
	join ostatniarealizacjaId on ost_rlrid=RLR_Id
	left join ostatnistatusId on oss_rlrrleid=RLR_RLEId
	left join cdn.ReklRealizacja sr on oss_rlrid=sr.RLR_Id
)

select rln_id
,CDN.NumerDokumentu(rln_typ,0,0,rln_numer,rln_rok,rln_seria,rln_miesiac) rln_numer
,convert(varchar(10),dateadd(d,rln_datawyst,'1800-12-28T00:00:00'), 121) rln_datawystawienia
,case when rln_datarozp=0 then null else convert(varchar(10),dateadd(d,rln_datarozp,'1800-12-28T00:00:00'), 121) end rln_datarozpatrzenia
,case when rln_datazamkniecia=0 then null else convert(varchar(10),dateadd(d,rln_datazamkniecia,'1800-12-28T00:00:00'), 121) end rln_datazamkniecia
,rln_opis
,rln_status
,case rln_status 
	when 0 then 'Rozpatrywana'
	when 1 then 'Uznana'
	when 2 then 'Odrzucona'
	when 3 then 'Zrealizowana'
	when 4 then 'Zrealizowana'
end rln_statusslownie
,rln_stan
,case rln_stan
when 1 then 'Niepotwierdzona'
when 2 then 'Niepotwierdzona'
when 6 then 'Anulowana'
when 10 then 'Potwierdzona'
when 20 then 'W realizacji'
when 21 then 'W realizacji'
when 30 then 'Rozpatrzona'
when 31 then 'Rozpatrzona'
when 40 then 'Zamkniêta'
end rln_stanslownie
,(select top 1 RLE_Zadanie
from cdn.ReklElem e 
 where rln_id=RLE_RLNId) rln_zadaniereklamujacegoid
,(select top 1 SLW_WartoscS '¯¹danie reklamuj¹cego'
from cdn.ReklElem e 
join cdn.Slowniki on slw_id=RLE_Zadanie where rln_id=RLE_RLNId) rln_zadaniereklamujacego

,isnull(AtrybutSposobDostawy.Atr_Wartosc,'') AtrybutSposobDostawy
,isnull(AtrybutNrListuPrzewozowego.Atr_Wartosc,'') AtrybutNrListuPrzewozowego
,isnull(AtrybutFormaZwrotu.Atr_Wartosc,'') AtrybutFormaZwrotu
,isnull(AtrybutKontoNr.Atr_Wartosc,'') AtrybutKontoNr
,isnull(AtrybutKontoNazwa.Atr_Wartosc,'') AtrybutKontoNazwa
,isnull(AtrybutUwagi.Atr_Wartosc,'') AtrybutUwagi

,knd.knt_gidnumer Knd_Gidnumer
,adw.Kna_Akronim Knd_Akronim
,adw.KnA_Nazwa1 Knd_Nazwa1
,adw.KnA_Nazwa2 Knd_Nazwa2
,adw.KnA_Nazwa3 Knd_Nazwa3
,adw.KnA_Nip Knd_NIP
,adw.KnA_Miasto Knd_Miasto
,adw.KnA_KodP Knd_KodP
,adw.KnA_Ulica Knd_Ulica
,adw.KnA_Adres Knd_Adres
--,knt.Knt_GIDNumer Knp_Gidnumer
--,knt.Knt_Akronim Knp_Akronim
--,knt.Knt_Nazwa1 Knp_Nazwa1
--,knt.Knt_Nazwa2 Knp_Nazwa2
--,knt.Knt_Nazwa3 Knp_Nazwa3
--,knt.Knt_Nip Knp_NIP
--,knt.Knt_Miasto Knp_Miasto
--,knt.Knt_KodP Knp_KodP
--,knt.Knt_Ulica Knp_Ulica
--,knt.Knt_Adres Knp_Adres

,case when isnull(trn_trnnumer,0)=0 then RLE_ZrdDokumentObcy else CDN.NumerDokumentu(TrN_GIDTyp,TrN_SpiTyp,TrN_TrNTyp,TrN_TrNNumer,TrN_TrNRok,TrN_TrNSeria,TrN_TrNMiesiac) end DokumentNumer
,trn_gidnumer DokumentId
,trn_GIDTyp DokumentTyp
,rle_zrdlp DokumentLp
,case when isnull(trn_trnnumer,0)=0 then isnull(AtrybutDokumentDataSprzedazy.Atr_Wartosc,'') else convert(varchar(10),dateadd(d,trn_data3,'1800-12-28T00:00:00'), 121) end DokumentDataSprzedazy
,RLE_TwrNumer
,RLE_TwrKod
,RLE_TwrNazwa
,RLE_Zadanie
,(select top 1 SLW_WartoscS '¯¹danie reklamuj¹cego'
from cdn.ReklElem e 
join cdn.Slowniki on slw_id=RLE_Zadanie where rln_id=RLE_RLNId) RLE_ZadanieReklamujacego
,rle_ilosc
,isnull(AtrybutSN.Atr_Wartosc,'') AtrybutSN
,rle_przyczyna
,rle_rozpatrzenie
,case when Twr_OkresGwarancji=0 then '-'
	when trn_data3+Twr_OkresGwarancji>rln_datawyst then 'Tak' else 'Nie' end TowarGwarancjaNaDzienReklamacji
,KnS_Nazwa Osoba
,KnS_EMail eMail
,r.*

from cdn.ReklNag
join cdn.ReklElem e on RLE_RLNId=RLN_Id
join cdn.TwrKarty on twr_gidnumer = RLE_TwrNumer
left join cdn.tranag on TrN_GIDNumer=RLE_ZrdNumer and RLE_ZrdTyp=TrN_GIDTyp
left join cdn.atrybuty AtrybutSN on AtrybutSN.Atr_ObiTyp=RLN_Typ and AtrybutSN.Atr_ObiNumer=RLE_Id and AtrybutSN.Atr_ObiSubLp=3586 and AtrybutSN.Atr_AtkId=383
left join cdn.atrybuty AtrybutSposobDostawy on AtrybutSposobDostawy.Atr_ObiTyp=RLN_Typ and AtrybutSposobDostawy.Atr_ObiNumer=RLN_Id and AtrybutSposobDostawy.Atr_ObiSubLp=0 and AtrybutSposobDostawy.Atr_AtkId=398
left join cdn.atrybuty AtrybutNrListuPrzewozowego on AtrybutNrListuPrzewozowego.Atr_ObiTyp=RLN_Typ and AtrybutNrListuPrzewozowego.Atr_ObiNumer=RLN_Id and AtrybutNrListuPrzewozowego.Atr_ObiSubLp=0 and AtrybutNrListuPrzewozowego.Atr_AtkId=400
left join cdn.atrybuty AtrybutFormaZwrotu on AtrybutFormaZwrotu.Atr_ObiTyp=RLN_Typ and AtrybutFormaZwrotu.Atr_ObiNumer=RLN_Id and AtrybutFormaZwrotu.Atr_ObiSubLp=0 and AtrybutFormaZwrotu.Atr_AtkId=401
left join cdn.atrybuty AtrybutKontoNr on AtrybutKontoNr.Atr_ObiTyp=RLN_Typ and AtrybutKontoNr.Atr_ObiNumer=RLN_Id and AtrybutKontoNr.Atr_ObiSubLp=0 and AtrybutKontoNr.Atr_AtkId=402
left join cdn.atrybuty AtrybutKontoNazwa on AtrybutKontoNazwa.Atr_ObiTyp=RLN_Typ and AtrybutKontoNazwa.Atr_ObiNumer=RLN_Id and AtrybutKontoNazwa.Atr_ObiSubLp=0 and AtrybutKontoNazwa.Atr_AtkId=403
left join cdn.atrybuty AtrybutUwagi on AtrybutUwagi.Atr_ObiTyp=RLN_Typ and AtrybutUwagi.Atr_ObiNumer=RLN_Id and AtrybutUwagi.Atr_ObiSubLp=0 and AtrybutUwagi.Atr_AtkId=404
left join cdn.atrybuty AtrybutDokumentDataSprzedazy on AtrybutDokumentDataSprzedazy.Atr_ObiTyp=RLN_Typ and AtrybutDokumentDataSprzedazy.Atr_ObiNumer=RLe_Id and AtrybutDokumentDataSprzedazy.Atr_ObiSubLp=3586 and AtrybutDokumentDataSprzedazy.Atr_AtkId=410
left join cdn.KntKarty knt on RLN_KntNumer=knt.Knt_GIDNumer and RLN_KntTyp=knt.Knt_GIDTyp
left join cdn.KntAdresy kna on RLN_KnANumer=kna.KnA_GIDNumer and RLN_KnATyp=kna.KnA_GIDTyp
left join cdn.KntKarty knd on RLN_KnDNumer=knd.Knt_GIDNumer and RLN_KnDTyp=knd.Knt_GIDTyp
left join cdn.KntAdresy adw on RLN_adwNumer=adw.KnA_GIDNumer and RLN_adwTyp=adw.KnA_GIDTyp
left join cdn.kntosoby oso on RLN_KnSNumerD=KnS_KntNumer and RLN_KnSTypD=KnS_KntTyp and rln_knslpd=KnS_KntLp
left join realizacje r on RLR_RLEId=RLE_Id
where RLN_KntNumer=@kndnumer and rln_id=@gidnumer



GO
