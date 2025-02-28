USE [master]
GO
/****** Object:  Database [InvoiceHandling]    Script Date: 1/17/2019 1:22:22 AM ******/
CREATE DATABASE [InvoiceHandling]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'InvoiceHandling', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLR2\MSSQL\DATA\InvoiceHandling.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'InvoiceHandling_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLR2\MSSQL\DATA\InvoiceHandling_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [InvoiceHandling].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [InvoiceHandling] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [InvoiceHandling] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [InvoiceHandling] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [InvoiceHandling] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [InvoiceHandling] SET ARITHABORT OFF 
GO
ALTER DATABASE [InvoiceHandling] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [InvoiceHandling] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [InvoiceHandling] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [InvoiceHandling] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [InvoiceHandling] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [InvoiceHandling] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [InvoiceHandling] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [InvoiceHandling] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [InvoiceHandling] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [InvoiceHandling] SET  DISABLE_BROKER 
GO
ALTER DATABASE [InvoiceHandling] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [InvoiceHandling] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [InvoiceHandling] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [InvoiceHandling] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [InvoiceHandling] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [InvoiceHandling] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [InvoiceHandling] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [InvoiceHandling] SET RECOVERY FULL 
GO
ALTER DATABASE [InvoiceHandling] SET  MULTI_USER 
GO
ALTER DATABASE [InvoiceHandling] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [InvoiceHandling] SET DB_CHAINING OFF 
GO
ALTER DATABASE [InvoiceHandling] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [InvoiceHandling] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'InvoiceHandling', N'ON'
GO
USE [InvoiceHandling]
GO
/****** Object:  Table [dbo].[AccountProfile]    Script Date: 1/17/2019 1:22:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountProfile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[Mobile] [nvarchar](50) NULL,
	[Nic] [nvarchar](50) NULL,
	[AddressLine1] [nvarchar](50) NULL,
	[AddressLine2] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[SystemUserId] [int] NOT NULL,
	[UserStatusTypeId] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
	[ProfileImage] [nvarchar](max) NULL,
	[UserRoleId] [int] NOT NULL,
 CONSTRAINT [PK_AccountProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityLog]    Script Date: 1/17/2019 1:22:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[Description] [varchar](max) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
	[InvoiceId] [int] NULL,
 CONSTRAINT [PK_ActivityLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BankAcount]    Script Date: 1/17/2019 1:22:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankAcount](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountNumber] [varchar](50) NOT NULL,
	[AccountName] [varchar](max) NOT NULL,
	[Bank] [varchar](50) NOT NULL,
	[Branch] [varchar](50) NOT NULL,
	[Info] [varchar](50) NULL,
	[ServiceProviderId] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_BankAcount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ForgotPassword]    Script Date: 1/17/2019 1:22:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ForgotPassword](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](50) NULL,
	[ResetedBy] [int] NULL,
	[ResetedDate] [datetime] NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
 CONSTRAINT [PK_ForgotPassword] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 1/17/2019 1:22:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](max) NULL,
	[ServiceProvideId] [int] NOT NULL,
	[Quentity] [int] NULL,
	[SubTotal] [decimal](18, 0) NULL,
	[ServiceFee] [decimal](18, 0) NULL,
	[Total] [decimal](18, 0) NULL,
	[InvoiceSubject] [varchar](100) NULL,
	[InvoiceStatus] [int] NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceItem]    Script Date: 1/17/2019 1:22:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[ItemId] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_InvoiceItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Item]    Script Date: 1/17/2019 1:22:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ItemDescription] [varchar](max) NULL,
	[UnitPrice] [decimal](18, 0) NULL,
	[ItemCode] [varchar](50) NULL,
	[ItemName] [varchar](100) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quotation]    Script Date: 1/17/2019 1:22:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quotation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RefferenceId] [int] NULL,
	[EstimatedQuentity] [int] NULL,
	[ApprovedAdminId] [int] NULL,
	[EstimatedSubTotal] [decimal](18, 0) NULL,
	[EstimatedServiceFee] [decimal](18, 0) NULL,
	[EstimatedTotal] [decimal](18, 0) NULL,
	[JobDescription] [varchar](max) NULL,
	[ServiceProviderId] [int] NOT NULL,
	[Status] [int] NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_Quotation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuotationItem]    Script Date: 1/17/2019 1:22:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuotationItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuotationId] [int] NOT NULL,
	[ItemId] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_QuotationItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuoteRequest]    Script Date: 1/17/2019 1:22:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuoteRequest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](max) NULL,
	[DueDate] [datetime] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_QuoteRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuoteRequestDetails]    Script Date: 1/17/2019 1:22:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuoteRequestDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuoteRequestId] [int] NOT NULL,
	[ServiceProviderId] [int] NOT NULL,
	[RefId] [int] NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_QuoteRequestDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceProvider]    Script Date: 1/17/2019 1:22:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceProvider](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CatId] [int] NULL,
	[Address] [varchar](max) NULL,
	[CompanyName] [varchar](50) NULL,
	[Category] [varchar](50) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_ServiceProvider] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemUsers]    Script Date: 1/17/2019 1:22:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserGuid] [uniqueidentifier] NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[DeletedAt] [datetime] NULL,
	[CreatedAt] [datetime] NULL,
	[ModifiedAt] [datetime] NULL,
 CONSTRAINT [PK_SystemUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 1/17/2019 1:22:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DueDate] [datetime] NULL,
	[PropertyHolderName] [varchar](100) NULL,
	[PHAddress] [varchar](max) NULL,
	[PHPhoneNumber] [varchar](50) NULL,
	[AssignStatus] [varchar](50) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 1/17/2019 1:22:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserStatusTypes]    Script Date: 1/17/2019 1:22:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserStatusTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_UserStatusTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkOrder]    Script Date: 1/17/2019 1:22:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkOrder](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [int] NULL,
	[DueDate] [datetime] NULL,
	[Description] [varchar](max) NULL,
	[ServiceProviderID] [int] NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ModifiedAt] [datetime] NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_WorkOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[AccountProfile]  WITH CHECK ADD  CONSTRAINT [FK_AccountProfile_SystemUsers] FOREIGN KEY([SystemUserId])
REFERENCES [dbo].[SystemUsers] ([Id])
GO
ALTER TABLE [dbo].[AccountProfile] CHECK CONSTRAINT [FK_AccountProfile_SystemUsers]
GO
ALTER TABLE [dbo].[AccountProfile]  WITH CHECK ADD  CONSTRAINT [FK_AccountProfile_UserRole] FOREIGN KEY([UserRoleId])
REFERENCES [dbo].[UserRole] ([Id])
GO
ALTER TABLE [dbo].[AccountProfile] CHECK CONSTRAINT [FK_AccountProfile_UserRole]
GO
ALTER TABLE [dbo].[AccountProfile]  WITH CHECK ADD  CONSTRAINT [FK_AccountProfile_UserStatusTypes] FOREIGN KEY([UserStatusTypeId])
REFERENCES [dbo].[UserStatusTypes] ([Id])
GO
ALTER TABLE [dbo].[AccountProfile] CHECK CONSTRAINT [FK_AccountProfile_UserStatusTypes]
GO
ALTER TABLE [dbo].[BankAcount]  WITH CHECK ADD  CONSTRAINT [FK_BankAcount_ServiceProvider] FOREIGN KEY([ServiceProviderId])
REFERENCES [dbo].[ServiceProvider] ([Id])
GO
ALTER TABLE [dbo].[BankAcount] CHECK CONSTRAINT [FK_BankAcount_ServiceProvider]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_ServiceProvider] FOREIGN KEY([ServiceProvideId])
REFERENCES [dbo].[ServiceProvider] ([Id])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_ServiceProvider]
GO
ALTER TABLE [dbo].[InvoiceItem]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceItem_Invoice] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([Id])
GO
ALTER TABLE [dbo].[InvoiceItem] CHECK CONSTRAINT [FK_InvoiceItem_Invoice]
GO
ALTER TABLE [dbo].[InvoiceItem]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceItem_Item] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([Id])
GO
ALTER TABLE [dbo].[InvoiceItem] CHECK CONSTRAINT [FK_InvoiceItem_Item]
GO
ALTER TABLE [dbo].[Quotation]  WITH CHECK ADD  CONSTRAINT [FK_Quotation_Quotation] FOREIGN KEY([ServiceProviderId])
REFERENCES [dbo].[ServiceProvider] ([Id])
GO
ALTER TABLE [dbo].[Quotation] CHECK CONSTRAINT [FK_Quotation_Quotation]
GO
ALTER TABLE [dbo].[QuotationItem]  WITH CHECK ADD  CONSTRAINT [FK_QuotationItem_Item] FOREIGN KEY([ItemId])
REFERENCES [dbo].[Item] ([Id])
GO
ALTER TABLE [dbo].[QuotationItem] CHECK CONSTRAINT [FK_QuotationItem_Item]
GO
ALTER TABLE [dbo].[QuotationItem]  WITH CHECK ADD  CONSTRAINT [FK_QuotationItem_Quotation] FOREIGN KEY([QuotationId])
REFERENCES [dbo].[Quotation] ([Id])
GO
ALTER TABLE [dbo].[QuotationItem] CHECK CONSTRAINT [FK_QuotationItem_Quotation]
GO
ALTER TABLE [dbo].[QuoteRequestDetails]  WITH CHECK ADD  CONSTRAINT [FK_QuoteRequestDetails_QuoteRequest] FOREIGN KEY([QuoteRequestId])
REFERENCES [dbo].[QuoteRequest] ([Id])
GO
ALTER TABLE [dbo].[QuoteRequestDetails] CHECK CONSTRAINT [FK_QuoteRequestDetails_QuoteRequest]
GO
ALTER TABLE [dbo].[QuoteRequestDetails]  WITH CHECK ADD  CONSTRAINT [FK_QuoteRequestDetails_ServiceProvider] FOREIGN KEY([ServiceProviderId])
REFERENCES [dbo].[ServiceProvider] ([Id])
GO
ALTER TABLE [dbo].[QuoteRequestDetails] CHECK CONSTRAINT [FK_QuoteRequestDetails_ServiceProvider]
GO
ALTER TABLE [dbo].[WorkOrder]  WITH CHECK ADD  CONSTRAINT [FK_WorkOrder_ServiceProvider] FOREIGN KEY([ServiceProviderID])
REFERENCES [dbo].[ServiceProvider] ([Id])
GO
ALTER TABLE [dbo].[WorkOrder] CHECK CONSTRAINT [FK_WorkOrder_ServiceProvider]
GO
USE [master]
GO
ALTER DATABASE [InvoiceHandling] SET  READ_WRITE 
GO
