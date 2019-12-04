CREATE TABLE [dbo].[IdentificationCards] (
    [IdentificationCardID] INT           IDENTITY (1, 1) NOT NULL,
    [Name]                 VARCHAR (120) NOT NULL,
    [OrgStructure]         VARCHAR (100) NOT NULL,
    [PhoneNumber]          VARCHAR (15)  NULL,
    [EmailAddress]         VARCHAR (128) NULL,
    [HireDate]             DATETIME      NULL,
    [CardExpireDate]       DATETIME      NOT NULL,
    [TerminationDate]      DATETIME      NULL,
    [WorkerTypeID]         INT           NOT NULL,
    [Company]              VARCHAR (150) NULL,
    [CourtAccessRequired]  BIT           NOT NULL,
    [IDCardNumber]         INT           NOT NULL,
    [DepatAbrev]           CHAR (3)      NOT NULL,
    [Department]           VARCHAR (120) NOT NULL,
    CONSTRAINT [PK_IdentificationCards] PRIMARY KEY CLUSTERED ([IdentificationCardID] ASC),
    CONSTRAINT [FK_IdentificationCards_WorkerTypes] FOREIGN KEY ([WorkerTypeID]) REFERENCES [dbo].[WorkerTypes] ([WorkerTypeID])
);

