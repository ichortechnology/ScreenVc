CREATE TABLE [dbo].[AttributeComment]
(
    [CommentId] BIGINT IDENTITY NOT NULL, 
    [Comment] NVARCHAR(MAX) NOT NULL, 
    [UserId] BIGINT NOT NULL, 
    [CompanyAttributeId] BIGINT NOT NULL, 
    [VisibilityId] SMALLINT NOT NULL , 
    CONSTRAINT [FK_Comment_UserProfile] FOREIGN KEY ([UserId]) REFERENCES [UserProfile]([UserId]), 
    CONSTRAINT [PK_Comment] PRIMARY KEY ([CommentId]), 
    CONSTRAINT [FK_Comment_Company] FOREIGN KEY ([CompanyAttributeId]) REFERENCES [CompanyAttribute]([CompanyAttributeId]), 
    CONSTRAINT [FK_Comment_Visibility] FOREIGN KEY ([VisibilityId]) REFERENCES [Visibility]([VisibilityId])
)
