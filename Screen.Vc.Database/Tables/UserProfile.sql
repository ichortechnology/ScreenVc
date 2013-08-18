CREATE TABLE [dbo].[UserProfile](
	[UserId] BIGINT IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
    [FirstName] [NVARCHAR](50) NULL, 
    [LastName] [NVARCHAR](50) NULL, 
    [EmailAddress] [nvarchar](100) NOT NULL,

    [Enabled] BIT NOT NULL DEFAULT 1, 
    PRIMARY KEY CLUSTERED 
    (
	    [UserId] ASC
    )
    
)
GO

CREATE INDEX [IX_UserProfile_EmailAddress] ON [dbo].[UserProfile] ([EmailAddress])

GO

CREATE INDEX [IX_UserProfile_UserName] ON [dbo].[UserProfile] ([UserName])

GO

CREATE INDEX [IX_UserProfile_FullName] ON [dbo].[UserProfile] ([LastName], [FirstName])
