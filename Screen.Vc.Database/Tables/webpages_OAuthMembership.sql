CREATE TABLE [dbo].[webpages_OAuthMembership](
	[Provider] [nvarchar](30) NOT NULL,
	[ProviderUserId] [nvarchar](100) NOT NULL,
	[UserId] [int] NOT NULL,

    PRIMARY KEY CLUSTERED 
    (
	    [Provider] ASC,
	    [ProviderUserId] ASC
    )
)