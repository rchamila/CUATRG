﻿CREATE TABLE [dbo].[tblImages]
(
	[IMG_IDPkey] INT NOT NULL PRIMARY KEY, 
    [IMG_Name] NVARCHAR(200) NOT NULL, 
    [LOC_IDFkey] INT NOT NULL, 
    [FTR_IDFkey] INT NOT NULL, 
    [ENC_IDFkey] INT NOT NULL, 
    [IMG_Path] NVARCHAR(500) NOT NULL
)
