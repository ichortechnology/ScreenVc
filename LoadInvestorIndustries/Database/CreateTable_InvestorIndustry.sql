
GO

/****** Object:  Table [dbo].[InvestorIndustry]    Script Date: 8/20/2013 10:35:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[InvestorIndustry](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvestorId] [int] NOT NULL,
	[IndustryId] [int] NOT NULL,
 CONSTRAINT [PK_InvestorIndustry] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[InvestorIndustry]  WITH CHECK ADD  CONSTRAINT [FK_InvestorIndustry_Industry] FOREIGN KEY([IndustryId])
REFERENCES [dbo].[Industry] ([Id])
GO

ALTER TABLE [dbo].[InvestorIndustry] CHECK CONSTRAINT [FK_InvestorIndustry_Industry]
GO

ALTER TABLE [dbo].[InvestorIndustry]  WITH CHECK ADD  CONSTRAINT [FK_InvestorIndustry_Investor] FOREIGN KEY([InvestorId])
REFERENCES [dbo].[Investor] ([Id])
GO

ALTER TABLE [dbo].[InvestorIndustry] CHECK CONSTRAINT [FK_InvestorIndustry_Investor]
GO

