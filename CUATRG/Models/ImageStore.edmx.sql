
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/15/2018 22:52:43
-- Generated from EDMX file: D:\Git\CUATRG\CUATRG\Models\ImageStore.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [CUATRG];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_tblImages_tblAlbums]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblImages] DROP CONSTRAINT [FK_tblImages_tblAlbums];
GO
IF OBJECT_ID(N'[dbo].[FK_tblImages_tblEnvironmentalConditions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblImages] DROP CONSTRAINT [FK_tblImages_tblEnvironmentalConditions];
GO
IF OBJECT_ID(N'[dbo].[FK_tblImages_tblFeatures]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblImages] DROP CONSTRAINT [FK_tblImages_tblFeatures];
GO
IF OBJECT_ID(N'[dbo].[FK_tblMetaData_tblImages]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblMetaData] DROP CONSTRAINT [FK_tblMetaData_tblImages];
GO
IF OBJECT_ID(N'[dbo].[FK_tblMetaData_tblMetaType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblMetaData] DROP CONSTRAINT [FK_tblMetaData_tblMetaType];
GO
IF OBJECT_ID(N'[dbo].[FK_tblProcessedImages_tblColorModes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblProcessedImages] DROP CONSTRAINT [FK_tblProcessedImages_tblColorModes];
GO
IF OBJECT_ID(N'[dbo].[FK_tblProcessedImages_tblFilters]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblProcessedImages] DROP CONSTRAINT [FK_tblProcessedImages_tblFilters];
GO
IF OBJECT_ID(N'[dbo].[FK_tblProcessedImages_tblImages]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblProcessedImages] DROP CONSTRAINT [FK_tblProcessedImages_tblImages];
GO
IF OBJECT_ID(N'[dbo].[FK_tblSensorData_tblImages]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblSensorData] DROP CONSTRAINT [FK_tblSensorData_tblImages];
GO
IF OBJECT_ID(N'[dbo].[FK_tblSensorData_tblSensorDataType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tblSensorData] DROP CONSTRAINT [FK_tblSensorData_tblSensorDataType];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[tblAlbums]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblAlbums];
GO
IF OBJECT_ID(N'[dbo].[tblColorModes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblColorModes];
GO
IF OBJECT_ID(N'[dbo].[tblEnvironmentalConditions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblEnvironmentalConditions];
GO
IF OBJECT_ID(N'[dbo].[tblFeatures]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblFeatures];
GO
IF OBJECT_ID(N'[dbo].[tblFilters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblFilters];
GO
IF OBJECT_ID(N'[dbo].[tblImages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblImages];
GO
IF OBJECT_ID(N'[dbo].[tblMetaData]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblMetaData];
GO
IF OBJECT_ID(N'[dbo].[tblMetaType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblMetaType];
GO
IF OBJECT_ID(N'[dbo].[tblProcessedImages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblProcessedImages];
GO
IF OBJECT_ID(N'[dbo].[tblSensorData]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblSensorData];
GO
IF OBJECT_ID(N'[dbo].[tblSensorDataType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblSensorDataType];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tblAlbums'
CREATE TABLE [dbo].[tblAlbums] (
    [ALB_IDPkey] int IDENTITY(1,1) NOT NULL,
    [ALB_Name] nvarchar(50)  NOT NULL,
    [ALB_Description] nvarchar(500)  NULL
);
GO

-- Creating table 'tblColorModes'
CREATE TABLE [dbo].[tblColorModes] (
    [CMD_IDPkey] int  NOT NULL,
    [CMD_Name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'tblEnvironmentalConditions'
CREATE TABLE [dbo].[tblEnvironmentalConditions] (
    [ENC_IDPkey] int  NOT NULL,
    [ENC_Description] nvarchar(50)  NOT NULL,
    [ENC_Name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'tblFeatures'
CREATE TABLE [dbo].[tblFeatures] (
    [FTR_IDPkey] int  NOT NULL,
    [FTR_Name] nvarchar(50)  NULL
);
GO

-- Creating table 'tblFilters'
CREATE TABLE [dbo].[tblFilters] (
    [FLT_IDPkey] int  NOT NULL,
    [FLT_Name] nchar(10)  NULL
);
GO

-- Creating table 'tblProcessedImages'
CREATE TABLE [dbo].[tblProcessedImages] (
    [PIM_IDPkey] int IDENTITY(1,1) NOT NULL,
    [PIM_Name] nvarchar(50)  NOT NULL,
    [FLT_IDFkey] int  NOT NULL,
    [CMD_IDFkey] int  NOT NULL,
    [IMG_IDFkey] int  NOT NULL,
    [PIM_Path] nvarchar(500)  NOT NULL
);
GO

-- Creating table 'tblSensorDatas'
CREATE TABLE [dbo].[tblSensorDatas] (
    [SND_IDPkey] int IDENTITY(1,1) NOT NULL,
    [IMG_IDFkey] int  NOT NULL,
    [SDT_IDFkey] int  NOT NULL,
    [SDT_Value] decimal(18,2)  NOT NULL
);
GO

-- Creating table 'tblSensorDataTypes'
CREATE TABLE [dbo].[tblSensorDataTypes] (
    [SDT_IDPkey] int  NOT NULL,
    [SDT_Name] nvarchar(50)  NULL
);
GO

-- Creating table 'tblImages'
CREATE TABLE [dbo].[tblImages] (
    [IMG_IDPkey] int IDENTITY(1,1) NOT NULL,
    [IMG_Name] nvarchar(200)  NOT NULL,
    [ALB_IDFkey] int  NOT NULL,
    [FTR_IDFkey] int  NOT NULL,
    [ENC_IDFkey] int  NOT NULL,
    [IMG_Path] nvarchar(500)  NOT NULL,
    [IMG_SensorDataPath] nvarchar(500)  NULL,
    [IMG_MetaDataPath] nvarchar(500)  NULL
);
GO

-- Creating table 'tblMetaTypes'
CREATE TABLE [dbo].[tblMetaTypes] (
    [MTT_IDPkey] int  NOT NULL,
    [MTT_Name] nvarchar(50)  NOT NULL,
    [MTT_Description] nvarchar(200)  NULL
);
GO

-- Creating table 'tblMetaDatas'
CREATE TABLE [dbo].[tblMetaDatas] (
    [MTD_IDPkey] int IDENTITY(1,1) NOT NULL,
    [MTD_Value] varchar(5000)  NULL,
    [IMG_IDFkey] int  NOT NULL,
    [MTT_IDFkey] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ALB_IDPkey] in table 'tblAlbums'
ALTER TABLE [dbo].[tblAlbums]
ADD CONSTRAINT [PK_tblAlbums]
    PRIMARY KEY CLUSTERED ([ALB_IDPkey] ASC);
GO

-- Creating primary key on [CMD_IDPkey] in table 'tblColorModes'
ALTER TABLE [dbo].[tblColorModes]
ADD CONSTRAINT [PK_tblColorModes]
    PRIMARY KEY CLUSTERED ([CMD_IDPkey] ASC);
GO

-- Creating primary key on [ENC_IDPkey] in table 'tblEnvironmentalConditions'
ALTER TABLE [dbo].[tblEnvironmentalConditions]
ADD CONSTRAINT [PK_tblEnvironmentalConditions]
    PRIMARY KEY CLUSTERED ([ENC_IDPkey] ASC);
GO

-- Creating primary key on [FTR_IDPkey] in table 'tblFeatures'
ALTER TABLE [dbo].[tblFeatures]
ADD CONSTRAINT [PK_tblFeatures]
    PRIMARY KEY CLUSTERED ([FTR_IDPkey] ASC);
GO

-- Creating primary key on [FLT_IDPkey] in table 'tblFilters'
ALTER TABLE [dbo].[tblFilters]
ADD CONSTRAINT [PK_tblFilters]
    PRIMARY KEY CLUSTERED ([FLT_IDPkey] ASC);
GO

-- Creating primary key on [PIM_IDPkey] in table 'tblProcessedImages'
ALTER TABLE [dbo].[tblProcessedImages]
ADD CONSTRAINT [PK_tblProcessedImages]
    PRIMARY KEY CLUSTERED ([PIM_IDPkey] ASC);
GO

-- Creating primary key on [SND_IDPkey] in table 'tblSensorDatas'
ALTER TABLE [dbo].[tblSensorDatas]
ADD CONSTRAINT [PK_tblSensorDatas]
    PRIMARY KEY CLUSTERED ([SND_IDPkey] ASC);
GO

-- Creating primary key on [SDT_IDPkey] in table 'tblSensorDataTypes'
ALTER TABLE [dbo].[tblSensorDataTypes]
ADD CONSTRAINT [PK_tblSensorDataTypes]
    PRIMARY KEY CLUSTERED ([SDT_IDPkey] ASC);
GO

-- Creating primary key on [IMG_IDPkey] in table 'tblImages'
ALTER TABLE [dbo].[tblImages]
ADD CONSTRAINT [PK_tblImages]
    PRIMARY KEY CLUSTERED ([IMG_IDPkey] ASC);
GO

-- Creating primary key on [MTT_IDPkey] in table 'tblMetaTypes'
ALTER TABLE [dbo].[tblMetaTypes]
ADD CONSTRAINT [PK_tblMetaTypes]
    PRIMARY KEY CLUSTERED ([MTT_IDPkey] ASC);
GO

-- Creating primary key on [MTD_IDPkey] in table 'tblMetaDatas'
ALTER TABLE [dbo].[tblMetaDatas]
ADD CONSTRAINT [PK_tblMetaDatas]
    PRIMARY KEY CLUSTERED ([MTD_IDPkey] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CMD_IDFkey] in table 'tblProcessedImages'
ALTER TABLE [dbo].[tblProcessedImages]
ADD CONSTRAINT [FK_tblProcessedImages_tblColorModes]
    FOREIGN KEY ([CMD_IDFkey])
    REFERENCES [dbo].[tblColorModes]
        ([CMD_IDPkey])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblProcessedImages_tblColorModes'
CREATE INDEX [IX_FK_tblProcessedImages_tblColorModes]
ON [dbo].[tblProcessedImages]
    ([CMD_IDFkey]);
GO

-- Creating foreign key on [FLT_IDFkey] in table 'tblProcessedImages'
ALTER TABLE [dbo].[tblProcessedImages]
ADD CONSTRAINT [FK_tblProcessedImages_tblFilters]
    FOREIGN KEY ([FLT_IDFkey])
    REFERENCES [dbo].[tblFilters]
        ([FLT_IDPkey])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblProcessedImages_tblFilters'
CREATE INDEX [IX_FK_tblProcessedImages_tblFilters]
ON [dbo].[tblProcessedImages]
    ([FLT_IDFkey]);
GO

-- Creating foreign key on [SDT_IDFkey] in table 'tblSensorDatas'
ALTER TABLE [dbo].[tblSensorDatas]
ADD CONSTRAINT [FK_tblSensorData_tblSensorDataType]
    FOREIGN KEY ([SDT_IDFkey])
    REFERENCES [dbo].[tblSensorDataTypes]
        ([SDT_IDPkey])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblSensorData_tblSensorDataType'
CREATE INDEX [IX_FK_tblSensorData_tblSensorDataType]
ON [dbo].[tblSensorDatas]
    ([SDT_IDFkey]);
GO

-- Creating foreign key on [ALB_IDFkey] in table 'tblImages'
ALTER TABLE [dbo].[tblImages]
ADD CONSTRAINT [FK_tblImages_tblAlbums]
    FOREIGN KEY ([ALB_IDFkey])
    REFERENCES [dbo].[tblAlbums]
        ([ALB_IDPkey])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblImages_tblAlbums'
CREATE INDEX [IX_FK_tblImages_tblAlbums]
ON [dbo].[tblImages]
    ([ALB_IDFkey]);
GO

-- Creating foreign key on [ENC_IDFkey] in table 'tblImages'
ALTER TABLE [dbo].[tblImages]
ADD CONSTRAINT [FK_tblImages_tblEnvironmentalConditions]
    FOREIGN KEY ([ENC_IDFkey])
    REFERENCES [dbo].[tblEnvironmentalConditions]
        ([ENC_IDPkey])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblImages_tblEnvironmentalConditions'
CREATE INDEX [IX_FK_tblImages_tblEnvironmentalConditions]
ON [dbo].[tblImages]
    ([ENC_IDFkey]);
GO

-- Creating foreign key on [FTR_IDFkey] in table 'tblImages'
ALTER TABLE [dbo].[tblImages]
ADD CONSTRAINT [FK_tblImages_tblFeatures]
    FOREIGN KEY ([FTR_IDFkey])
    REFERENCES [dbo].[tblFeatures]
        ([FTR_IDPkey])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblImages_tblFeatures'
CREATE INDEX [IX_FK_tblImages_tblFeatures]
ON [dbo].[tblImages]
    ([FTR_IDFkey]);
GO

-- Creating foreign key on [IMG_IDFkey] in table 'tblProcessedImages'
ALTER TABLE [dbo].[tblProcessedImages]
ADD CONSTRAINT [FK_tblProcessedImages_tblImages]
    FOREIGN KEY ([IMG_IDFkey])
    REFERENCES [dbo].[tblImages]
        ([IMG_IDPkey])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblProcessedImages_tblImages'
CREATE INDEX [IX_FK_tblProcessedImages_tblImages]
ON [dbo].[tblProcessedImages]
    ([IMG_IDFkey]);
GO

-- Creating foreign key on [IMG_IDFkey] in table 'tblSensorDatas'
ALTER TABLE [dbo].[tblSensorDatas]
ADD CONSTRAINT [FK_tblSensorData_tblImages]
    FOREIGN KEY ([IMG_IDFkey])
    REFERENCES [dbo].[tblImages]
        ([IMG_IDPkey])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblSensorData_tblImages'
CREATE INDEX [IX_FK_tblSensorData_tblImages]
ON [dbo].[tblSensorDatas]
    ([IMG_IDFkey]);
GO

-- Creating foreign key on [IMG_IDFkey] in table 'tblMetaDatas'
ALTER TABLE [dbo].[tblMetaDatas]
ADD CONSTRAINT [FK_tblMetaData_tblImages]
    FOREIGN KEY ([IMG_IDFkey])
    REFERENCES [dbo].[tblImages]
        ([IMG_IDPkey])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblMetaData_tblImages'
CREATE INDEX [IX_FK_tblMetaData_tblImages]
ON [dbo].[tblMetaDatas]
    ([IMG_IDFkey]);
GO

-- Creating foreign key on [MTT_IDFkey] in table 'tblMetaDatas'
ALTER TABLE [dbo].[tblMetaDatas]
ADD CONSTRAINT [FK_tblMetaData_tblMetaType]
    FOREIGN KEY ([MTT_IDFkey])
    REFERENCES [dbo].[tblMetaTypes]
        ([MTT_IDPkey])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tblMetaData_tblMetaType'
CREATE INDEX [IX_FK_tblMetaData_tblMetaType]
ON [dbo].[tblMetaDatas]
    ([MTT_IDFkey]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------