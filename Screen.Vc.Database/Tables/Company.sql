CREATE TABLE [dbo].[Company]
(
    [CompanyId] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [AddressId] BIGINT NOT NULL, 
    [TagLine] NVARCHAR(50) NULL, 
    [LogoUrl] NVARCHAR(1024) NULL, 
    [Pitch30Sec] NVARCHAR(1024) NULL, 
    [Pitch3Min] NVARCHAR(1024) NULL, 
    [Pitch10Min] NVARCHAR(1024) NULL, 
    [ExecutiveSummary] NVARCHAR(MAX) NULL, 
    [Faq] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_Company_Address] FOREIGN KEY ([AddressId]) REFERENCES [Address]([AddressId])
)
