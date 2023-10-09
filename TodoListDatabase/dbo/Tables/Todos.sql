﻿CREATE TABLE [dbo].[Todos] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [Title]  NVARCHAR (50) NULL,
    [IsDone] BIT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

