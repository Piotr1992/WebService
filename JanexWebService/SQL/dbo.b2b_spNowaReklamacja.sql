if exists (SELECT 1 FROM sys.objects WHERE (object_id = OBJECT_ID(N'dbo.b2b_spNowaReklamacja') AND type IN ( N'P', N'PC' ))) drop procedure dbo.b2b_spNowaReklamacja
if type_id('dbo.b2b_udttReklamacja') is not null drop type dbo.b2b_udttReklamacja
if type_ID(N'dbo.b2b_udttReklamacjaElementy') is not null drop type dbo.b2b_udttReklamacjaElementy
go

CREATE TYPE dbo.b2b_udttReklamacja AS TABLE(
    KntNumer int not null,
   	KnsLp int null,
    AdwTyp int null,
    AdwNumer int null,
	ZadanieId int not null,
    Opis varchar(1999) null,
	AtrybutSposobDostawy varchar(200) null,
	AtrybutNrListuPrzewozowego varchar(200) null,
	AtrybutFormaZwrotu varchar(200) null,
	AtrybutKontoNr varchar(200) null,
	AtrybutKontoNazwa varchar(200) null,
	AtrybutUwagi varchar(1999) null
)
GO
Create Type dbo.b2b_udttReklamacjaElementy as Table (
    Lp int not null ,
    TwrNumer int null,
    TwrKod varchar(40) null,
    Ilosc decimal(11,4) not null,
    ZrdTyp int,
    ZrdNumer int,
    ZrdLp int,
    Przyczyna varchar(1999),
    DokumentObcy varchar(40),
	AtrybutSN varchar(100),
	AtrybutDokumentDataSprzedazy varchar (100),
	AtrybutRodzajZgloszenia varchar(100)
)
go
create procedure [dbo].[b2b_spNowaReklamacja] @nag b2b_udttReklamacja READONLY, @elem b2b_udttReklamacjaElementy READONLY, @zal image,@zalNazwa varchar(50),@zalRozszerzenie Varchar(50),@zalRozmiar int
as
begin
if (select count(*) from @nag)=1 and (select count(*) from @elem)>=1
	begin
	begin tran
	declare @seria varchar(5)='B2B'
	declare @knttyp int=32,@rlntyp int=3584,@rodid int=-1,@openumer int=149,@twrtyp int=16,@kntlp int=0,@jmformat int=0
	declare @dabid int=0
	declare @tmp int
	
	declare @rlnid int
	declare @rleid int=0
	declare @dokumentnr varchar(40)
	declare @sn varchar(200),	@AtrybutSposobDostawy varchar(200),	@AtrybutNrListuPrzewozowego varchar(200),	@AtrybutFormaZwrotu varchar(200) 
	,@AtrybutKontoNr varchar(200),	@AtrybutKontoNazwa varchar(200),@AtrybutUwagi varchar(1999) 
	declare @AtrybutDokumentDataSprzedazy varchar (100)
	declare @AtrybutRodzajZgloszenia varchar (100)


	declare @kntnumer int,@zadanieid int,@knatyp int,@knanumer int,@kndnumer int,@adwtyp int,@adwnumer int,@opis varchar(1999)
	select @kntnumer=KntNumer,@zadanieid=ZadanieId,@knatyp=knt_KnaTyp,@knanumer=knt_KnaNumer,@kndnumer=kntnumer,@adwtyp=isnull(nullif(AdwTyp,0),knt_knatyp),@adwnumer=isnull(nullif(AdwNumer,0),knt_knanumer),@opis=Opis
		,@AtrybutSposobDostawy=AtrybutSposobDostawy,@AtrybutNrListuPrzewozowego=AtrybutNrListuPrzewozowego,@AtrybutFormaZwrotu=AtrybutFormaZwrotu
		,@AtrybutKontoNr=AtrybutKontoNr,@AtrybutKontoNazwa=AtrybutKontoNazwa,@AtrybutUwagi=AtrybutUwagi,@kntlp=isnull(knslp,0)
	from @nag left join cdn.kntkarty on knt_gidnumer=kntnumer
	declare @lp int,@twrnumer int,@twrkod varchar(50), @ilosc decimal (11,4), @zrdtyp int, @zrdnumer int,@zrdlp int,@przyczyna varchar(1999),@dokumentobcy varchar(100)
	select @lp=Lp,@twrnumer=TwrNumer,@twrkod=TwrKod,@ilosc=Ilosc,@zrdtyp=ZrdTyp,@zrdnumer=ZrdNumer,@zrdlp=ZrdLp,@przyczyna=Przyczyna,@dokumentobcy=DokumentObcy,@sn=atrybutSN,@AtrybutDokumentDataSprzedazy=AtrybutDokumentDataSprzedazy,@AtrybutRodzajZgloszenia=AtrybutRodzajZgloszenia from @elem where lp=1
	if @twrnumer=0 set @twrnumer=isnull((select twr_gidnumer from cdn.twrkarty where twr_kod = @twrkod),0)
	
	declare @updated tinyint=0
	if @twrnumer <> 0 and (select twr_archiwalny from cdn.TwrKarty where Twr_GIDnumer=@twrnumer)=1
	begin
		update cdn.TwrKarty set Twr_Archiwalny=0 where Twr_GIDNumer=@twrnumer
		set @updated=1
	end

	exec cdn.bmp_XLNowaReklamacja @rlntyp=@rlntyp,@rodid=@rodid,@openumer=@openumer
		,@kntnumer=@kntnumer,@knttyp=@knttyp,@kntlp=@kntlp
		,@knatyp=@knatyp,@knanumer=@knanumer
		,@kndnumer=@kndnumer,@kndtyp=@knttyp
		,@adwtyp=@adwtyp,@adwnumer=@adwnumer
		,@twrtyp=@twrtyp,@twrnumer=@twrnumer,@twrkod=@twrkod
		,@ilosc=@ilosc,@jmformat=@jmformat
		,@zrdtyp=@zrdtyp,@zrdnumer=@zrdnumer,@zrdlp=@zrdlp
		,@dokumentnr=@dokumentnr output
		,@przyczyna=@przyczyna,@opisnaglowek=@opis,@zadanie=@zadanieid,@dokumentobcy=@dokumentobcy,@opis=''
		,@id=@rlnid output 
		,@seria=@seria
	if @rlnid is not null
	begin --atrybuty
	select @rleid=rle_id from cdn.ReklElem where RLE_RLNId=@rlnid
		if @rleid<>0 and @sn<>'' and @rleid is not null
		insert into cdn.Atrybuty( [Atr_ObiTyp], [Atr_ObiFirma], [Atr_ObiNumer], [Atr_ObiLp], [Atr_ObiSubLp], [Atr_AtkId], [Atr_Wartosc], [Atr_AtrTyp], [Atr_AtrFirma], [Atr_AtrNumer], [Atr_AtrLp], [Atr_AtrSubLp], [Atr_OptimaId], [Atr_Grupujacy])
		values (3584,408066,@rleid,0,3586,383,@sn,0,0,0,0,0,0,0)
		if @rleid<>0 and @AtrybutDokumentDataSprzedazy<>'' and @rleid is not null
		insert into cdn.Atrybuty( [Atr_ObiTyp], [Atr_ObiFirma], [Atr_ObiNumer], [Atr_ObiLp], [Atr_ObiSubLp], [Atr_AtkId], [Atr_Wartosc], [Atr_AtrTyp], [Atr_AtrFirma], [Atr_AtrNumer], [Atr_AtrLp], [Atr_AtrSubLp], [Atr_OptimaId], [Atr_Grupujacy])
		values (3584,408066,@rleid,0,3586,410,@AtrybutDokumentDataSprzedazy,0,0,0,0,0,0,0)
		insert into cdn.Atrybuty( [Atr_ObiTyp], [Atr_ObiFirma], [Atr_ObiNumer], [Atr_ObiLp], [Atr_ObiSubLp], [Atr_AtkId], [Atr_Wartosc], [Atr_AtrTyp], [Atr_AtrFirma], [Atr_AtrNumer], [Atr_AtrLp], [Atr_AtrSubLp], [Atr_OptimaId], [Atr_Grupujacy])
		values (3584,408066,@rleid,0,3586,14,@AtrybutRodzajZgloszenia,0,0,0,0,0,0,0)

		if isnull(@AtrybutFormaZwrotu,'')<>'' exec dbo.b2b_spDodajAtrybutReklamacji @rlnId,401,@AtrybutFormaZwrotu
		if isnull(@AtrybutSposobDostawy,'')<>'' exec dbo.b2b_spDodajAtrybutReklamacji @rlnId,398,@AtrybutSposobDostawy
		if isnull(@AtrybutNrListuPrzewozowego,'')<>'' exec dbo.b2b_spDodajAtrybutReklamacji @rlnId,400,@AtrybutNrListuPrzewozowego
		if isnull(@AtrybutKontoNr,'')<>'' exec dbo.b2b_spDodajAtrybutReklamacji @rlnId,402,@AtrybutKontoNr
		if isnull(@AtrybutKontoNazwa,'')<>'' exec dbo.b2b_spDodajAtrybutReklamacji @rlnId,403,@AtrybutKontoNazwa
		if isnull(@AtrybutUwagi,'')<>'' exec dbo.b2b_spDodajAtrybutReklamacji @rlnId,404,@AtrybutUwagi
		if @rleid<>0 and @rleid is not null
		begin
		;with s as (
			select @rleid id,AtK_ID
			from cdn.AtrybutyObiekty
			join cdn.AtrybutyKlasy on AtO_AtKId=AtK_ID
			 where AtO_GIDTyp=3584 and AtO_Element=1 and AtK_Automat=1
			 )
			insert into cdn.Atrybuty( [Atr_ObiTyp], [Atr_ObiFirma], [Atr_ObiNumer], [Atr_ObiLp], [Atr_ObiSubLp], [Atr_AtkId], [Atr_Wartosc], [Atr_AtrTyp], [Atr_AtrFirma], [Atr_AtrNumer], [Atr_AtrLp], [Atr_AtrSubLp], [Atr_OptimaId], [Atr_Grupujacy])
			 select 3584,408066,@rleid,0,3586,atk_id,'',0,0,0,0,0,0,0 
			 from s left join cdn.Atrybuty on Atr_ObiNumer=id and atk_id=Atr_AtkId
			 where  isnull(Atr_ObiSubLp,3586)=3586 and atr_id is null
		end

	end

	if @updated=1
	begin
		update cdn.TwrKarty set Twr_Archiwalny=1 where Twr_GIDNumer=@twrnumer
		set @updated=0
	end

	if  (select count(*) from @elem)>=1 and @rlnid is not null
	begin
		declare cur cursor fast_forward
		for
		select * from @elem where lp>1
		open cur
		fetch next from cur into @lp,@twrnumer,@twrkod,@ilosc,@zrdtyp,@zrdnumer,@zrdlp,@przyczyna,@dokumentobcy,@sn,@AtrybutDokumentDataSprzedazy,@AtrybutRodzajZgloszenia
		WHILE @@FETCH_STATUS = 0  
		BEGIN
		set @updated=0
		if @twrnumer=0 set @twrnumer=isnull((select twr_gidnumer from cdn.twrkarty where twr_kod = @twrkod),0)
		
		if @twrnumer <> 0 and (select twr_archiwalny from cdn.TwrKarty where Twr_GIDnumer=@twrnumer)=1
		begin
			update cdn.TwrKarty set Twr_Archiwalny=0 where Twr_GIDNumer=@twrnumer
			set @updated=1
		end

        exec cdn.bmp_XLDodajPozycjeReklamacji @rlnid=@rlnid
					,@zrdtyp=@zrdtyp,@zrdnumer=@zrdnumer,@zrdlp=@zrdlp
					,@twrtyp=@twrtyp,@twrnumer=@twrnumer,@twrkod=@twrkod,@ilosc=@ilosc,@jmformat=@jmformat
					,@przyczyna=@przyczyna,@zadanie=@zadanieid,@openumer=@openumer
					,@kntnumer=@kntnumer,@dokumentobcy=@dokumentobcy
					,@rleid=@rleid output

		if @updated=1
		begin
			update cdn.TwrKarty set Twr_Archiwalny=1 where Twr_GIDNumer=@twrnumer
			set @updated=0
		end
		if @rleid<>0 and @sn<>'' and @rleid is not null
		insert into cdn.Atrybuty( [Atr_ObiTyp], [Atr_ObiFirma], [Atr_ObiNumer], [Atr_ObiLp], [Atr_ObiSubLp], [Atr_AtkId], [Atr_Wartosc], [Atr_AtrTyp], [Atr_AtrFirma], [Atr_AtrNumer], [Atr_AtrLp], [Atr_AtrSubLp], [Atr_OptimaId], [Atr_Grupujacy])
		values (3584,408066,@rleid,0,3586,383,@sn,0,0,0,0,0,0,0)
		if @rleid<>0 and @AtrybutDokumentDataSprzedazy<>'' and @rleid is not null
		insert into cdn.Atrybuty( [Atr_ObiTyp], [Atr_ObiFirma], [Atr_ObiNumer], [Atr_ObiLp], [Atr_ObiSubLp], [Atr_AtkId], [Atr_Wartosc], [Atr_AtrTyp], [Atr_AtrFirma], [Atr_AtrNumer], [Atr_AtrLp], [Atr_AtrSubLp], [Atr_OptimaId], [Atr_Grupujacy])
		values (3584,408066,@rleid,0,3586,410,@AtrybutDokumentDataSprzedazy,0,0,0,0,0,0,0)
		insert into cdn.Atrybuty( [Atr_ObiTyp], [Atr_ObiFirma], [Atr_ObiNumer], [Atr_ObiLp], [Atr_ObiSubLp], [Atr_AtkId], [Atr_Wartosc], [Atr_AtrTyp], [Atr_AtrFirma], [Atr_AtrNumer], [Atr_AtrLp], [Atr_AtrSubLp], [Atr_OptimaId], [Atr_Grupujacy])
		values (3584,408066,@rleid,0,3586,14,@AtrybutRodzajZgloszenia,0,0,0,0,0,0,0)
		if @rleid<>0 and @rleid is not null
		begin
		;with s as (
			select @rleid id,AtK_ID
			from cdn.AtrybutyObiekty
			join cdn.AtrybutyKlasy on AtO_AtKId=AtK_ID
			 where AtO_GIDTyp=3584 and AtO_Element=1 and AtK_Automat=1
			 )
			insert into cdn.Atrybuty( [Atr_ObiTyp], [Atr_ObiFirma], [Atr_ObiNumer], [Atr_ObiLp], [Atr_ObiSubLp], [Atr_AtkId], [Atr_Wartosc], [Atr_AtrTyp], [Atr_AtrFirma], [Atr_AtrNumer], [Atr_AtrLp], [Atr_AtrSubLp], [Atr_OptimaId], [Atr_Grupujacy])
			 select 3584,408066,@rleid,0,3586,atk_id,'',0,0,0,0,0,0,0 
			 from s left join cdn.Atrybuty on Atr_ObiNumer=id and atk_id=Atr_AtkId
			 where  isnull(Atr_ObiSubLp,3586)=3586 and atr_id is null
		end

		FETCH NEXT FROM cur into @lp,@twrnumer,@twrkod,@ilosc,@zrdtyp,@zrdnumer,@zrdlp,@przyczyna,@dokumentobcy,@sn,@AtrybutDokumentDataSprzedazy,@AtrybutRodzajZgloszenia;  
		END;
		close cur
		deallocate cur
	end

	
	if @rlnid<>0 and @zalRozmiar<>0 exec dbo.b2b_AddAttachement @zal,@zalRozmiar,@zalRozszerzenie,@zalNazwa,'z',0,@rlnid,3584,0,@dabid output 

	commit tran
	select @rlnid rlnid,@dokumentnr doknr,@dabid dabid,RLN_Stan,RLN_Status,RLE_Pozycja,RLE_TwrKod,RLE_Ilosc from cdn.ReklNag join cdn.ReklElem on rln_id=RLE_RLNId where rln_id=@rlnid

	end
end




--declare @p1 dbo.b2b_udttReklamacja
--insert into @p1 values(N'32743',N'2564',N'864',N'77199',N'32743',N'864',N'77199',N'opis reklamacji')

--declare @p2 dbo.b2b_udttReklamacjaElementy
--insert into @p2 values(N'1',N'39909',N'DS-2CD2625FWD-IZS(2.8-12MM)(BLACK)',N'1',N'2033',N'741632',N'1',N'botak',N'cokolwiek')
--insert into @p2 values(N'2',N'39797',N'DS-2CD2043G0-I(2.8MM)(BLACK)',N'1',N'2033',N'741632',N'2',N'botak',N'cokolwiek')

----exec dbo.b2b_spNowaReklamacja @nag=@p1,@elem=@p2

