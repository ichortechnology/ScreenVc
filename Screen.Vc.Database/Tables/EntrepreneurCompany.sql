CREATE TABLE [dbo].[EntrepreneurCompany]
(
    [EntrepreneurId] BIGINT NOT NULL, 
    [CompanyId] BIGINT NOT NULL, 
    CONSTRAINT [PK_EntrepreneurCompany] PRIMARY KEY CLUSTERED ([EntrepreneurId], [CompanyId]) 
    
)
