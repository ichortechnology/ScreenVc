CREATE TABLE [dbo].[AttributeQuestion]
(
    [Id] BIGINT IDENTITY NOT NULL, 
    [Question] NVARCHAR(MAX) NOT NULL, 
    [Response] NVARCHAR(MAX) NULL, 
    [CompanyAttributeId] BIGINT NOT NULL, 
    [CreateDate] DATETIME NOT NULL DEFAULT getutcdate(), 
    [UpdateDate] DATETIME NOT NULL DEFAULT getutcdate(), 
    [CreatedBy] NVARCHAR(50) NOT NULL, 

    CONSTRAINT [PK_AttributeQuestion_Id] PRIMARY KEY (Id),
    CONSTRAINT [FK_AttributeQuestion_CompanyAttribute] FOREIGN KEY ([CompanyAttributeId]) REFERENCES [CompanyAttribute]([Id])
)
