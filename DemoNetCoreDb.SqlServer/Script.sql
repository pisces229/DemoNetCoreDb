﻿
CREATE TABLE [dbo].[Person](
	[Row] [int] IDENTITY(1,1) NOT NULL,
	[Id] [char](10) NOT NULL,
	[Name] [nvarchar](10) NOT NULL,
	[Age] [int] NOT NULL,
	[Birthday] [datetime] NOT NULL,
	[Remark] [nvarchar](100) NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Row] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE UNIQUE NONCLUSTERED INDEX [IDX_Person_Id] ON [dbo].[Person]
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Address](
	[Row] [int] IDENTITY(1,1) NOT NULL,
	[Id] [char](10) NOT NULL,
	[Text] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[Row] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_Id] FOREIGN KEY([Id])
REFERENCES [dbo].[Person] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_Id]
GO


INSERT INTO [dbo].[Person]
([Id],[Name],[Age],[Birthday],[Remark])
VALUES
('A123456789','AAA',18,GETDATE(),''),
('B123456789','BBB',28,GETDATE(),'')
GO

INSERT INTO [dbo].[Address]
([Id],[Text])
VALUES
('A123456789','A1'),
('A123456789','A2'),
('B123456789','B1'),
('B123456789','B2')
GO