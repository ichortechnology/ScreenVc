CREATE TABLE [dbo].[ExternalIndustry]
(
    [ExternalIndustryId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ExternalId] INT NOT NULL, 
    [Name] NVARCHAR(255) NOT NULL, 
	[DisplayName] NVARCHAR(255) NULL,
	[Updated] DATETIME2(7) NULL



    CONSTRAINT [AK_ExternalIndustry_Name] UNIQUE ([Name]),
)
