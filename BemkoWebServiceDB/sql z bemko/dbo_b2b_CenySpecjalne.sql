ALTER  PROCEDURE [dbo].[dbo_b2b_CenySpecjalne] (@kntgidnumer int)

AS

Create Table #Towary
(
knt_gidnumer int
,twr_gidnumer int
,twr_gidtyp int
,knu_typ int
,knu_upust decimal(15,4)
,knu_symbol varchar(5)
)

insert into #towary
select knu_kntnumer, Knu_TwgNumer, Knu_TwgTyp, Knu_Typ, KnU_Upust, KnU_Symbol
from cdn.KnTUpusty
where Knu_TwgTyp=16 


declare @kntnumer int
declare @twrnumer int
declare @twrtyp int
declare @knutyp int
declare @knuupust decimal(15,4)
declare @knusymbol varchar(5)
declare @cennik int

set @cennik=1

select @cennik=knt_cena from cdn.KntKarty where Knt_GIDNumer=@kntgidnumer

if @cennik=0 begin set @cennik=1 end


declare list Cursor local fast_forward for
select knu_kntnumer, Knu_TwgNumer, Knu_TwgTyp, Knu_Typ, KnU_Upust, KnU_Symbol
from cdn.KnTUpusty
where Knu_TwgTyp=-16


open list
fetch next from list into @kntnumer, @twrnumer, @twrtyp, @knutyp, @knuupust, @knusymbol
while @@FETCH_STATUS = 0
			begin 
			
			insert into #towary
			select @kntnumer, twr_gidnumer, 16, @knutyp, @knuupust, @knusymbol
			from cdn.TwrKarty
			where Twr_GIDNumer in (select * from dbo.bmp_towarygrupy(@twrnumer,''))
			
			
			fetch next from list into @kntnumer, @twrnumer, @twrtyp, @knutyp, @knuupust, @knusymbol
			end 
close list
deallocate list

select *
from(

select 
knt_gidnumer
,twr_gidnumer
,case when typpromocji=3 then wartoscpromocji else cast (case when Knu_Typ=1 then CenaKatalogowa*(1-(KnU_Upust+Rabat+wartoscpromocji)/100)
else case when Knu_Typ=3 then KnU_Upust else CenaKatalogowa end end as decimal(15,2)) end twr_CenaKontrahenta
,case when knu_symbol='' then (select twc_waluta from cdn.twrceny where twc_twrnumer=twr_gidnumer and twc_twrlp=@cennik) else knu_symbol end Waluta
from
(
select *
,isnull((select typ from  [dbo].[b2b_cenapromocyjna] (twr_gidnumer, knt_gidnumer)),0) typpromocji
,isnull((select wartosc from  [dbo].[b2b_cenapromocyjna] (twr_gidnumer, knt_gidnumer)),0) wartoscpromocji
,(select twc_wartosc from cdn.TwrCeny where TwC_TwrNumer=twr_gidnumer and TwC_TwrLp=1) as CenaKatalogowa
,(select Knt_Rabat from cdn.KntKarty where #towary.knt_gidnumer=cdn.kntkarty.knt_gidnumer) Rabat
 from #Towary
 where Twr_GIDNumer in (select * from dbo.bmp_towarygrupy(2527,''))
 and exists (select top 1 1 from cdn.Atrybuty where Atr_ObiNumer=knt_gidnumer and Atr_ObiTyp=32 and Atr_Wartosc='tak' and Atr_AtkId=11)
)a
where CenaKatalogowa<>0
)a
where knt_gidnumer=@kntgidnumer

drop table #Towary




