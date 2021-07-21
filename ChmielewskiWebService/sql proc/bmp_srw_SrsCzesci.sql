-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE bmp_srw_SrsCzesci
	@SrC_SrZId int
AS
BEGIN
	SELECT [SrC_SrCId]
      ,[SrC_SrZId]
      ,[SrC_Lp]
      ,[SrC_TwrId]
      ,[SrC_KatID]
      ,[SrC_MmTreID]
      ,[SrC_MmZwrot]
      ,[SrC_Opis]
      ,[SrC_SerwisantTyp]
      ,[SrC_SerwisantId]
      ,[SrC_MagId]
      ,[SrC_Status]
      ,[SrC_Dokument]
      ,[SrC_Fakturowac]
      ,[SrC_TwCNumer]
      ,[SrC_IloscPobierana]
      ,[SrC_IloscPobieranaJM]
      ,[SrC_Ilosc]
      ,[SrC_IloscJM]
      ,[SrC_JM]
      ,[SrC_Atr1_DeAId]
      ,[SrC_Atr1_Kod]
      ,[SrC_Atr1_Wartosc]
      ,[SrC_Atr2_DeAId]
      ,[SrC_Atr2_Kod]
      ,[SrC_Atr2_Wartosc]
      ,[SrC_Atr3_DeAId]
      ,[SrC_Atr3_Kod]
      ,[SrC_Atr3_Wartosc]
      ,[SrC_Atr4_DeAId]
      ,[SrC_Atr4_Kod]
      ,[SrC_Atr4_Wartosc]
      ,[SrC_Atr5_DeAId]
      ,[SrC_Atr5_Kod]
      ,[SrC_Atr5_Wartosc]
  FROM [CDN].[SrsCzesci]
  where [SrC_SrZId]=@SrC_SrZId
  order by [SrC_Lp]
END
GO
