CREATE TABLE [dbo].[webpages_Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,

    PRIMARY KEY CLUSTERED 
    (
	    [RoleId] ASC
    ),

    UNIQUE NONCLUSTERED 
    (
	    [RoleName] ASC
    )
)