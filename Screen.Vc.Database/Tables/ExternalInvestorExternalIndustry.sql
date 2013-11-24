CREATE TABLE [dbo].[ExternalInvestorExternalIndustry]
(
    [ExternalInvestorId] INT NOT NULL,
    [ExternalIndustryId] INT NOT NULL, 
    CONSTRAINT [FK_ExternalInvestorExternalIndustry_ExternalInvestor] FOREIGN KEY ([ExternalInvestorId]) REFERENCES [ExternalInvestor]([ExternalInvestorId]), 
    CONSTRAINT [FK_ExternalInvestorExternalIndustry_ExternalIndustry] FOREIGN KEY ([ExternalIndustryId]) REFERENCES [ExternalIndustry]([ExternalIndustryId])


)
