CREATE TABLE [dbo].[BlobContainer]
(
    [BlobContainerId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(128) NOT NULL, 


    CONSTRAINT [UQ_BlobContainer_Name] UNIQUE(Name),

)
