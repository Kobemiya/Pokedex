CREATE TABLE [dbo].[Pokemons] (
    [id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [name]        VARCHAR (30)  NOT NULL,
    [def]         INT           NOT NULL,
    [def_spe]     INT           NOT NULL,
    [attack]      INT           NOT NULL,
    [attack_spe]  INT           NOT NULL,
    [speed]       INT           NOT NULL,
    [hp]          INT           NOT NULL,
    [type_1]      VARCHAR (10)  NOT NULL,
    [type_2]      VARCHAR (10)  NULL,
    [description] VARCHAR (200) NOT NULL,
    [image_path]  VARCHAR (100) NULL,
    CONSTRAINT [PK_Pokemons] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [CK_Pokemons_Type1] CHECK ([type_1]='???' OR [type_1]='Fée' OR [type_1]='Acier' OR [type_1]='Ténèbres' OR [type_1]='Spectre' OR [type_1]='Dragon' OR [type_1]='Insecte' OR [type_1]='Glace' OR [type_1]='Roche' OR [type_1]='Sol' OR [type_1]='Poison' OR [type_1]='Vol' OR [type_1]='Psy' OR [type_1]='Electrique' OR [type_1]='Plante' OR [type_1]='Eau' OR [type_1]='Combat' OR [type_1]='Normal' OR [type_1]='Feu'),
    CONSTRAINT [CK_Pokemons_Type2] CHECK ([type_2]='???' OR [type_2]='Fée' OR [type_2]='Acier' OR [type_2]='Ténèbres' OR [type_2]='Spectre' OR [type_2]='Dragon' OR [type_2]='Insecte' OR [type_2]='Glace' OR [type_2]='Roche' OR [type_2]='Sol' OR [type_2]='Poison' OR [type_2]='Vol' OR [type_2]='Psy' OR [type_2]='Electrique' OR [type_2]='Plante' OR [type_2]='Eau' OR [type_2]='Combat' OR [type_2]='Normal' OR [type_2]='Feu')
);

