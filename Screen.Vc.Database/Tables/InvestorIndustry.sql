CREATE TABLE [dbo].[InvestorIndustry]
(
    [InvestorUserId] BIGINT NOT NULL, 
    [IndustryId] INT NOT NULL, 
    CONSTRAINT [PK_InvestorIndustry] PRIMARY KEY ([InvestorUserId], [IndustryId]), 
    CONSTRAINT [FK_InvestorIndustry_UserProfile] FOREIGN KEY ([InvestorUserId]) REFERENCES [UserProfile]([UserId]), 
    CONSTRAINT [FK_InvestorIndustry_Indistry] FOREIGN KEY ([IndustryId]) REFERENCES [Industry]([IndustryId])
)
