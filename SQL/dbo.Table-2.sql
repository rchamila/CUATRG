CREATE TABLE [dbo].[Log] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Date]      DATETIME       NOT NULL,
    [Thread]    VARCHAR (255)  NOT NULL,
    [Level]     VARCHAR (50)   NOT NULL,
    [Logger]    VARCHAR (255)  NOT NULL,
    [Message]   VARCHAR (4000) NOT NULL,
    [Exception] VARCHAR (2000) NULL
);

Go

CREATE TABLE [dbo].[tblColorModes] (
    [CMD_IDPkey] INT           NOT NULL,
    [CMD_Name]   NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([CMD_IDPkey] ASC)
);

Go

CREATE TABLE [dbo].[tblEnvironmentalConditions] (
    [ENC_IDPkey]      INT           NOT NULL,
    [ENC_Description] NVARCHAR (50) NOT NULL,
    [ENC_Name]        NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([ENC_IDPkey] ASC)
);

Go

CREATE TABLE [dbo].[tblFeatures] (
    [FTR_IDPkey] INT           NOT NULL,
    [FTR_Name]   NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([FTR_IDPkey] ASC)
);

Go

CREATE TABLE [dbo].[tblFilters] (
    [FLT_IDPkey]      INT           NOT NULL,
    [FLT_Name]        NCHAR (10)    NULL,
    [FLT_Description] NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([FLT_IDPkey] ASC)
);

Go

CREATE TABLE [dbo].[tblMetaType] (
    [MTT_IDPkey]      INT            NOT NULL,
    [MTT_Name]        NVARCHAR (50)  NOT NULL,
    [MTT_Description] NVARCHAR (200) NULL,
    [MTT_Inactive]    BIT            DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([MTT_IDPkey] ASC)
);

Go

CREATE TABLE [dbo].[tblSensorDataType] (
    [SDT_IDPkey] INT           NOT NULL,
    [SDT_Name]   NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([SDT_IDPkey] ASC)
);

Go

CREATE TABLE [dbo].[tblAlbums] (
    [ALB_IDPkey]      INT            IDENTITY (1, 1) NOT NULL,
    [ALB_Name]        NVARCHAR (50)  NOT NULL,
    [ALB_Description] NVARCHAR (500) NULL,
    PRIMARY KEY CLUSTERED ([ALB_IDPkey] ASC)
);

Go

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

Go

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

Go

CREATE TABLE [dbo].[tblMetaData] (
    [MTD_IDPkey] INT            IDENTITY (1, 1) NOT NULL,
    [MTD_Value]  VARCHAR (5000) NULL,
    [IMG_IDFkey] INT            NOT NULL,
    [MTT_IDFkey] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([MTD_IDPkey] ASC),
    CONSTRAINT [FK_tblMetaData_tblImages] FOREIGN KEY ([IMG_IDFkey]) REFERENCES [dbo].[tblImages] ([IMG_IDPkey]),
    CONSTRAINT [FK_tblMetaData_tblMetaType] FOREIGN KEY ([MTT_IDFkey]) REFERENCES [dbo].[tblMetaType] ([MTT_IDPkey])
);

Go
 
