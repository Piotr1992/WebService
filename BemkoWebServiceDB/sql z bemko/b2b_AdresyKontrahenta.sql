ALTER function  [dbo].[b2b_AdresyKontrahenta] (@kntnumer int)
returns table
as
return
select 
Knt_GIDNumer
,KnA_GIDNumer
,KnA_GIDTyp
,KnA_Akronim
,KnA_Nazwa1
,KnA_Nazwa2
,KnA_Nazwa3
,KnA_KodP
,KnA_Miasto
,KnA_Ulica
,KnA_Adres
,KnA_Kraj
,KnA_Wojewodztwo
,KnA_Wysylkowy
,case when KnA_GIDTyp=864 then 1 else 0 end KnA_Domyslny
from cdn.KntKarty
join cdn.KntAdresy a on KnA_KntNumer=Knt_GIDNumer
where knt_typ in (16,24)
and Knt_Archiwalny=0
and Knt_Wsk=0
and KnA_DataArc=0
and Knt_GIDNumer=@kntnumer
