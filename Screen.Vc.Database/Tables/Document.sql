CREATE TABLE [dbo].[Document]
(
    [DocumentId]        BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Name]              NVARCHAR(1024) NOT NULL, 
    [BlobContainerId]   INT NOT NULL, 

    CONSTRAINT [FK_Document_BlobContainer] FOREIGN KEY ([BlobContainerId]) REFERENCES [BlobContainer]([BlobContainerId]), 
)
