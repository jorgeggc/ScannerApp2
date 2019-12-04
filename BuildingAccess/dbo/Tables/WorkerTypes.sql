CREATE TABLE [dbo].[WorkerTypes] (
    [WorkerTypeID]   INT          IDENTITY (1, 1) NOT NULL,
    [WorkerTypeDesc] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_WorkerTypes] PRIMARY KEY CLUSTERED ([WorkerTypeID] ASC)
);

