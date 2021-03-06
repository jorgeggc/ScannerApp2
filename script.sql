USE [master]
GO
/****** Object:  Database [BuildingAccess]    Script Date: 4/27/2020 1:17:52 PM ******/
CREATE DATABASE [BuildingAccess]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BuildingAccess', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\BuildingAccess.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BuildingAccess_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\BuildingAccess_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [BuildingAccess] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BuildingAccess].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BuildingAccess] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BuildingAccess] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BuildingAccess] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BuildingAccess] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BuildingAccess] SET ARITHABORT OFF 
GO
ALTER DATABASE [BuildingAccess] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BuildingAccess] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BuildingAccess] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BuildingAccess] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BuildingAccess] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BuildingAccess] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BuildingAccess] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BuildingAccess] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BuildingAccess] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BuildingAccess] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BuildingAccess] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BuildingAccess] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BuildingAccess] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BuildingAccess] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BuildingAccess] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BuildingAccess] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BuildingAccess] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BuildingAccess] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BuildingAccess] SET  MULTI_USER 
GO
ALTER DATABASE [BuildingAccess] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BuildingAccess] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BuildingAccess] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BuildingAccess] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BuildingAccess] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BuildingAccess] SET QUERY_STORE = OFF
GO
USE [BuildingAccess]
GO
/****** Object:  Table [dbo].[AccessLocations]    Script Date: 4/27/2020 1:17:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessLocations](
	[AccessLocationID] [int] IDENTITY(1,1) NOT NULL,
	[LocationDesc] [varchar](50) NOT NULL,
 CONSTRAINT [PK_AccessLocations] PRIMARY KEY CLUSTERED 
(
	[AccessLocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccessLogs]    Script Date: 4/27/2020 1:17:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessLogs](
	[AccessLogID] [int] IDENTITY(1,1) NOT NULL,
	[AccessLocationID] [int] NOT NULL,
	[StationID] [varchar](25) NOT NULL,
	[AccessDate] [datetime] NOT NULL,
	[IDCardNumber] [int] NULL,
	[DeclineReason] [varchar](15) NULL,
	[OperatorLogin] [varchar](15) NOT NULL,
 CONSTRAINT [PK_AccessLogs] PRIMARY KEY CLUSTERED 
(
	[AccessLogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IdentificationCards]    Script Date: 4/27/2020 1:17:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IdentificationCards](
	[IdentificationCardID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](120) NOT NULL,
	[OrgStructure] [varchar](100) NOT NULL,
	[PhoneNumber] [varchar](15) NULL,
	[EmailAddress] [varchar](128) NULL,
	[HireDate] [datetime] NULL,
	[CardExpireDate] [datetime] NOT NULL,
	[TerminationDate] [datetime] NULL,
	[WorkerTypeID] [int] NOT NULL,
	[Company] [varchar](150) NULL,
	[CourtAccessRequired] [bit] NOT NULL,
	[IDCardNumber] [int] NOT NULL,
	[DepatAbrev] [char](3) NOT NULL,
	[Department] [varchar](120) NOT NULL,
 CONSTRAINT [PK_IdentificationCards] PRIMARY KEY CLUSTERED 
(
	[IdentificationCardID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OperatorLogin]    Script Date: 4/27/2020 1:17:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OperatorLogin](
	[LoginNum] [int] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkerTypes]    Script Date: 4/27/2020 1:17:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkerTypes](
	[WorkerTypeID] [int] IDENTITY(1,1) NOT NULL,
	[WorkerTypeDesc] [varchar](50) NOT NULL,
 CONSTRAINT [PK_WorkerTypes] PRIMARY KEY CLUSTERED 
(
	[WorkerTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AccessLogs]  WITH CHECK ADD  CONSTRAINT [FK_AccessLogs_AccessLocations] FOREIGN KEY([AccessLocationID])
REFERENCES [dbo].[AccessLocations] ([AccessLocationID])
GO
ALTER TABLE [dbo].[AccessLogs] CHECK CONSTRAINT [FK_AccessLogs_AccessLocations]
GO
ALTER TABLE [dbo].[IdentificationCards]  WITH CHECK ADD  CONSTRAINT [FK_IdentificationCards_WorkerTypes] FOREIGN KEY([WorkerTypeID])
REFERENCES [dbo].[WorkerTypes] ([WorkerTypeID])
GO
ALTER TABLE [dbo].[IdentificationCards] CHECK CONSTRAINT [FK_IdentificationCards_WorkerTypes]
GO
/****** Object:  StoredProcedure [dbo].[Insert_Access]    Script Date: 4/27/2020 1:17:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[Insert_Access] @AccessLocationID int, @IdentificationCardID int, @date int, @pass varchar(50), @firstname varchar(50)
AS
Begin
    Set NOCOUNT ON;
    INSERT INTO AccessLogs (AccessLocationID,StationID,AccessDate,IDCardNumber,DeclineReason,OperatorLogin)
    Values ((Select [AccessLocationID] FROM [AccessLocations] Where [AccessLocationID] = @AccessLocationID), +
           (Select [OrgStructure] FROM [IdentificationCards] Where [IdentificationCardID] = @IdentificationCardID), +
           SYSDATETIME(), +
           (Select [IDCardNumber] FROM [IdentificationCards] Where [IdentificationCardID] = @IdentificationCardID), + 
           (@pass), + 
           (Select [firstName] From [OperatorLogin] Where [firstName] = @firstname));
End
GO
/****** Object:  StoredProcedure [dbo].[Insert_Accesslog]    Script Date: 4/27/2020 1:17:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_Accesslog] @AccessLocationID int, @StationID varchar(10), @IDCardNumber int, @DeclineReason varchar(50), @OperatorLogin varchar(50)
AS
Begin
    Set NOCOUNT ON;
    INSERT INTO AccessLogs (AccessLocationID,StationID,AccessDate,IDCardNumber,DeclineReason,OperatorLogin)
    Values ((Select [AccessLocationID] FROM [AccessLocations] Where [AccessLocationID] = @AccessLocationID), +
           (@StationID), +
           SYSDATETIME(), +
           (Select [IDCardNumber] FROM [IdentificationCards] Where [IDCardNumber] = @IDCardNumber), + 
           (@DeclineReason), + 
           (Select [firstName] From [OperatorLogin] Where [LoginNum] = @OperatorLogin));
End
GO
/****** Object:  StoredProcedure [dbo].[LastThree_AccessLog]    Script Date: 4/27/2020 1:17:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[LastThree_AccessLog]
AS
Begin
    Set NOCOUNT ON;
    SELECT * FROM AccessLogs WHERE AccessLogID not in 
	(SELECT TOP (SELECT COUNT(1)-3 FROM AccessLogs) AccessLogID FROM AccessLogs)
End
GO
/****** Object:  StoredProcedure [dbo].[Load_Employees]    Script Date: 4/27/2020 1:17:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Load_Employees]
AS
select IdentificationCardID, Name, CourtAccessRequired, IDCardNumber from dbo.IdentificationCards
GO;
GO
/****** Object:  StoredProcedure [dbo].[Load_Location]    Script Date: 4/27/2020 1:17:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Load_Location]
AS
select AccessLocationID, LocationDesc from dbo.AccessLocations
GO;
GO
/****** Object:  StoredProcedure [dbo].[Load_ScannerLog]    Script Date: 4/27/2020 1:17:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Load_ScannerLog]
AS
select AccessLogID, AccessLocationID, StationID, AccessDate, IDCardNumber, DeclineReason from dbo.AccessLogs
GO;
GO
/****** Object:  StoredProcedure [dbo].[Load_ScannerLogFilter]    Script Date: 4/27/2020 1:17:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[Load_ScannerLogFilter]
AS
select AccessLogID, AccessLocationID, StationID, AccessDate, IDCardNumber, DeclineReason from dbo.AccessLogs
where IDCardNumber = 99991
GO
/****** Object:  StoredProcedure [dbo].[Select_Information]    Script Date: 4/27/2020 1:17:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:        <Author,,Name>
-- Create date: <Create Date,,>
-- Description:    <Description,,>
-- =============================================
Create PROCEDURE [dbo].[Select_Information] 
            @IDCardNumber int,
            @ReturnIDValue int OUTPUT,
            @ReturnIDAccess bit OUTPUT,
            @ReturnIDExpiration DATETIME OUTPUT,
            @ReturnIDTermination DATETIME OUTPUT

AS
BEGIN

    --Declare @TerminationUpdate DATETIME = CURRENT_TIMESTAMP

    if (exists (SELECT [IDCardNumber] FROM dbo.IdentificationCards
    WHERE [IDCardNumber] = @IDCardNumber))
    begin
        set @ReturnIDValue = 1;
        SELECT @ReturnIDAccess = CourtAccessRequired
        FROM [dbo].[IdentificationCards]
        WHERE [IDCardNumber] = @IDCardNumber

        SELECT @ReturnIDExpiration = CardExpireDate
        FROM [dbo].[IdentificationCards]
        WHERE [IDCardNumber] = @IDCardNumber

        SELECT @ReturnIDTermination = TerminationDate 
        FROM [dbo].[IdentificationCards] 
        WHERE [IDCardNumber] = @IDCardNumber
        SELECT @ReturnIDTermination = isNull(@ReturnIDTermination, DATEADD(hour, 1, CAST(CURRENT_TIMESTAMP as DateTime2)))

    end
    else
    begin
        set @ReturnIDValue = 0;
        set @ReturnIDAccess = 0;
        set @ReturnIDExpiration = 0;
        set @ReturnIDTermination = 0;
    end

    --return @ReturnIDValue
    --return @ReturnIDAccess

END
GO
USE [master]
GO
ALTER DATABASE [BuildingAccess] SET  READ_WRITE 
GO
