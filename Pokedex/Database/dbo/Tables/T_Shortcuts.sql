CREATE TABLE [dbo].[T_Shortcuts] (
    [id]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [url]       VARCHAR (100) NOT NULL,
    [hash]      NCHAR (32)    NOT NULL,
    [sessionId] VARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_T_Shorcuts] PRIMARY KEY CLUSTERED ([id] ASC)
);

