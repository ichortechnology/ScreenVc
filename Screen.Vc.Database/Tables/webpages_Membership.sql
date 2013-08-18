CREATE TABLE [dbo].[webpages_Membership](
	[UserId] BIGINT NOT NULL,
	[CreateDate] [datetime] NULL,
	[ConfirmationToken] [nvarchar](128) NULL,
	[IsConfirmed] [bit] NULL,
	[LastPasswordFailureDate] [datetime] NULL,
	[PasswordFailuresSinceLastSuccess] [int] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordChangedDate] [datetime] NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[PasswordVerificationToken] [nvarchar](128) NULL,
	[PasswordVerificationTokenExpirationDate] [datetime] NULL,

    PRIMARY KEY CLUSTERED 
    (
	    [UserId] ASC
    ),
    CONSTRAINT [FK_webpagesMemberShip_UserProfile_UserId] FOREIGN KEY ([UserId]) REFERENCES [UserProfile]([UserId])
)