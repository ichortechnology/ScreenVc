CREATE TABLE [dbo].[UserReadQuestion]
(
    [UserId] BIGINT NOT NULL, 
    [AttributeQuestionId] BIGINT NOT NULL, 

    CONSTRAINT [PK_QuestionUserFreshness] PRIMARY KEY ([UserId], [AttributeQuestionId] ), 
    CONSTRAINT [FK_QuestionUserFreshness_UserProfile] FOREIGN KEY ([UserId]) REFERENCES [UserProfile]([UserId]),
    CONSTRAINT [FK_QuestionUserFreshness_AttributeQuestion] FOREIGN KEY ([AttributeQuestionId]) REFERENCES [AttributeQuestion]([AttributeQuestionId])
)
