CREATE TABLE [dbo].[CompanyIndustry]
(
    [CompanyId] BIGINT NOT NULL , 
    [IndustryId] INT NOT NULL, 

    CONSTRAINT [PK_CompanyIndustry] PRIMARY KEY ([CompanyId], [IndustryId]), 
    CONSTRAINT [FK_CompanyIndustry_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([CompanyId]), 
    CONSTRAINT [FK_CompanyIndustry_Industry] FOREIGN KEY ([IndustryId]) REFERENCES [Industry]([IndustryId])
)
