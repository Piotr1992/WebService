create view dbo.b2b_vReklamacjeAtrybutySlowniki as
select atk_id,AtK_Nazwa,AtW_Wartosc 
from 
	cdn.AtrybutyKlasy join cdn.AtrybutyWartosci on atk_id=AtW_AtKId
	
where atk_id in (1,398,401)