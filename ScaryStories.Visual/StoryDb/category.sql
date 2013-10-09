CREATE TABLE [CategoryDetail] ([Id] uniqueidentifier  NOT NULL  
, [Name] varchar(4000) NULL  
, [Image] varbinary  NULL  
);
ALTER TABLE [CategoryDetail] ADD CONSTRAINT [PK_CategoryDetail] PRIMARY KEY ([Id]);

