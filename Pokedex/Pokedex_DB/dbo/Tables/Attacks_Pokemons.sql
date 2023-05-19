CREATE TABLE [dbo].[Attacks_Pokemons] (
    [pokemon_id] BIGINT NOT NULL,
    [attack_id]  BIGINT NOT NULL,
    CONSTRAINT [PK_Attacks_Pokemons] PRIMARY KEY CLUSTERED ([pokemon_id] ASC, [attack_id] ASC),
    CONSTRAINT [FK_Attacks_AttacksPokemons] FOREIGN KEY ([attack_id]) REFERENCES [dbo].[Attacks] ([id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Pokemons_AttacksPokemons] FOREIGN KEY ([pokemon_id]) REFERENCES [dbo].[Pokemons] ([id]) ON DELETE CASCADE
);

