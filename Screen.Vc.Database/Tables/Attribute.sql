CREATE TABLE [dbo].[Attribute]
(
    [AttributeId] INT NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(100) NOT NULL, 

    CONSTRAINT [AK_Attribute_Name] UNIQUE ([Name])

)
