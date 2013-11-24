CREATE TABLE [dbo].[ExternalInvestorSource]
(
    [ExternalInvestorSourceId] INT NOT NULL PRIMARY KEY, 
    [SourceName] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(256) NULL, 
    [Uri] NVARCHAR(1024) NULL
)
