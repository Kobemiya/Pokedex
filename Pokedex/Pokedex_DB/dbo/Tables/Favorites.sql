CREATE TABLE [dbo].[Favorites] (
    [pokemon_id] BIGINT NOT NULL,
    [user_id]    BIGINT NOT NULL,
    CONSTRAINT [FK_Pokemons_Favorites] FOREIGN KEY ([pokemon_id]) REFERENCES [dbo].[Pokemons] ([id]),
    CONSTRAINT [FK_Users_Favorites] FOREIGN KEY ([user_id]) REFERENCES [dbo].[Users] ([id])
);

