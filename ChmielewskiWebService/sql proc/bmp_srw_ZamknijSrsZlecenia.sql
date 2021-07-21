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
ALTER PROCEDURE bmp_srw_ZamknijSrsZlecenia
	@SrZ_SrZId int,
	@SrZ_Opis nvarchar(max)
AS
BEGIN
	update [CDN].[SrsZlecenia]
	set [SrZ_Opis]=@SrZ_Opis
	where [SrZ_SrZId]=@SrZ_SrZId;
END
GO
