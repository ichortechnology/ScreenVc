CREATE TABLE [dbo].[CompanyAttribute]
(
    [Id]            BIGINT NOT NULL PRIMARY KEY IDENTITY,
    [AttributeId]   INT NOT NULL, 
    [Value]         NVARCHAR(MAX) NOT NULL, 
    [CompanyId]     BIGINT NOT NULL, 

    CONSTRAINT [FK_CompanyAttribute_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([CompanyId]), 
    CONSTRAINT [FK_CompanyAttribute_Attribute] FOREIGN KEY ([AttributeId]) REFERENCES [Attribute]([AttributeId])
)
