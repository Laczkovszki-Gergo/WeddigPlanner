
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/09/2020 23:26:18
-- Generated from EDMX file: D:\C#\Eskuvo_tervezo\Eskuvo_tervezo\Models\WeddigPlannerModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [WeddingPlanner];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Calendar_Login]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Calendar] DROP CONSTRAINT [FK_Calendar_Login];
GO
IF OBJECT_ID(N'[dbo].[FK_CalendarLogEntrys_Calendar1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CalendarLogEntrys] DROP CONSTRAINT [FK_CalendarLogEntrys_Calendar1];
GO
IF OBJECT_ID(N'[dbo].[FK_Contacts_Login]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Contacts] DROP CONSTRAINT [FK_Contacts_Login];
GO
IF OBJECT_ID(N'[dbo].[FK_Guests_WeddingData]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Guests] DROP CONSTRAINT [FK_Guests_WeddingData];
GO
IF OBJECT_ID(N'[dbo].[FK_Log_Login]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Log] DROP CONSTRAINT [FK_Log_Login];
GO
IF OBJECT_ID(N'[dbo].[FK_WeddingData_Login]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WeddingData] DROP CONSTRAINT [FK_WeddingData_Login];
GO
IF OBJECT_ID(N'[dbo].[FK_WeddingExpenses_WeddingVenue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WeddingExpenses] DROP CONSTRAINT [FK_WeddingExpenses_WeddingVenue];
GO
IF OBJECT_ID(N'[dbo].[FK_WeddingVenue_WeddingData]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WeddingVenue] DROP CONSTRAINT [FK_WeddingVenue_WeddingData];
GO
IF OBJECT_ID(N'[dbo].[FK_WeddingVenueImages_WeddingVenue]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WeddingVenueImages] DROP CONSTRAINT [FK_WeddingVenueImages_WeddingVenue];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Calendar]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Calendar];
GO
IF OBJECT_ID(N'[dbo].[CalendarLogEntrys]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CalendarLogEntrys];
GO
IF OBJECT_ID(N'[dbo].[Contacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contacts];
GO
IF OBJECT_ID(N'[dbo].[Guests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Guests];
GO
IF OBJECT_ID(N'[dbo].[Log]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Log];
GO
IF OBJECT_ID(N'[dbo].[Login]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Login];
GO
IF OBJECT_ID(N'[dbo].[WeddingData]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WeddingData];
GO
IF OBJECT_ID(N'[dbo].[WeddingExpenses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WeddingExpenses];
GO
IF OBJECT_ID(N'[dbo].[WeddingVenue]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WeddingVenue];
GO
IF OBJECT_ID(N'[dbo].[WeddingVenueImages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WeddingVenueImages];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Calendar'
CREATE TABLE [dbo].[Calendar] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NOT NULL,
    [Date] datetime  NOT NULL
);
GO

-- Creating table 'CalendarLogEntrys'
CREATE TABLE [dbo].[CalendarLogEntrys] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CalID] int  NOT NULL,
    [LogEntry] nchar(250)  NOT NULL
);
GO

-- Creating table 'Contacts'
CREATE TABLE [dbo].[Contacts] (
    [Con_ID] int IDENTITY(1,1) NOT NULL,
    [User_Id] int  NOT NULL,
    [Con_Name] nchar(60)  NOT NULL,
    [Con_Phone] nchar(30)  NULL,
    [Con_Email] nchar(40)  NULL
);
GO

-- Creating table 'Guests'
CREATE TABLE [dbo].[Guests] (
    [Guest_ID] int IDENTITY(1,1) NOT NULL,
    [Wedding_ID] int  NOT NULL,
    [Guest_Name] nchar(50)  NOT NULL,
    [Guest_Count] int  NOT NULL,
    [Bride_Groom] int  NOT NULL
);
GO

-- Creating table 'Log'
CREATE TABLE [dbo].[Log] (
    [IDLog] int IDENTITY(1,1) NOT NULL,
    [UserID] int  NOT NULL,
    [WindowsUser] nchar(30)  NOT NULL,
    [TimeStamp] datetime  NOT NULL
);
GO

-- Creating table 'Login'
CREATE TABLE [dbo].[Login] (
    [IDLogin] int IDENTITY(1,1) NOT NULL,
    [User] nchar(30)  NOT NULL,
    [Password] nchar(120)  NOT NULL,
    [EmailAddress] nchar(120)  NOT NULL
);
GO

-- Creating table 'WeddingData'
CREATE TABLE [dbo].[WeddingData] (
    [ID] int  NOT NULL,
    [User_ID] int  NOT NULL,
    [Wedding_Date] datetime  NOT NULL,
    [BrideName] nchar(50)  NOT NULL,
    [GroomName] nchar(50)  NOT NULL,
    [Budget] int  NULL
);
GO

-- Creating table 'WeddingExpenses'
CREATE TABLE [dbo].[WeddingExpenses] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [VenueID] int  NOT NULL,
    [ExpenseName] nchar(60)  NOT NULL,
    [Expense] int  NOT NULL,
    [Count] int  NOT NULL
);
GO

-- Creating table 'WeddingVenue'
CREATE TABLE [dbo].[WeddingVenue] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [WeddingID] int  NOT NULL,
    [Wedding_Venue] nchar(60)  NOT NULL,
    [Venue_Address] nchar(60)  NOT NULL
);
GO

-- Creating table 'WeddingVenueImages'
CREATE TABLE [dbo].[WeddingVenueImages] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [WeddingVenueID] int  NOT NULL,
    [Image] varbinary(max)  NOT NULL,
    [ImageName] nchar(200)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Calendar'
ALTER TABLE [dbo].[Calendar]
ADD CONSTRAINT [PK_Calendar]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'CalendarLogEntrys'
ALTER TABLE [dbo].[CalendarLogEntrys]
ADD CONSTRAINT [PK_CalendarLogEntrys]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Con_ID] in table 'Contacts'
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [PK_Contacts]
    PRIMARY KEY CLUSTERED ([Con_ID] ASC);
GO

-- Creating primary key on [Guest_ID] in table 'Guests'
ALTER TABLE [dbo].[Guests]
ADD CONSTRAINT [PK_Guests]
    PRIMARY KEY CLUSTERED ([Guest_ID] ASC);
GO

-- Creating primary key on [IDLog] in table 'Log'
ALTER TABLE [dbo].[Log]
ADD CONSTRAINT [PK_Log]
    PRIMARY KEY CLUSTERED ([IDLog] ASC);
GO

-- Creating primary key on [IDLogin] in table 'Login'
ALTER TABLE [dbo].[Login]
ADD CONSTRAINT [PK_Login]
    PRIMARY KEY CLUSTERED ([IDLogin] ASC);
GO

-- Creating primary key on [ID] in table 'WeddingData'
ALTER TABLE [dbo].[WeddingData]
ADD CONSTRAINT [PK_WeddingData]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'WeddingExpenses'
ALTER TABLE [dbo].[WeddingExpenses]
ADD CONSTRAINT [PK_WeddingExpenses]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'WeddingVenue'
ALTER TABLE [dbo].[WeddingVenue]
ADD CONSTRAINT [PK_WeddingVenue]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'WeddingVenueImages'
ALTER TABLE [dbo].[WeddingVenueImages]
ADD CONSTRAINT [PK_WeddingVenueImages]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserID] in table 'Calendar'
ALTER TABLE [dbo].[Calendar]
ADD CONSTRAINT [FK_Calendar_Login]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Login]
        ([IDLogin])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Calendar_Login'
CREATE INDEX [IX_FK_Calendar_Login]
ON [dbo].[Calendar]
    ([UserID]);
GO

-- Creating foreign key on [CalID] in table 'CalendarLogEntrys'
ALTER TABLE [dbo].[CalendarLogEntrys]
ADD CONSTRAINT [FK_CalendarLogEntrys_Calendar1]
    FOREIGN KEY ([CalID])
    REFERENCES [dbo].[Calendar]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CalendarLogEntrys_Calendar1'
CREATE INDEX [IX_FK_CalendarLogEntrys_Calendar1]
ON [dbo].[CalendarLogEntrys]
    ([CalID]);
GO

-- Creating foreign key on [User_Id] in table 'Contacts'
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [FK_Contacts_Login]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Login]
        ([IDLogin])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Contacts_Login'
CREATE INDEX [IX_FK_Contacts_Login]
ON [dbo].[Contacts]
    ([User_Id]);
GO

-- Creating foreign key on [Wedding_ID] in table 'Guests'
ALTER TABLE [dbo].[Guests]
ADD CONSTRAINT [FK_Guests_WeddingData]
    FOREIGN KEY ([Wedding_ID])
    REFERENCES [dbo].[WeddingData]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Guests_WeddingData'
CREATE INDEX [IX_FK_Guests_WeddingData]
ON [dbo].[Guests]
    ([Wedding_ID]);
GO

-- Creating foreign key on [UserID] in table 'Log'
ALTER TABLE [dbo].[Log]
ADD CONSTRAINT [FK_Log_Login]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Login]
        ([IDLogin])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Log_Login'
CREATE INDEX [IX_FK_Log_Login]
ON [dbo].[Log]
    ([UserID]);
GO

-- Creating foreign key on [User_ID] in table 'WeddingData'
ALTER TABLE [dbo].[WeddingData]
ADD CONSTRAINT [FK_WeddingData_Login]
    FOREIGN KEY ([User_ID])
    REFERENCES [dbo].[Login]
        ([IDLogin])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WeddingData_Login'
CREATE INDEX [IX_FK_WeddingData_Login]
ON [dbo].[WeddingData]
    ([User_ID]);
GO

-- Creating foreign key on [WeddingID] in table 'WeddingVenue'
ALTER TABLE [dbo].[WeddingVenue]
ADD CONSTRAINT [FK_WeddingVenue_WeddingData]
    FOREIGN KEY ([WeddingID])
    REFERENCES [dbo].[WeddingData]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WeddingVenue_WeddingData'
CREATE INDEX [IX_FK_WeddingVenue_WeddingData]
ON [dbo].[WeddingVenue]
    ([WeddingID]);
GO

-- Creating foreign key on [VenueID] in table 'WeddingExpenses'
ALTER TABLE [dbo].[WeddingExpenses]
ADD CONSTRAINT [FK_WeddingExpenses_WeddingVenue]
    FOREIGN KEY ([VenueID])
    REFERENCES [dbo].[WeddingVenue]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WeddingExpenses_WeddingVenue'
CREATE INDEX [IX_FK_WeddingExpenses_WeddingVenue]
ON [dbo].[WeddingExpenses]
    ([VenueID]);
GO

-- Creating foreign key on [WeddingVenueID] in table 'WeddingVenueImages'
ALTER TABLE [dbo].[WeddingVenueImages]
ADD CONSTRAINT [FK_WeddingVenueImages_WeddingVenue]
    FOREIGN KEY ([WeddingVenueID])
    REFERENCES [dbo].[WeddingVenue]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WeddingVenueImages_WeddingVenue'
CREATE INDEX [IX_FK_WeddingVenueImages_WeddingVenue]
ON [dbo].[WeddingVenueImages]
    ([WeddingVenueID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------