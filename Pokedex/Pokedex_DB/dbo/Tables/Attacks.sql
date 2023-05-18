CREATE TABLE [dbo].[Attacks] (
    [id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [name]        VARCHAR (30)  NOT NULL,
    [damage]      INT           NOT NULL,
    [description] VARCHAR (200) NOT NULL,
    [accuracy]    INT           NOT NULL,
    CONSTRAINT [PK_Attacks] PRIMARY KEY CLUSTERED ([id] ASC)
);

