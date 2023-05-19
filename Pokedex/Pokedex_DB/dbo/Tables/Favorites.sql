CREATE TABLE [dbo].[Favorites] (
    [pokemon_id] BIGINT NOT NULL,
    [user_id]    BIGINT NOT NULL,
    CONSTRAINT [PK_Favorites] PRIMARY KEY CLUSTERED ([pokemon_id] ASC, [user_id] ASC),
    CONSTRAINT [FK_Pokemons_Favorites] FOREIGN KEY ([pokemon_id]) REFERENCES [dbo].[Pokemons] ([id]),
    CONSTRAINT [FK_Users_Favorites] FOREIGN KEY ([user_id]) REFERENCES [dbo].[Users] ([id])
);

