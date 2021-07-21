alter procedure [dbo].[b2b_spSprzedazKontrahentaSN](@kntnumer int, @searchString varchar(100),@searchType int=0)
as
begin
declare @sstring varchar(100)=ltrim(rtrim(@searchString))
declare @today int=datediff(d,'1800-12-28T00:00:00',getdate())

;with sprzedaz as (
select distinct
--,CDN.NumerDokumentu(w.TrN_GIDTyp,w.TrN_SpiTyp,w.TrN_TrNTyp,w.TrN_TrNNumer,w.TrN_TrNRok,w.TrN_TrNSeria,w.TrN_TrNMiesiac)  DokumentNumer
CDN.NumerDokumentu(s.TrN_GIDTyp,s.TrN_spiTyp,s.TrN_TrNTyp,s.TrN_TrNNumer,s.TrN_TrNRok,s.TrN_TrNSeria,s.TrN_TrNMiesiac)  DokumentNumer
,w.trn_gidnumer DokumentId
,TrE_GIDTyp DokumentTyp
,TrE_GIDLp DokumentLp
,convert(varchar(10),dateadd(d,w.trn_data3,'1800-12-28T00:00:00'), 121) DokumentDataSprzedazy
,w.TrN_KntNumer KntNumer
,Twr_Kod TowarKod
,Twr_Nazwa TowarNazwa
,Twr_GIDNumer TowarId
,case when Twr_OkresGwarancji=0 then '-'
	when w.trn_data3+Twr_OkresGwarancji>@today then 'Tak' else 'Nie' end TowarGwarancja
,isnull(sn.Code,'') SN
,case when knt_gidnumer=0 then '' else Knt_Akronim end Producent
,Knt_GIDNumer ProducentId
,sn.id SNId,sn.ItemId SNItemId
,TrE_Ilosc
,cdn.ZwrocDaneSElementy_IloscPoKorekcie(TrE_GIDTyp,TrE_GIDNumer,TrE_GIDLp,TrE_Ilosc) IloscZkorektami
from cdn.magnag
	join cdn.MagSElem on man_gidnumer=MaS_GIDNumer
	join cdn.TwrKarty on Twr_GIDNumer=MaS_TwrNumer
	join cdn.kntkarty on Twr_PrdNumer=Knt_GIDNumer
	left join wms.Documents d on MaN_GIDNumer=d.SourceDocumentId
	left join wms.Items i on d.Id=i.DocumentId and i.ArticleId=MaS_TwrNumer
	left join wms.ScannedQuantityCodes sn on sn.ItemId=i.Id
	left join cdn.TraElem on tre_gidnumer=MaS_ZrdNumer and TrE_GIDTyp=MaS_ZrdTyp and TrE_GIDLp=MaS_ZrdLp
	left join cdn.tranag w on tre_gidnumer=w.TrN_GIDNumer and TrE_GIDTyp=w.TrN_GIDTyp
	left join cdn.TraNag s on case w.trn_spinumer when 0 then w.trn_gidnumer else w.TrN_SpiNumer end =s.TrN_GIDNumer
where w.TrN_GIDTyp in (2033,2001)
and MaN_KntNumer=@kntnumer
union all
select distinct
	null  DokumentNumer
	,null DokumentId
	,null DokumentTyp
	,null DokumentLp
	,null DokumentDataSprzedazy
	,null KntNumer
	,Twr_Kod TowarKod
	,Twr_Nazwa TowarNazwa
	,Twr_GIDNumer TowarId
	,'Nie' TowarGwarancja
	,null SN
	,case when knt_gidnumer=0 then '' else Knt_Akronim end Producent
	,Knt_GIDNumer ProducentId
	,null SNId
	,null SNItemId
	,null TrE_Ilosc
	,1 IloscZkorektami
from cdn.TwrKarty 
	join cdn.kntkarty on Twr_PrdNumer=Knt_GIDNumer
where Twr_GIDNumer<>0
)
select * 
from sprzedaz 
where IloscZkorektami<>0
and (TowarKod like '%' + @sstring  + '%'
or SN like '%' + @sstring  + '%'
or DokumentNumer like '%' + @sstring + '%'
or TowarNazwa like '%' + @sstring  + '%'
or Producent like '%' + @sstring  + '%'
)
end
go