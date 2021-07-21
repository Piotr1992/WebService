--funkcja wybierajaca serie dokumentu na podstawie magazynu

CREATE FUNcTION [dbo].[bmp_b2b_SeriaMagazynu] (@magnumer int)
returns varchar(50)

as
begin
declare @r as nvarchar(50)=null
--select @r=ser_nazwa from
--cdn.magazyny join cdn.KntKarty on mag_kntnumer=Knt_GIDNumer and MAG_KntNumer<>0
--join cdn.atrybuty on Atr_AtkId=106 and Atr_Obinumer=Knt_GIDNumer and Atr_ObiTyp=Knt_GIDtyp and atr_obifirma=Knt_GIDFirma and atr_obilp=Knt_GIDLp
--join cdn.FrmStruktura on atr_atrnumer=FRS_GIDNumer and Atr_AtrTyp=FRS_GIDTyp and Atr_AtrFirma=FRS_GIDFirma and Atr_AtrLp=FRS_GIDLp
--join cdn.serie on SER_GIDNumer=FRS_SERNumer
--where MAG_GIDNumer=@magnumer

return @r
end
GO
