

create proc dbo.b2b_spDodajAtrybutReklamacji @rlnId int, @atkId int, @atrWartosc varchar(200)
as
begin
set nocount on

insert into cdn.Atrybuty( [Atr_ObiTyp], [Atr_ObiFirma], [Atr_ObiNumer], [Atr_ObiLp], [Atr_ObiSubLp], [Atr_AtkId], [Atr_Wartosc], [Atr_AtrTyp], [Atr_AtrFirma], [Atr_AtrNumer], [Atr_AtrLp], [Atr_AtrSubLp], [Atr_OptimaId], [Atr_Grupujacy])
values (3584,408066,@rlnid,0,0,@atkid,@atrwartosc,0,0,0,0,0,0,0)

set nocount off
end
GO


