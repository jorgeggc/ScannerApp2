CREATE TABLE [dbo].[AccessLogs] (
    [AccessLogID]      INT          IDENTITY (1, 1) NOT NULL,
    [AccessLocationID] INT          NOT NULL,
    [StationID]        VARCHAR (25) NOT NULL,
    [AccessDate]       DATETIME     NOT NULL,
    [IDCardNumber]     INT          NULL,
    [DeclineReason]    VARCHAR (15) NULL,
    [OperatorLogin]    VARCHAR (15) NOT NULL,
    CONSTRAINT [PK_AccessLogs] PRIMARY KEY CLUSTERED ([AccessLogID] ASC),
    CONSTRAINT [FK_AccessLogs_AccessLocations] FOREIGN KEY ([AccessLocationID]) REFERENCES [dbo].[AccessLocations] ([AccessLocationID])
);

