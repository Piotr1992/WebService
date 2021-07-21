

ALTER procedure [dbo].[b2b_spReklNag] (@gidnumer int,@rlnzadanie int)

AS

select rln_id
,CDN.NumerDokumentu(rln_typ,0,0,RLN_Numer,rln_rok,rln_seria,rln_miesiac) rln_numer
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
,RLN_KnSLp KnsLp
from cdn.ReklNag 

left join cdn.atrybuty AtrybutSposobDostawy on AtrybutSposobDostawy.Atr_ObiTyp=RLN_Typ and AtrybutSposobDostawy.Atr_ObiNumer=RLN_Id and AtrybutSposobDostawy.Atr_ObiSubLp=0 and AtrybutSposobDostawy.Atr_AtkId=398
left join cdn.atrybuty AtrybutNrListuPrzewozowego on AtrybutNrListuPrzewozowego.Atr_ObiTyp=RLN_Typ and AtrybutNrListuPrzewozowego.Atr_ObiNumer=RLN_Id and AtrybutNrListuPrzewozowego.Atr_ObiSubLp=0 and AtrybutNrListuPrzewozowego.Atr_AtkId=400
left join cdn.atrybuty AtrybutFormaZwrotu on AtrybutFormaZwrotu.Atr_ObiTyp=RLN_Typ and AtrybutFormaZwrotu.Atr_ObiNumer=RLN_Id and AtrybutFormaZwrotu.Atr_ObiSubLp=0 and AtrybutFormaZwrotu.Atr_AtkId=401
left join cdn.atrybuty AtrybutKontoNr on AtrybutKontoNr.Atr_ObiTyp=RLN_Typ and AtrybutKontoNr.Atr_ObiNumer=RLN_Id and AtrybutKontoNr.Atr_ObiSubLp=0 and AtrybutKontoNr.Atr_AtkId=402
left join cdn.atrybuty AtrybutKontoNazwa on AtrybutKontoNazwa.Atr_ObiTyp=RLN_Typ and AtrybutKontoNazwa.Atr_ObiNumer=RLN_Id and AtrybutKontoNazwa.Atr_ObiSubLp=0 and AtrybutKontoNazwa.Atr_AtkId=403
left join cdn.atrybuty AtrybutUwagi on AtrybutUwagi.Atr_ObiTyp=RLN_Typ and AtrybutUwagi.Atr_ObiNumer=RLN_Id and AtrybutUwagi.Atr_ObiSubLp=0 and AtrybutUwagi.Atr_AtkId=404


where RLN_KntNumer=@gidnumer and exists (select top 1 RLE_Zadanie
from cdn.ReklElem e 
 where rln_id=RLE_RLNId and rle_zadanie=@rlnzadanie)

