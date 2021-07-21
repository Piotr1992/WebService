USE [Cas]
GO
/****** Object:  UserDefinedFunction [dbo].[b2b_adresykontrahentow]    Script Date: 2015-03-25 18:35:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER function  [dbo].[b2b_adresykontrahentow] ()
returns table
as
return
select 
Knt_GIDNumer
,KnA_GIDNumer
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
from cdn.KntKarty
join cdn.KntAdresy a on KnA_KntNumer=Knt_GIDNumer
where knt_typ in (16,24)
and Knt_Archiwalny=0
and Knt_Wsk=0
and KnA_DataArc=0