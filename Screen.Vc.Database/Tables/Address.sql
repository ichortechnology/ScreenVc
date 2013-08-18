CREATE TABLE [dbo].[Address]
(
    [AddressId] BIGINT NOT NULL PRIMARY KEY, 
    [Block] NVARCHAR(10) NOT NULL, 
    [Street] NVARCHAR(50) NOT NULL, 
    [AptNumber] NVARCHAR(20) NULL, 
    [City] NVARCHAR(50) NOT NULL, 
    [State] NVARCHAR(50) NOT NULL, 
    [Country] NVARCHAR(50) NOT NULL DEFAULT 'US'
)
