USE [C:\USERS\HARI GABOR\SOURCE\REPOS\ARTIFINDER\ARTIFINDER\ARTIFINDERDB.MDF]
GO
/****** Object:  Table [dbo].[AppUsers]    Script Date: 2020. 04. 02. 14:31:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUsers](
	[UserID] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[Email] [varchar](120) NOT NULL,
	[Facebook] [varchar](50) NULL,
	[StatusFK] [int] NOT NULL,
	[Admin] [bit] NOT NULL,
	[Suspended] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Artifact]    Script Date: 2020. 04. 02. 14:31:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Artifact](
	[ArtifactSK] [int] IDENTITY(1,1) NOT NULL,
	[ArtifactName] [nvarchar](120) NOT NULL,
	[ShortDescrition] [nvarchar](50) NULL,
	[PeriodBegin] [int] NOT NULL,
	[PeriodEnd] [int] NOT NULL,
	[CountryFK] [int] NOT NULL,
	[PlaceOfOrigin] [nvarchar](120) NULL,
	[Link] [varchar](120) NOT NULL,
	[MuseumFK] [int] NULL,
	[Approved] [bit] NOT NULL,
 CONSTRAINT [PK_Artifact] PRIMARY KEY CLUSTERED 
(
	[ArtifactSK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 2020. 04. 02. 14:31:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[CountrySK] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[CountrySK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Intermediates]    Script Date: 2020. 04. 02. 14:31:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Intermediates](
	[UserFK] [nvarchar](50) NOT NULL,
	[ArtifactFK] [int] NOT NULL,
	[IsUploader] [bit] NOT NULL,
 CONSTRAINT [PK_Intermediate] PRIMARY KEY CLUSTERED 
(
	[UserFK] ASC,
	[ArtifactFK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Museum]    Script Date: 2020. 04. 02. 14:31:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Museum](
	[MuseumSK] [int] IDENTITY(1,1) NOT NULL,
	[MuseumName] [nvarchar](120) NOT NULL,
	[Website] [varchar](120) NOT NULL,
	[CountryFK] [int] NOT NULL,
	[City] [nvarchar](120) NOT NULL,
	[Address] [nvarchar](120) NOT NULL,
	[Approved] [bit] NOT NULL,
 CONSTRAINT [PK_Museum] PRIMARY KEY CLUSTERED 
(
	[MuseumSK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 2020. 04. 02. 14:31:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[StatusSK] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[StatusSK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[AppUsers] ([UserID], [Password], [Email], [Facebook], [StatusFK], [Admin], [Suspended]) VALUES (N'Kovács Béla', N'a3889bca20836fbf715cf7fd62c9eaa39d93aaf8dbf52063f0cc14c968848575d8ea799654ce9ec126fb4c6829ed8ce9dcb4f7b60fabb5fed9916617d438a000', N'kovacs.bela@gmail.com', N'facebook.com/bela.kovacs', 1, 1, 0)
INSERT [dbo].[AppUsers] ([UserID], [Password], [Email], [Facebook], [StatusFK], [Admin], [Suspended]) VALUES (N'Régész János', N'a3889bca20836fbf715cf7fd62c9eaa39d93aaf8dbf52063f0cc14c968848575d8ea799654ce9ec126fb4c6829ed8ce9dcb4f7b60fabb5fed9916617d438a000', N'janos.regesz@gmail.com', NULL, 2, 0, 0)
INSERT [dbo].[AppUsers] ([UserID], [Password], [Email], [Facebook], [StatusFK], [Admin], [Suspended]) VALUES (N'Történész Edit', N'a3889bca20836fbf715cf7fd62c9eaa39d93aaf8dbf52063f0cc14c968848575d8ea799654ce9ec126fb4c6829ed8ce9dcb4f7b60fabb5fed9916617d438a000', N'edit@gmail.com', NULL, 3, 0, 0)
SET IDENTITY_INSERT [dbo].[Artifact] ON 

INSERT [dbo].[Artifact] ([ArtifactSK], [ArtifactName], [ShortDescrition], [PeriodBegin], [PeriodEnd], [CountryFK], [PlaceOfOrigin], [Link], [MuseumFK], [Approved]) VALUES (1, N'Sutton Hoo helmet', N'Anglo-saxon helmet', 625, 625, 1, N'Sutton Hoo, Suffolk', N'https://en.wikipedia.org/wiki/Sutton_Hoo_helmet', 1, 1)
INSERT [dbo].[Artifact] ([ArtifactSK], [ArtifactName], [ShortDescrition], [PeriodBegin], [PeriodEnd], [CountryFK], [PlaceOfOrigin], [Link], [MuseumFK], [Approved]) VALUES (2, N'Voynich manuscript', N'Encrypted manuscript', 1404, 1438, 2, NULL, N'https://en.wikipedia.org/wiki/Voynich_manuscript', 9, 1)
INSERT [dbo].[Artifact] ([ArtifactSK], [ArtifactName], [ShortDescrition], [PeriodBegin], [PeriodEnd], [CountryFK], [PlaceOfOrigin], [Link], [MuseumFK], [Approved]) VALUES (3, N'Rosetta Stone', N'Ancient stone', -196, -196, 9, N'Memphis', N'https://en.wikipedia.org/wiki/Rosetta_Stone', 1, 1)
INSERT [dbo].[Artifact] ([ArtifactSK], [ArtifactName], [ShortDescrition], [PeriodBegin], [PeriodEnd], [CountryFK], [PlaceOfOrigin], [Link], [MuseumFK], [Approved]) VALUES (4, N'Szíriuszi Űrhajó', N'Nagyon valódi ősmagyar', -50000, -50000, 6, N'Mucsaröcsöge', N'kamulink', NULL, 0)
INSERT [dbo].[Artifact] ([ArtifactSK], [ArtifactName], [ShortDescrition], [PeriodBegin], [PeriodEnd], [CountryFK], [PlaceOfOrigin], [Link], [MuseumFK], [Approved]) VALUES (5, N'Ősmagyar gránátvető', NULL, -40000, -40000, 6, NULL, N'másik kamulink', 10, 0)
SET IDENTITY_INSERT [dbo].[Artifact] OFF
SET IDENTITY_INSERT [dbo].[Country] ON 

INSERT [dbo].[Country] ([CountrySK], [CountryName]) VALUES (1, N'England')
INSERT [dbo].[Country] ([CountrySK], [CountryName]) VALUES (2, N'Italy')
INSERT [dbo].[Country] ([CountrySK], [CountryName]) VALUES (3, N'France')
INSERT [dbo].[Country] ([CountrySK], [CountryName]) VALUES (4, N'Germany')
INSERT [dbo].[Country] ([CountrySK], [CountryName]) VALUES (5, N'Poland')
INSERT [dbo].[Country] ([CountrySK], [CountryName]) VALUES (6, N'Hungary')
INSERT [dbo].[Country] ([CountrySK], [CountryName]) VALUES (7, N'United States, CT')
INSERT [dbo].[Country] ([CountrySK], [CountryName]) VALUES (8, N'United States, CA')
INSERT [dbo].[Country] ([CountrySK], [CountryName]) VALUES (9, N'Egypt')
SET IDENTITY_INSERT [dbo].[Country] OFF
INSERT [dbo].[Intermediates] ([UserFK], [ArtifactFK], [IsUploader]) VALUES (N'Régész János', 1, 0)
INSERT [dbo].[Intermediates] ([UserFK], [ArtifactFK], [IsUploader]) VALUES (N'Történész Edit', 1, 0)
SET IDENTITY_INSERT [dbo].[Museum] ON 

INSERT [dbo].[Museum] ([MuseumSK], [MuseumName], [Website], [CountryFK], [City], [Address], [Approved]) VALUES (1, N'British Museum', N'https://www.britishmuseum.org/', 1, N'London', N'Great Russell St, Bloomsbury', 1)
INSERT [dbo].[Museum] ([MuseumSK], [MuseumName], [Website], [CountryFK], [City], [Address], [Approved]) VALUES (2, N'Yale University, Beinecke Rare Book & Manuscript Library', N'https://beinecke.library.yale.edu/', 7, N'New Haven', N'121 Wall St', 1)
INSERT [dbo].[Museum] ([MuseumSK], [MuseumName], [Website], [CountryFK], [City], [Address], [Approved]) VALUES (3, N'MiNdeNtuDás MuZeUMa', N'kamulink', 6, N'Budapest', N'Panel a nyócban', 0)
SET IDENTITY_INSERT [dbo].[Museum] OFF
SET IDENTITY_INSERT [dbo].[Statuses] ON 

INSERT [dbo].[Statuses] ([StatusSK], [StatusName]) VALUES (1, N'Amateur historian\Reenactor')
INSERT [dbo].[Statuses] ([StatusSK], [StatusName]) VALUES (2, N'Amateur archeologist')
INSERT [dbo].[Statuses] ([StatusSK], [StatusName]) VALUES (3, N'Craftsman')
INSERT [dbo].[Statuses] ([StatusSK], [StatusName]) VALUES (4, N'Professional historian')
INSERT [dbo].[Statuses] ([StatusSK], [StatusName]) VALUES (5, N'Professional archeologist')
SET IDENTITY_INSERT [dbo].[Statuses] OFF
ALTER TABLE [dbo].[AppUsers] ADD  CONSTRAINT [DF_User_Admin]  DEFAULT ((0)) FOR [Admin]
GO
ALTER TABLE [dbo].[AppUsers] ADD  CONSTRAINT [DF_AppUsers_Suspended]  DEFAULT ((0)) FOR [Suspended]
GO
ALTER TABLE [dbo].[Artifact] ADD  CONSTRAINT [DF_Artifact_Approved]  DEFAULT ((0)) FOR [Approved]
GO
ALTER TABLE [dbo].[Intermediates] ADD  CONSTRAINT [DF_Intermediates_IsUploader]  DEFAULT ((0)) FOR [IsUploader]
GO
ALTER TABLE [dbo].[Museum] ADD  CONSTRAINT [DF_Museum_Approved]  DEFAULT ((0)) FOR [Approved]
GO
ALTER TABLE [dbo].[AppUsers]  WITH NOCHECK ADD  CONSTRAINT [FK_AppUsers_Statuses] FOREIGN KEY([StatusFK])
REFERENCES [dbo].[Statuses] ([StatusSK])
GO
ALTER TABLE [dbo].[AppUsers] CHECK CONSTRAINT [FK_AppUsers_Statuses]
GO
ALTER TABLE [dbo].[Artifact]  WITH NOCHECK ADD  CONSTRAINT [FK_Artifact_Country] FOREIGN KEY([CountryFK])
REFERENCES [dbo].[Country] ([CountrySK])
GO
ALTER TABLE [dbo].[Artifact] CHECK CONSTRAINT [FK_Artifact_Country]
GO
ALTER TABLE [dbo].[Artifact]  WITH NOCHECK ADD  CONSTRAINT [FK_Artifact_Museum] FOREIGN KEY([MuseumFK])
REFERENCES [dbo].[Museum] ([MuseumSK])
GO
ALTER TABLE [dbo].[Artifact] CHECK CONSTRAINT [FK_Artifact_Museum]
GO
ALTER TABLE [dbo].[Intermediates]  WITH NOCHECK ADD  CONSTRAINT [FK_Intermediates_AppUsers] FOREIGN KEY([UserFK])
REFERENCES [dbo].[AppUsers] ([UserID])
GO
ALTER TABLE [dbo].[Intermediates] CHECK CONSTRAINT [FK_Intermediates_AppUsers]
GO
ALTER TABLE [dbo].[Intermediates]  WITH NOCHECK ADD  CONSTRAINT [FK_Intermediates_Artifact] FOREIGN KEY([ArtifactFK])
REFERENCES [dbo].[Artifact] ([ArtifactSK])
GO
ALTER TABLE [dbo].[Intermediates] CHECK CONSTRAINT [FK_Intermediates_Artifact]
GO
ALTER TABLE [dbo].[Museum]  WITH NOCHECK ADD  CONSTRAINT [FK_Museum_Country] FOREIGN KEY([CountryFK])
REFERENCES [dbo].[Country] ([CountrySK])
GO
ALTER TABLE [dbo].[Museum] CHECK CONSTRAINT [FK_Museum_Country]
GO
