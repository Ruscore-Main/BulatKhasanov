
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/10/2022 23:09:39
-- Generated from EDMX file: D:\3 курс 2 семестр\MONEY\BulatKhasanov\BulatKhasanov\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [C:\USERS\RUSLAN\DOCUMENTS\BULATKHASANOV.MDF];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_WorkPlan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WorkSet] DROP CONSTRAINT [FK_WorkPlan];
GO
IF OBJECT_ID(N'[dbo].[FK_EmployeePlan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlanSet] DROP CONSTRAINT [FK_EmployeePlan];
GO
IF OBJECT_ID(N'[dbo].[FK_BuildObjectPlan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlanSet] DROP CONSTRAINT [FK_BuildObjectPlan];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[WorkSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WorkSet];
GO
IF OBJECT_ID(N'[dbo].[BuildObjectSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BuildObjectSet];
GO
IF OBJECT_ID(N'[dbo].[EmployeeSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeSet];
GO
IF OBJECT_ID(N'[dbo].[PlanSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlanSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Patronymic] nvarchar(max)  NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Post] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'WorkSet'
CREATE TABLE [dbo].[WorkSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [WorkName] nvarchar(max)  NOT NULL,
    [Price] int  NOT NULL,
    [Scale] int  NOT NULL,
    [IsDone] bit  NOT NULL,
    [PlanContractNumber] int  NULL,
    [EmployeeId] int  NOT NULL
);
GO

-- Creating table 'BuildObjectSet'
CREATE TABLE [dbo].[BuildObjectSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [ContactPerson] nvarchar(max)  NOT NULL,
    [CustomerNumber] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'EmployeeSet'
CREATE TABLE [dbo].[EmployeeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL,
    [Patronymic] nvarchar(max)  NOT NULL,
    [DateOfBirth] nvarchar(max)  NOT NULL,
    [Classification] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PlanSet'
CREATE TABLE [dbo].[PlanSet] (
    [ContractNumber] int  NOT NULL,
    [WorkBeginning] nvarchar(max)  NOT NULL,
    [WorkEnding] nvarchar(max)  NOT NULL,
    [EmployeeId] int  NOT NULL,
    [BuildObject_Id] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WorkSet'
ALTER TABLE [dbo].[WorkSet]
ADD CONSTRAINT [PK_WorkSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BuildObjectSet'
ALTER TABLE [dbo].[BuildObjectSet]
ADD CONSTRAINT [PK_BuildObjectSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeSet'
ALTER TABLE [dbo].[EmployeeSet]
ADD CONSTRAINT [PK_EmployeeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ContractNumber] in table 'PlanSet'
ALTER TABLE [dbo].[PlanSet]
ADD CONSTRAINT [PK_PlanSet]
    PRIMARY KEY CLUSTERED ([ContractNumber] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PlanContractNumber] in table 'WorkSet'
ALTER TABLE [dbo].[WorkSet]
ADD CONSTRAINT [FK_WorkPlan]
    FOREIGN KEY ([PlanContractNumber])
    REFERENCES [dbo].[PlanSet]
        ([ContractNumber])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WorkPlan'
CREATE INDEX [IX_FK_WorkPlan]
ON [dbo].[WorkSet]
    ([PlanContractNumber]);
GO

-- Creating foreign key on [BuildObject_Id] in table 'PlanSet'
ALTER TABLE [dbo].[PlanSet]
ADD CONSTRAINT [FK_BuildObjectPlan]
    FOREIGN KEY ([BuildObject_Id])
    REFERENCES [dbo].[BuildObjectSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BuildObjectPlan'
CREATE INDEX [IX_FK_BuildObjectPlan]
ON [dbo].[PlanSet]
    ([BuildObject_Id]);
GO

-- Creating foreign key on [EmployeeId] in table 'WorkSet'
ALTER TABLE [dbo].[WorkSet]
ADD CONSTRAINT [FK_WorkEmployee]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[EmployeeSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WorkEmployee'
CREATE INDEX [IX_FK_WorkEmployee]
ON [dbo].[WorkSet]
    ([EmployeeId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------