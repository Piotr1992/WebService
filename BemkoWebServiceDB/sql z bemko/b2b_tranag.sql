CREATE procedure [dbo].[b2b_tranag] (@gidnumer int)
AS
select 
TrN_GIDNumer
,CDN.NumerDokumentuTRN(TrN_GIDTyp,TrN_SpiTyp,TrN_TrNTyp,TrN_TrNNumer,TrN_TrNRok,TrN_TrNSeria) trn_numer
,convert(varchar(10),dateadd(d,trn_Data3,'1800-12-28T00:00:00'), 121) trn_datasprzedazy
,convert(varchar(10),dateadd(d,TrN_Data2,'1800-12-28T00:00:00'), 121) trn_datawystawienia
,TrN_NettoR as trn_wartoscNetto
from cdn.TraNag
where TrN_GIDTyp in (2033,2041)
and trn_KnpNumer=@gidnumer
and TrN_Stan > 2
order by TrN_GIDNumer desc



