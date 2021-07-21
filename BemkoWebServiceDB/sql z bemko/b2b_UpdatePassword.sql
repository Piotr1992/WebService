CREATE PROCEDURE [dbo].[b2b_UpdatePassword] 
	(@KnS_EMail varchar(255),
	@KnS_HasloOsoby varchar(11))
AS
BEGIN
	update cdn.KntOsoby set KnS_HasloOsoby=@KnS_HasloOsoby
                                where KnS_EMail=@KnS_EMail and
                                exists (select top 1 1 from cdn.Atrybuty where Atr_ObiNumer=KnS_KntNumer and Atr_ObiTyp=32 and Atr_Wartosc='tak' and Atr_AtkId=11)
                                and KnS_UpowaznionaZam=1
END
