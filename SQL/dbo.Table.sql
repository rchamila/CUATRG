CREATE TABLE [dbo].[Log] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Date]      DATETIME       NOT NULL,
    [Thread]    VARCHAR (255)  NOT NULL,
    [Level]     VARCHAR (50)   NOT NULL,
    [Logger]    VARCHAR (255)  NOT NULL,
    [Message]   VARCHAR (4000) NOT NULL,
    [Exception] VARCHAR (2000) NULL
);

CREATE TABLE [dbo].[tblAlbums] (
    [ALB_IDPkey]      INT            IDENTITY (1, 1) NOT NULL,
    [ALB_Name]        NVARCHAR (50)  NOT NULL,
    [ALB_Description] NVARCHAR (500) NULL,
    PRIMARY KEY CLUSTERED ([ALB_IDPkey] ASC)
);

CREATE TABLE [dbo].[tblColorModes] (
    [CMD_IDPkey] INT           NOT NULL,
    [CMD_Name]   NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([CMD_IDPkey] ASC)
);

CREATE TABLE [dbo].[tblEnvironmentalConditions] (
    [ENC_IDPkey]      INT           NOT NULL,
    [ENC_Description] NVARCHAR (50) NOT NULL,
    [ENC_Name]        NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([ENC_IDPkey] ASC)
);

CREATE TABLE [dbo].[tblFeatures] (
    [FTR_IDPkey] INT           NOT NULL,
    [FTR_Name]   NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([FTR_IDPkey] ASC)
);

CREATE TABLE [dbo].[tblFilters] (
    [FLT_IDPkey] INT        NOT NULL,
    [FLT_Name]   NCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([FLT_IDPkey] ASC)
);

CREATE TABLE [dbo].[tblImages] (
    [IMG_IDPkey]         INT            IDENTITY (1, 1) NOT NULL,
    [IMG_Name]           NVARCHAR (200) NOT NULL,
    [ALB_IDFkey]         INT            NOT NULL,
    [FTR_IDFkey]         INT            NOT NULL,
    [ENC_IDFkey]         INT            NOT NULL,
    [IMG_Path]           NVARCHAR (500) NOT NULL,
    [IMG_SensorDataPath] NVARCHAR (500) NULL,
    [IMG_MetaDataPath]   NVARCHAR (500) NULL,
    PRIMARY KEY CLUSTERED ([IMG_IDPkey] ASC),
    CONSTRAINT [FK_tblImages_tblAlbums] FOREIGN KEY ([ALB_IDFkey]) REFERENCES [dbo].[tblAlbums] ([ALB_IDPkey]),
    CONSTRAINT [FK_tblImages_tblEnvironmentalConditions] FOREIGN KEY ([ENC_IDFkey]) REFERENCES [dbo].[tblEnvironmentalConditions] ([ENC_IDPkey]),
    CONSTRAINT [FK_tblImages_tblFeatures] FOREIGN KEY ([FTR_IDFkey]) REFERENCES [dbo].[tblFeatures] ([FTR_IDPkey])
);

CREATE TABLE [dbo].[tblMetaData] (
    [MTD_IDPkey] INT            IDENTITY (1, 1) NOT NULL,
    [MTD_Value]  VARCHAR (5000) NULL,
    [IMG_IDFkey] INT            NOT NULL,
    [MTT_IDFkey] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([MTD_IDPkey] ASC),
    CONSTRAINT [FK_tblMetaData_tblImages] FOREIGN KEY ([IMG_IDFkey]) REFERENCES [dbo].[tblImages] ([IMG_IDPkey]),
    CONSTRAINT [FK_tblMetaData_tblMetaType] FOREIGN KEY ([MTT_IDFkey]) REFERENCES [dbo].[tblMetaType] ([MTT_IDPkey])
);

CREATE TABLE [dbo].[tblMetaType] (
    [MTT_IDPkey]      INT            NOT NULL,
    [MTT_Name]        NVARCHAR (50)  NOT NULL,
    [MTT_Description] NVARCHAR (200) NULL,
    PRIMARY KEY CLUSTERED ([MTT_IDPkey] ASC)
);

CREATE TABLE [dbo].[tblProcessedImages] (
    [PIM_IDPkey] INT            IDENTITY (1, 1) NOT NULL,
    [PIM_Name]   NVARCHAR (50)  NOT NULL,
    [FLT_IDFkey] INT            NOT NULL,
    [CMD_IDFkey] INT            NOT NULL,
    [IMG_IDFkey] INT            NOT NULL,
    [PIM_Path]   NVARCHAR (500) NOT NULL,
    PRIMARY KEY CLUSTERED ([PIM_IDPkey] ASC),
    CONSTRAINT [FK_tblProcessedImages_tblImages] FOREIGN KEY ([IMG_IDFkey]) REFERENCES [dbo].[tblImages] ([IMG_IDPkey]),
    CONSTRAINT [FK_tblProcessedImages_tblColorModes] FOREIGN KEY ([CMD_IDFkey]) REFERENCES [dbo].[tblColorModes] ([CMD_IDPkey]),
    CONSTRAINT [FK_tblProcessedImages_tblFilters] FOREIGN KEY ([FLT_IDFkey]) REFERENCES [dbo].[tblFilters] ([FLT_IDPkey])
);

CREATE TABLE [dbo].[tblSensorData] (
    [SND_IDPkey] INT             IDENTITY (1, 1) NOT NULL,
    [IMG_IDFkey] INT             NOT NULL,
    [SDT_IDFkey] INT             NOT NULL,
    [SDT_Value]  DECIMAL (18, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([SND_IDPkey] ASC),
    CONSTRAINT [FK_tblSensorData_tblImages] FOREIGN KEY ([IMG_IDFkey]) REFERENCES [dbo].[tblImages] ([IMG_IDPkey]),
    CONSTRAINT [FK_tblSensorData_tblSensorDataType] FOREIGN KEY ([SDT_IDFkey]) REFERENCES [dbo].[tblSensorDataType] ([SDT_IDPkey])
);

CREATE TABLE [dbo].[tblSensorDataType] (
    [SDT_IDPkey] INT           NOT NULL,
    [SDT_Name]   NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([SDT_IDPkey] ASC)
);

