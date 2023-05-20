CREATE TABLE [dbo].[Attacks] (
    [id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [name]        VARCHAR (30)  NOT NULL,
    [damage]      INT           NOT NULL,
    [description] VARCHAR (200) NOT NULL,
    [accuracy]    INT           NOT NULL,
    [type]        NCHAR (10)    NOT NULL,
    CONSTRAINT [PK_Attacks] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [CK_Attacks_Type] CHECK ([type]='???' OR [type]='Fée' OR [type]='Acier' OR [type]='Ténèbres' OR [type]='Spectre' OR [type]='Dragon' OR [type]='Insecte' OR [type]='Glace' OR [type]='Roche' OR [type]='Sol' OR [type]='Poison' OR [type]='Vol' OR [type]='Psy' OR [type]='Electrique' OR [type]='Plante' OR [type]='Eau' OR [type]='Combat' OR [type]='Normal' OR [type]='Feu')
);

