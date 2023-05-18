CREATE TABLE [dbo].[Attacks_Pokemons] (
    [pokemon_id] BIGINT NOT NULL,
    [attack_id]  BIGINT NOT NULL,
    CONSTRAINT [FK_Attacks_AttacksPokemons] FOREIGN KEY ([attack_id]) REFERENCES [dbo].[Attacks] ([id]),
    CONSTRAINT [FK_Pokemons_AttacksPokemons] FOREIGN KEY ([pokemon_id]) REFERENCES [dbo].[Pokemons] ([id])
);

