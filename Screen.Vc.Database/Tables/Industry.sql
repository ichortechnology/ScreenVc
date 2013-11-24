CREATE TABLE [dbo].[Industry]
(
    [IndustryId]    INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name]          NVARCHAR(255) NOT NULL, 
	[DisplayName]   NVARCHAR(255) NULL,
	[Updated]       DATETIME2(7) NULL

    CONSTRAINT [AK_Industry_Name] UNIQUE ([Name]),
)
