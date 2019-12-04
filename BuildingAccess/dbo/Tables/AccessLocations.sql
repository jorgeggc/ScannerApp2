CREATE TABLE [dbo].[AccessLocations] (
    [AccessLocationID] INT          IDENTITY (1, 1) NOT NULL,
    [LocationDesc]     VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_AccessLocations] PRIMARY KEY CLUSTERED ([AccessLocationID] ASC)
);