CREATE TABLE [dbo].[tblSensorData] (
    [SND_IDPkey] INT             IDENTITY (1, 1) NOT NULL,
    [IMG_IDFkey] INT             NOT NULL,
    [SDT_IDFkey] INT             NOT NULL,
    [SDT_Value]  DECIMAL (18, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([SND_IDPkey] ASC),
    CONSTRAINT [FK_tblSensorData_tblImages] FOREIGN KEY ([IMG_IDFkey]) REFERENCES [dbo].[tblImages] ([IMG_IDPkey]),
    CONSTRAINT [FK_tblSensorData_tblSensorDataType] FOREIGN KEY ([SDT_IDFkey]) REFERENCES [dbo].[tblSensorDataType] ([SDT_IDPkey])
);

GO

CREATE TABLE [dbo].[tblFile] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [FileName]   NVARCHAR (50) NOT NULL,
    [Status]     INT           NOT NULL,
    [StartStamp] DATETIME      NOT NULL,
    [EndStamp]   DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO

INSERT INTO [dbo].[tblColorModes] ([CMD_IDPkey], [CMD_Name]) VALUES (1, N'RGB')
INSERT INTO [dbo].[tblColorModes] ([CMD_IDPkey], [CMD_Name]) VALUES (2, N'Gray Scale')
INSERT INTO [dbo].[tblColorModes] ([CMD_IDPkey], [CMD_Name]) VALUES (3, N'Black and White')

Go

INSERT INTO [dbo].[tblEnvironmentalConditions] ([ENC_IDPkey], [ENC_Description], [ENC_Name]) VALUES (1, N'Sunny Day', N'Sunny')
INSERT INTO [dbo].[tblEnvironmentalConditions] ([ENC_IDPkey], [ENC_Description], [ENC_Name]) VALUES (2, N'Morning', N'Morning')
INSERT INTO [dbo].[tblEnvironmentalConditions] ([ENC_IDPkey], [ENC_Description], [ENC_Name]) VALUES (3, N'Cloudy', N'Cloudy')
INSERT INTO [dbo].[tblEnvironmentalConditions] ([ENC_IDPkey], [ENC_Description], [ENC_Name]) VALUES (4, N'Noon', N'Noon')
INSERT INTO [dbo].[tblEnvironmentalConditions] ([ENC_IDPkey], [ENC_Description], [ENC_Name]) VALUES (5, N'Evening', N'Evening')
INSERT INTO [dbo].[tblEnvironmentalConditions] ([ENC_IDPkey], [ENC_Description], [ENC_Name]) VALUES (6, N'Night', N'Night')

GO

INSERT INTO [dbo].[tblFeatures] ([FTR_IDPkey], [FTR_Name]) VALUES (1, N'Normal')
INSERT INTO [dbo].[tblFeatures] ([FTR_IDPkey], [FTR_Name]) VALUES (2, N'Blurred')
INSERT INTO [dbo].[tblFeatures] ([FTR_IDPkey], [FTR_Name]) VALUES (3, N'Shadowy')

Go

INSERT INTO [dbo].[tblFilters] ([FLT_IDPkey], [FLT_Name], [FLT_Description]) VALUES (1, N'Sobel     ', N'Sobel')
INSERT INTO [dbo].[tblFilters] ([FLT_IDPkey], [FLT_Name], [FLT_Description]) VALUES (2, N'Prewitt   ', N'Prewitt')
INSERT INTO [dbo].[tblFilters] ([FLT_IDPkey], [FLT_Name], [FLT_Description]) VALUES (3, N'Canny     ', N'Canny ')
INSERT INTO [dbo].[tblFilters] ([FLT_IDPkey], [FLT_Name], [FLT_Description]) VALUES (4, N'Canny0.2  ', N'Canny 0.2')
INSERT INTO [dbo].[tblFilters] ([FLT_IDPkey], [FLT_Name], [FLT_Description]) VALUES (5, N'Canny0.25 ', N'Canny 0.25')
INSERT INTO [dbo].[tblFilters] ([FLT_IDPkey], [FLT_Name], [FLT_Description]) VALUES (6, N'Canny0.3  ', N'Canny 0.3')
INSERT INTO [dbo].[tblFilters] ([FLT_IDPkey], [FLT_Name], [FLT_Description]) VALUES (7, N'Grayscale ', N'Grayscale')
INSERT INTO [dbo].[tblFilters] ([FLT_IDPkey], [FLT_Name], [FLT_Description]) VALUES (8, N'Roberts   ', N'Roberts')

GO

INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (0, N'GPSVersionID', N'GPS tag version', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (1, N'GPSLatitudeRef', N'North or South Latitude', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (2, N'GPSLatitude', N'Latitude', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (3, N'GPSLongitudeRef', N'East or West Longitude', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (4, N'GPSLongitude', N'Longitude', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (5, N'GPSAltitudeRef', N'Altitude reference', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (6, N'GPSAltitude', N'Altitude', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (7, N'GPSTimeStamp', N'GPS time (atomic clock)', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (8, N'GPSSatellites', N'GPS satellites used for measurement', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (9, N'GPSStatus', N'GPS receiver status', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (10, N'GPSMeasureMode', N'GPS measurement mode', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (11, N'GPSDOP', N'Measurement precision', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (12, N'GPSSpeedRef', N'Speed unit', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (13, N'GPSSpeed', N'Speed of GPS receiver', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (14, N'GPSTrackRef', N'Reference for direction of movement', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (15, N'GPSTrack', N'Direction of movement', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (16, N'GPSImgDirectionRef', N'Reference for direction of image', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (17, N'GPSImgDirection', N'Direction of image', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (18, N'GPSMapDatum', N'Geodetic survey data used', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (19, N'GPSDestLatitudeRef', N'Reference for latitude of destination', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (20, N'GPSDestLatitude', N'Latitude of destination', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (21, N'GPSDestLongitudeRef', N'Reference for longitude of destination', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (22, N'GPSDestLongitude', N'Longitude of destination', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (23, N'GPSDestBearingRef', N'Reference for bearing of destination', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (24, N'GPSDestBearing', N'Bearing of destination', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (25, N'GPSDestDistanceRef', N'Reference for distance to destination', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (26, N'GPSDestDistance', N'Distance to destination', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (27, N'GPSProcessingMethod', N'Name of GPS processing method', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (28, N'GPSAreaInformation', N'Name of GPS area', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (29, N'GPSDateStamp', N'GPS date', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (30, N'GPSDifferential', N'GPS differential correction', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (256, N'ImageWidth', N'Image width', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (257, N'ImageHeight', N'Image height', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (258, N'BitsPerSample', N'Number of bits per component', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (259, N'Compression', N'Compression scheme', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (262, N'PhotometricInterpretation', N'Pixel composition', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (270, N'ImageDescription', N'Image title', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (271, N'Make', N'Image input equipment manufacturer', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (272, N'Model', N'Image input equipment model', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (273, N'StripOffsets', N'Image data location', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (274, N'Orientation', N'Orientation of image', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (277, N'SamplesPerPixel', N'Number of components', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (278, N'RowsPerStrip', N'Number of rows per strip', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (279, N'StripByteCounts', N'Bytes per compressed strip', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (282, N'XResolution', N'Image resolution in width direction', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (283, N'YResolution', N'Image resolution in height direction', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (284, N'PlanarConfiguration', N'Image data arrangement', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (296, N'ResolutionUnit', N'Unit of X and Y resolution', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (301, N'TransferFunction', N'Transfer function', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (305, N'Software', N'Software used', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (306, N'DateTime', N'File change date and time', 1)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (315, N'Artist', N'Person who created the image', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (318, N'WhitePoint', N'White point chromaticity', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (319, N'PrimaryChromaticities', N'Chromaticities of primaries', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (513, N'JPEGInterchangeFormat', N'Offset to JPEG SOI', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (514, N'JPEGInterchangeFormatLength', N'Bytes of JPEG data', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (529, N'YCbCrCoefficients', N'Color space transformation matrix coefficients', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (530, N'YCbCrSubSampling', N'Subsampling ratio of Y to C', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (531, N'YCbCrPositioning', N'Y and C positioning', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (532, N'ReferenceBlackWhite', N'Pair of black and white reference values', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (33432, N'Copyright', N'Copyright holder', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (33434, N'ExposureTime', N'Exposure time', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (33437, N'FNumber', N'F number', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (34850, N'ExposureProgram', N'Exposure program', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (34852, N'SpectralSensitivity', N'Spectral sensitivity', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (34855, N'ISOSpeedRatings', N'ISO speed rating', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (34856, N'OECF', N'Optoelectric conversion factor', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (36864, N'ExifVersion', N'Exif version', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (36867, N'DateTimeOriginal', N'Date and time of original data generation', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (36868, N'DateTimeDigitized', N'Date and time of digital data generation', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (37121, N'ComponentsConfiguration', N'Meaning of each component', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (37122, N'CompressedBitsPerPixel', N'Image compression mode', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (37377, N'ShutterSpeedValue', N'Shutter speed', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (37378, N'ApertureValue', N'Aperture', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (37379, N'BrightnessValue', N'Brightness', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (37380, N'ExposureBiasValue', N'Exposure bias', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (37381, N'MaxApertureValue', N'Maximum lens aperture', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (37382, N'SubjectDistance', N'Subject distance', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (37383, N'MeteringMode', N'Metering mode', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (37384, N'LightSource', N'Light source', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (37385, N'Flash', N'Flash', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (37386, N'FocalLength', N'Lens focal length', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (37396, N'SubjectArea', N'Subject area', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (37500, N'MakerNote', N'Manufacturer notes', 1)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (37510, N'UserComment', N'User comments', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (37520, N'SubSecTime', N'DateTime subseconds', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (37521, N'SubSecTimeOriginal', N'DateTimeOriginal subseconds', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (37522, N'SubSecTimeDigitized', N'DateTimeDigitized subseconds', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (40960, N'FlashpixVersion', N'Supported Flashpix version', 1)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (40961, N'ColorSpace', N'Color space information', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (40962, N'PixelXDimension', N'Valid image width', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (40963, N'PixelYDimension', N'Valid image height', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (40964, N'RelatedSoundFile', N'Related audio file', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41483, N'FlashEnergy', N'Flash energy', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41484, N'SpatialFrequencyResponse', N'Spatial frequency response', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41486, N'FocalPlaneXResolution', N'Focal plane X resolution', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41487, N'FocalPlaneYResolution', N'Focal plane Y resolution', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41488, N'FocalPlaneResolutionUnit', N'Focal plane resolution unit', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41492, N'SubjectLocation', N'Subject location', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41493, N'ExposureIndex', N'Exposure index', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41495, N'SensingMethod', N'Sensing method', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41728, N'FileSource', N'File source', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41729, N'SceneType', N'Scene type', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41730, N'CFAPattern', N'CFA pattern', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41985, N'CustomRendered', N'Custom image processing', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41986, N'ExposureMode', N'Exposure mode', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41987, N'WhiteBalance', N'White balance', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41988, N'DigitalZoomRatio', N'Digital zoom ratio', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41989, N'FocalLengthIn35mmFilm', N'Focal length in 35 mm film', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41990, N'SceneCaptureType', N'Scene capture type', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41991, N'GainControl', N'Gain control', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41992, N'Contrast', N'Contrast', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41993, N'Saturation', N'Saturation', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41994, N'Sharpness', N'Sharpness', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41995, N'DeviceSettingDescription', N'Device settings description', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (41996, N'SubjectDistanceRange', N'Subject distance range', 0)
INSERT INTO [dbo].[tblMetaType] ([MTT_IDPkey], [MTT_Name], [MTT_Description], [MTT_Inactive]) VALUES (42016, N'ImageUniqueID', N'Unique image ID', 0)

