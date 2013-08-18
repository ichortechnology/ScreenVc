CREATE TABLE [dbo].[webpages_UsersInRoles](
	[UserId] BIGINT NOT NULL,
	[RoleId] [int] NOT NULL,

    PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [fk_RoleId] FOREIGN KEY([RoleId]) REFERENCES [dbo].[webpages_Roles] ([RoleId]),
    CONSTRAINT [fk_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[UserProfile] ([UserId])

)
