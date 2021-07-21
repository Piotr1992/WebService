create procedure [dbo].[b2b_twrzamienniki] 
AS
SELECT 
TwP_TwrNumer
,TwP_ZamNumer
,TwP_PrzeliczL
,TwP_PrzeliczM
  FROM [CDNXL_Bemko].[CDN].[TwrPodm]