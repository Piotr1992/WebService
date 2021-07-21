-- tabela przez ktora mozna wybrac innego platnika dokumentu zamowienia na podstawie formy platnosci (pobranie)

CREATE TABLE [dbo].[bmp_b2b_platniFormyPlatnosci](
	[formaPlGid] [int] NOT NULL,
	[Knt_GIDTyp] [smallint] NULL,
	[Knt_GIDFirma] [int] NULL,
	[Knt_GIDNumer] [int] IDENTITY(1,1) NOT NULL,
	[knt_gidlp] [smallint] NULL,
	[Knt_KnAtyp] [smallint] NULL,
	[Knt_KnAFirma] [int] NULL,
	[Knt_KnANumer] [int] NULL,
	[knt_typ] [tinyint] NULL,
	[Knt_Akronim] [varchar](20) NULL
) ON [PRIMARY]
GO


