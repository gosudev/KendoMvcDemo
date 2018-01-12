/* Drop existing structure */
IF EXISTS (select 1 from sys.views where name='ComplaintViewSearch' and type='v')
DROP VIEW ComplaintViewSearch;

DROP TABLE IF EXISTS [dbo].[Complaint] 
DROP TABLE IF EXISTS [dbo].[Product] 

GO 
CREATE TABLE [dbo].[Product] (
    [ProductId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.Product] PRIMARY KEY CLUSTERED ([ProductId] ASC)
);

GO 
CREATE TABLE [dbo].[Complaint] (
    [ComplaintId] INT            IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (MAX) NULL,
    [WhatHappend] NVARCHAR (MAX) NULL,
    [Company]     NVARCHAR (MAX) NULL,
    [SentDate]    DATETIME       NOT NULL,
    [ProductId]   INT            NOT NULL,
    CONSTRAINT [PK_dbo.Complaint] PRIMARY KEY CLUSTERED ([ComplaintId] ASC),
    CONSTRAINT [FK_dbo.Complaint_dbo.Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([ProductId]) ON DELETE CASCADE
);

CREATE NONCLUSTERED INDEX [IX_ProductId]
    ON [dbo].[Complaint]([ProductId] ASC);
	
/* Populate data */

INSERT INTO [dbo].[Product] ([Name]) VALUES ('DELL LATITUDE E6530 I5 16 GB RAM 320 GB HDD WIN7PRO')
INSERT INTO [dbo].[Product] ([Name]) VALUES ('DELL LATITUDE E6530 I5 8 GB RAM 160 GB SSD WIN7PRO')
INSERT INTO [dbo].[Product] ([Name]) VALUES ('DELL LATITUDE E5430 I5 4 GB RAM 1000 GB HDD WIN7')
INSERT INTO [dbo].[Product] ([Name]) VALUES ('DELL LATITUDE E5430 I5 4 GB RAM 240 GB SSD WIN7PRO')
GO

INSERT INTO [dbo].[Complaint] ([Title],[WhatHappend],[Company],[SentDate],[ProductId])  VALUES ('Complaint 1','Missing software','Apple' ,getdate() ,1)
INSERT INTO [dbo].[Complaint] ([Title],[WhatHappend],[Company],[SentDate],[ProductId])  VALUES ('Complaint 1','Missing software','Apple' ,getdate() ,2)
INSERT INTO [dbo].[Complaint] ([Title],[WhatHappend],[Company],[SentDate],[ProductId])  VALUES ('Complaint 1','Missing software','Apple' ,getdate() ,3)
INSERT INTO [dbo].[Complaint] ([Title],[WhatHappend],[Company],[SentDate],[ProductId])  VALUES ('Complaint 1','Missing software','Apple' ,getdate() ,4)

INSERT INTO [dbo].[Complaint] ([Title],[WhatHappend],[Company],[SentDate],[ProductId])  VALUES ('Complaint 2','Missing hardware','IBM' ,getdate() ,1)
INSERT INTO [dbo].[Complaint] ([Title],[WhatHappend],[Company],[SentDate],[ProductId])  VALUES ('Complaint 2','Missing hardware','IBM' ,getdate() ,2)
INSERT INTO [dbo].[Complaint] ([Title],[WhatHappend],[Company],[SentDate],[ProductId])  VALUES ('Complaint 2','Missing hardware','IBM' ,getdate() ,3)
INSERT INTO [dbo].[Complaint] ([Title],[WhatHappend],[Company],[SentDate],[ProductId])  VALUES ('Complaint 2','Missing hardware','IBM' ,getdate() ,4)

/* Show data */
SELECT * FROM [dbo].[Product]
SELECT * FROM [dbo].[Complaint]
  
/* Delete existing a full text catalog */
IF EXISTS (SELECT * FROM sys.fulltext_catalogs WHERE name = N'FT_COMPLAINT_CATALOG') 
DROP FULLTEXT CATALOG FT_COMPLAINT_CATALOG 
GO 
  
/* Create a full text catalog */
CREATE FULLTEXT CATALOG FT_COMPLAINT_CATALOG AS DEFAULT; 
GO 

/* Create view */
CREATE VIEW ComplaintViewSearch WITH SCHEMABINDING AS
SELECT c.[ComplaintId] 
      ,c.[Title]
      ,c.[WhatHappend]
      ,c.[Company]
      ,c.[SentDate]
      ,c.[ProductId]
	  ,p.[Name] as 'ProductName'
	  ,c.[Company] + ' '+ p.[Name] as 'SummarySearchColumn'
  FROM [dbo].[Complaint] c
  join [dbo].[Product] p on c.ProductId = p.ProductId

GO
CREATE UNIQUE CLUSTERED INDEX IX_ComplaintViewSearch ON ComplaintViewSearch ([ComplaintId])

/* Delete existing a full text index */
IF EXISTS (SELECT * FROM sys.fulltext_indexes WHERE object_id = object_id('[dbo].[ComplaintViewSearch]') ) 
DROP FULLTEXT INDEX ON [dbo].[ComplaintViewSearch] 
GO 
  
/* Create a full text index */
CREATE FULLTEXT INDEX ON [dbo].[ComplaintViewSearch] ([SummarySearchColumn]) KEY INDEX IX_ComplaintViewSearch ON FT_COMPLAINT_CATALOG; 
GO 
ALTER FULLTEXT INDEX ON [dbo].[ComplaintViewSearch] ENABLE; 
GO
ALTER FULLTEXT INDEX ON [dbo].[ComplaintViewSearch] START FULL POPULATION; 
GO 

/* Check if search works */
SELECT * from [ComplaintViewSearch]
WHERE
FREETEXT([SummarySearchColumn],  'E5430') 

--SELECT * FROM sys.indexes i JOIN sys.objects o ON i.object_id = o.object_id WHERE is_ms_shipped = 0;   
--SELECT * FROM sys.fulltext_catalogs;   
--SELECT * FROM sys.fulltext_indexes; 

GO



