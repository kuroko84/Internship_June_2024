USE [master]
GO
/****** Object:  Database [Student_Management]    Script Date: 6/25/2024 2:04:57 AM ******/
CREATE DATABASE [Student_Management]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Student_Management', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Student_Management.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Student_Management_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Student_Management_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Student_Management] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Student_Management].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Student_Management] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Student_Management] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Student_Management] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Student_Management] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Student_Management] SET ARITHABORT OFF 
GO
ALTER DATABASE [Student_Management] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Student_Management] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Student_Management] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Student_Management] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Student_Management] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Student_Management] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Student_Management] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Student_Management] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Student_Management] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Student_Management] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Student_Management] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Student_Management] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Student_Management] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Student_Management] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Student_Management] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Student_Management] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Student_Management] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Student_Management] SET RECOVERY FULL 
GO
ALTER DATABASE [Student_Management] SET  MULTI_USER 
GO
ALTER DATABASE [Student_Management] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Student_Management] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Student_Management] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Student_Management] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Student_Management] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Student_Management] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Student_Management', N'ON'
GO
ALTER DATABASE [Student_Management] SET QUERY_STORE = ON
GO
ALTER DATABASE [Student_Management] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Student_Management]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/25/2024 2:04:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Classes]    Script Date: 6/25/2024 2:04:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[NumberOfStudent] [int] NOT NULL,
	[StartDay] [datetime2](7) NOT NULL,
	[EndDay] [datetime2](7) NOT NULL,
	[SubjectId] [int] NOT NULL,
 CONSTRAINT [PK_Classes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Enrollments]    Script Date: 6/25/2024 2:04:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enrollments](
	[StudentId] [int] NOT NULL,
	[ClassId] [int] NOT NULL,
 CONSTRAINT [PK_Enrollments] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC,
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Scores]    Script Date: 6/25/2024 2:04:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Scores](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Mark] [float] NOT NULL,
	[SubjectId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
 CONSTRAINT [PK_Scores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 6/25/2024 2:04:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subjects]    Script Date: 6/25/2024 2:04:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subjects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Subjects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240624180302_InitialCreate', N'8.0.6')
GO
SET IDENTITY_INSERT [dbo].[Classes] ON 

INSERT [dbo].[Classes] ([Id], [Name], [Description], [NumberOfStudent], [StartDay], [EndDay], [SubjectId]) VALUES (8, N'Math Class 101', N'Introduction to Mathematics', 50, CAST(N'2024-06-25T01:42:41.4600672' AS DateTime2), CAST(N'2024-09-25T01:42:41.4627244' AS DateTime2), 1)
INSERT [dbo].[Classes] ([Id], [Name], [Description], [NumberOfStudent], [StartDay], [EndDay], [SubjectId]) VALUES (9, N'Physics Class 101', N'Introduction to Physics', 50, CAST(N'2024-06-25T01:42:41.4628841' AS DateTime2), CAST(N'2024-09-25T01:42:41.4628847' AS DateTime2), 2)
SET IDENTITY_INSERT [dbo].[Classes] OFF
GO
INSERT [dbo].[Enrollments] ([StudentId], [ClassId]) VALUES (2, 8)
INSERT [dbo].[Enrollments] ([StudentId], [ClassId]) VALUES (1, 9)
GO
SET IDENTITY_INSERT [dbo].[Scores] ON 

INSERT [dbo].[Scores] ([Id], [Mark], [SubjectId], [StudentId]) VALUES (8, 9, 2, 1)
INSERT [dbo].[Scores] ([Id], [Mark], [SubjectId], [StudentId]) VALUES (9, 9.5, 1, 2)
SET IDENTITY_INSERT [dbo].[Scores] OFF
GO
SET IDENTITY_INSERT [dbo].[Students] ON 

INSERT [dbo].[Students] ([Id], [Name], [DateOfBirth]) VALUES (1, N'John Doe', CAST(N'2000-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Students] ([Id], [Name], [DateOfBirth]) VALUES (2, N'Jane Smith', CAST(N'2001-02-02T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Students] OFF
GO
SET IDENTITY_INSERT [dbo].[Subjects] ON 

INSERT [dbo].[Subjects] ([Id], [Name], [Description]) VALUES (1, N'Mathematics', N'Study of numbers, quantities, and shapes.')
INSERT [dbo].[Subjects] ([Id], [Name], [Description]) VALUES (2, N'Physics', N'Study of matter, energy, and the interaction between them.')
SET IDENTITY_INSERT [dbo].[Subjects] OFF
GO
/****** Object:  Index [IX_Classes_SubjectId]    Script Date: 6/25/2024 2:04:57 AM ******/
CREATE NONCLUSTERED INDEX [IX_Classes_SubjectId] ON [dbo].[Classes]
(
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Enrollments_ClassId]    Script Date: 6/25/2024 2:04:57 AM ******/
CREATE NONCLUSTERED INDEX [IX_Enrollments_ClassId] ON [dbo].[Enrollments]
(
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Scores_StudentId]    Script Date: 6/25/2024 2:04:57 AM ******/
CREATE NONCLUSTERED INDEX [IX_Scores_StudentId] ON [dbo].[Scores]
(
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Scores_SubjectId]    Script Date: 6/25/2024 2:04:57 AM ******/
CREATE NONCLUSTERED INDEX [IX_Scores_SubjectId] ON [dbo].[Scores]
(
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_Classes_Subjects_SubjectId] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subjects] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_Classes_Subjects_SubjectId]
GO
ALTER TABLE [dbo].[Enrollments]  WITH CHECK ADD  CONSTRAINT [FK_Enrollments_Classes_ClassId] FOREIGN KEY([ClassId])
REFERENCES [dbo].[Classes] ([Id])
GO
ALTER TABLE [dbo].[Enrollments] CHECK CONSTRAINT [FK_Enrollments_Classes_ClassId]
GO
ALTER TABLE [dbo].[Enrollments]  WITH CHECK ADD  CONSTRAINT [FK_Enrollments_Students_StudentId] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([Id])
GO
ALTER TABLE [dbo].[Enrollments] CHECK CONSTRAINT [FK_Enrollments_Students_StudentId]
GO
ALTER TABLE [dbo].[Scores]  WITH CHECK ADD  CONSTRAINT [FK_Scores_Students_StudentId] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Students] ([Id])
GO
ALTER TABLE [dbo].[Scores] CHECK CONSTRAINT [FK_Scores_Students_StudentId]
GO
ALTER TABLE [dbo].[Scores]  WITH CHECK ADD  CONSTRAINT [FK_Scores_Subjects_SubjectId] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subjects] ([Id])
GO
ALTER TABLE [dbo].[Scores] CHECK CONSTRAINT [FK_Scores_Subjects_SubjectId]
GO
USE [master]
GO
ALTER DATABASE [Student_Management] SET  READ_WRITE 
GO
