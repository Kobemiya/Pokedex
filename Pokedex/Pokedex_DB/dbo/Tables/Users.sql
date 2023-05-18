CREATE TABLE [dbo].[Users] (
    [id]       BIGINT       IDENTITY (1, 1) NOT NULL,
    [username] VARCHAR (30) NOT NULL,
    [password] VARCHAR (30) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Username]
    ON [dbo].[Users]([username] ASC);

