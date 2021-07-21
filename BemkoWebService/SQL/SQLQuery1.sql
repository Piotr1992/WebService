CREATE TYPE dbo.bmp_udttReklamacja AS TABLE(
    KntNumer int not null,

    ZadanieId int not null,
    KnaTyp int null,
    KnaNumer int null,
    KndNumer int null,
    AdwTyp int null,
    AdwNumer int null,
    Opis varchar(1999) null
)
GO
Create Type dbo.bmp_udttReklamacjaElementy as Table (
    Lp int not null identity(1,1),
    TwrNumer int null,
    TwrKod varchar(40) null,
    Ilosc decimal(11,4) not null,
    ZrdTyp int,
    ZrdNumer int,
    ZrdLp int,
    Przyczyna varchar(1999),
    DokumentObcy varchar(40)
)


--towar 14755
--knt 219
--typ 3584
--zadanie 2563
declare @rlnid int
declare @dokumentnr varchar(40)
exec cdn.XLNowaReklamacja @rlntyp=3584,@rodid=-1
    ,@knttyp=32,@kntnumer=219,@kntlp=0
    ,@twrtyp=16,@twrnumer=14755
    ,@ilosc=1,@jmformat=0
    ,@dokumentnr=@dokumentnr output
    ,@przyczyna='',@opis='',@zadanie=2563,@dokumentobcy=''
    ,@openumer=149
    ,@id=@rlnid output  

    select @rlnid,@dokumentnr


    select distinct * from dbo.b2b_operatorzy() where KnS_EMail like '%bmp%'