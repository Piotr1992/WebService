
alter PROCEDURE CDN.bmp_XLDodajPozycjeReklamacji
@RlnId                  INT,                            -- identyfikator reklamacji
@ZrdTyp                 SMALLINT = NULL,        -- GID dokumentu reklamowanego
@ZrdNumer               INT = NULL,
@ZrdLp                  SMALLINT = NULL,
@DokNumer               VARCHAR(40)= NULL,      -- Numer dokumentu
@TwrTyp                 SMALLINT = NULL,        -- GID reklamowanego towaru
@TwrNumer               INT = NULL,
@twrkod					varchar(100)=null,
@Ilosc                  DECIMAL(11,4),          -- iloœæ
@JmFormat               tinyINT,                        -- miejsca po przecinku
@Przyczyna              VARCHAR(1999),          -- przyczyna
@Zadanie                INT,                            -- identyfikator ¿¹dania reklamuj¹cego
@RLEOldElemId   INT = NULL,
@KntNumer               INT,
@OpeNumer               INT,
@RleId          INT OUTPUT,                      -- Identyfikator elementu reklamacji
@DokumentObcy varchar(200)
-- Zwraca numer b³edu:
-- 2 - nie istnieje wskazany kontrahent
-- 5 - nie powiod³o siê dodawanie do ReklElem
-- 6 - nie powiod³o siê dodawanie do ReklRealizacja
-- 8 - nie znaleziono reklamacji o podanym identyfikatorze
AS
IF NOT EXISTS(SELECT * FROM CDN.ReklNag WHERE RLN_Id=@RlnId)
BEGIN
        RAISERROR ('XLDodajPozycjeReklamacji: Nie znaleziono reklamacji o podanym identyfikatorze.', 16, 1)
        RETURN 8
END
IF NOT EXISTS(SELECT * FROM CDN.KntKarty WHERE Knt_GIDNumer=@KntNumer)
BEGIN
        RAISERROR ('XLDodajPozycjeReklamacji: Nie istnieje wskazany kontrahent.', 16, 1)
        RETURN 2
END
BEGIN TRAN
INSERT INTO CDN.ReklElem(
        RLE_RLNId,
        RLE_Pozycja,
    RLE_ZrdTyp,
    RLE_ZrdNumer,
    RLE_ZrdLp,
    RLE_DokNumer,
    RLE_TwrTyp,
    RLE_TwrNumer,
    RLE_TwrNazwa,
    RLE_TwrKod,
    RLE_TypTwr,
        RLE_Ilosc,
        RLE_JmFormat,
    RLE_Zadanie,
    RLE_Status,
    RLE_Opis,
    RLE_Przyczyna,
    RLE_Rozpatrzenie,
    RLE_MagNumer,
    RLE_ZrdDokumentObcy,
    RLE_OddElemId)
SELECT
        @RlnId,
        (select ISNULL(MAX(RLE_Pozycja),0)+1 from CDN.ReklElem where RLE_RLNId = @RlnId),
    ISNULL(TrE_GIDTyp,0),
    ISNULL(TrE_GIDNumer,0),
    ISNULL(TrE_GIDLp,0),
    ISNULL(@DokNumer,ISNULL(CDN.NumerDokumentu(TrN_GIDTyp,TrN_SpiTyp,TrN_TrNTyp,TrN_TrNNumer,TrN_TrNRok,TrN_TrNSeria,TrN_TrNMiesiac),0)),
    ISNULL(TrE_TwrTyp,Twr_GIDTyp),
    ISNULL(TrE_TwrNumer,Twr_GIDNumer),
    coalesce(TrE_TwrNazwa,case when twr_gidnumer=0 then @twrkod else null end,Twr_Nazwa),
    ISNULL(TrE_TwrKod,Twr_Kod),
    ISNULL(TrE_TypTwr,Twr_Typ),
        @Ilosc,
        @JmFormat,
    @Zadanie,
    0,
    '',
    @Przyczyna,
    '',
    0,
    isnull(@dokumentobcy,''),
    ISNULL(@RLEOldElemId,0)
FROM CDN.TwrKarty
FULL OUTER JOIN CDN.TraElem ON Twr_GIDNumer = TrE_TwrNumer AND TrE_GIDTyp = @ZrdTyp AND TrE_GIDNumer = @ZrdNumer AND TrE_GIDLp = @ZrdLp
LEFT OUTER JOIN CDN.TraNag ON TrE_GIDNumer = TrN_GIDNumer
WHERE Twr_GIDNumer = ISNULL(@TwrNumer,TrE_TwrNumer) AND NOT (ISNULL(@TwrNumer,0)<> 0 AND ISNULL(@ZrdTyp,0)<>0 AND TrE_TwrNumer IS NULL)

IF @@ROWCOUNT=0 BEGIN
        RAISERROR ('XLDodajPozycjeReklamacjis: Nie powiod³o siê dodawanie wpisu do tabeli CDN.ReklElem.', 16, 1)
        ROLLBACK TRAN
        RETURN 5
END


SET @RleId = SCOPE_IDENTITY()

INSERT INTO CDN.ReklRealizacja(
        RLR_RLEId,
        RLR_RODId,
        RLR_Nazwa,
        RLR_Opis,
        RLR_StanPo,
    RLR_DokTyp,
    RLR_DokNumer,
    RLR_DokLp,
    RLR_DataWykonania,
        RLR_OpeTyp,
        RLR_OpeNumer,
        RLR_Opublikowana,
        RLR_Status,
        RLR_MagNumer,
        RLR_RODKluczowa
)
SELECT
        @RleID,
        ROD_Id,
        ROD_Nazwa,
        '',
        ROD_StanPo,
        0,
        0,
        0,
        DATEDIFF(d,'18001228',GETDATE()),
    Ope_GIDTyp,
    Ope_GIDNumer,
        ROD_Opublikowana,
        ROD_Status,
        0,
        ROD_Id
FROM   CDN.ReklOperacjeDef
LEFT OUTER JOIN CDN.KntAplikacje ON KAp_KntNumer = @KntNumer
INNER JOIN CDN.OpeKarty ON Ope_GIDNumer=@OpeNumer OR (Ope_GIDNumer=KAp_OpeNumer AND @OpeNumer IS NULL)
WHERE  ROD_Id = -1 --dodanie elementu

IF @@ROWCOUNT=0 BEGIN
        RAISERROR ('XLDodajPozycjeReklamacji: Nie powiod³o siê dodawanie wpisu do tabeli CDN.ReklRealizacja.', 16, 1)
        ROLLBACK TRAN
        RETURN 6
END

COMMIT TRAN
RETURN 0

