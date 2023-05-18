CREATE TABLE [dbo].[Pokemons] (
    [id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [name]        VARCHAR (30)  NOT NULL,
    [def]         INT           NOT NULL,
    [def_spe]     INT           NOT NULL,
    [attack]      INT           NOT NULL,
    [attack_spe]  INT           NOT NULL,
    [speed]       INT           NOT NULL,
    [hp]          INT           NOT NULL,
    [type_1]      NCHAR (10)    NOT NULL,
    [type_2]      NCHAR (10)    NULL,
    [description] VARCHAR (200) NOT NULL,
    [image_path]  VARCHAR (100) NULL,
    CONSTRAINT [PK_Pokemons] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [CK_Pokemons_type_1] CHECK ([type_1]='???' OR [type_1]='Fairy' OR [type_1]='Steel' OR [type_1]='Dark' OR [type_1]='Ghost' OR [type_1]='Dragon' OR [type_1]='Bug' OR [type_1]='Ice' OR [type_1]='Rock' OR [type_1]='Ground' OR [type_1]='Poison' OR [type_1]='Flying' OR [type_1]='Psychic' OR [type_1]='Electric' OR [type_1]='Grass' OR [type_1]='Water' OR [type_1]='Fighting' OR [type_1]='Normal' OR [type_1]='Fire'),
    CONSTRAINT [CK_Pokemons_type_2] CHECK ([type_2]='???' OR [type_2]='Fairy' OR [type_2]='Steel' OR [type_2]='Dark' OR [type_2]='Ghost' OR [type_2]='Dragon' OR [type_2]='Bug' OR [type_2]='Ice' OR [type_2]='Rock' OR [type_2]='Ground' OR [type_2]='Poison' OR [type_2]='Flying' OR [type_2]='Psychic' OR [type_2]='Electric' OR [type_2]='Grass' OR [type_2]='Water' OR [type_2]='Fighting' OR [type_2]='Normal' OR [type_2]='Fire')
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Checks if the 1st type of the pokemon is valid', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pokemons', @level2type = N'CONSTRAINT', @level2name = N'CK_Pokemons_type_1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Checks if the 2nd type of the pokemon is valid', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pokemons', @level2type = N'CONSTRAINT', @level2name = N'CK_Pokemons_type_2';

