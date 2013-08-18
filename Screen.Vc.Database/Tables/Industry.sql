CREATE TABLE [dbo].[Industry]
(
    [IndustryId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 


    CONSTRAINT [AK_Industry_Name] UNIQUE ([Name]),
)
