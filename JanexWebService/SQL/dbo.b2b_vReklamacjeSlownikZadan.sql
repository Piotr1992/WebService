alter view dbo.b2b_vReklamacjeSlownikZadan
as
--w webserwisie select * - dodanie kolumny zwroci ja w response
select SLW_ID ID, ltrim(rtrim(SLW_Nazwa)) Nazwa --,*
from CDN.Slowniki 
where slw_slsid=65 --SLW_Kategoria = '¯¹danie reklamuj¹cego'
and SLW_Aktywny=1