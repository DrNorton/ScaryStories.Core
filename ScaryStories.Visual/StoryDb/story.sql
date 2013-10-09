CREATE TABLE [StoryDetail] ([_categoryId] uniqueidentifier  NOT NULL  
, [Id] uniqueidentifier  NOT NULL  
, [Header] varchar(4000) NULL  
, [Image] varbinary  NULL  
, [Likes] bigint  NOT NULL  
, [IsFavorite] bit  NOT NULL  
, [Text] varchar(4000) NULL  
);
ALTER TABLE [StoryDetail] ADD CONSTRAINT [PK_StoryDetail] PRIMARY KEY ([Id]);
ALTER TABLE [StoryDetail] ADD CONSTRAINT [FK_StoryDetail_Category] FOREIGN KEY ([_categoryId]) REFERENCES [CategoryDetail] ([Id]);

