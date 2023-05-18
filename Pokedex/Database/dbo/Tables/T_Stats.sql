CREATE TABLE [dbo].[T_Stats] (
    [id]    BIGINT IDENTITY (1, 1) NOT NULL,
    [idUrl] BIGINT NOT NULL,
    [date]  DATE   NOT NULL,
    CONSTRAINT [PK_T_Stats] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_T_Stats_T_Shorcuts] FOREIGN KEY ([idUrl]) REFERENCES [dbo].[T_Shortcuts] ([id])
);

