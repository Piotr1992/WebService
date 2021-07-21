CREATE FUNCTION [dbo].[b2b_CenyKlientow] ()
RETURNS TABLE
AS
RETURN 

select 
Knt_GIDNumer Knp_GIDNumer
,TwC_TwrNumer Twr_Gidnumer
,TwC_Waluta Waluta	
,TwC_Wartosc Cena
from cdn.KntKarty
join cdn.TwrCeny on TwC_TwrLp=case when Knt_Cena=0 then 1 else Knt_Cena end
where exists (select top 1 1 from cdn.Atrybuty where Atr_ObiNumer=Knt_GIDNumer and Atr_ObiTyp=32 and Atr_Wartosc='tak' and Atr_AtkId=11)
and TwC_TwrNumer in (select * from dbo.bmp_TowaryGrupy(2527,''))

GO