USE [master]
GO
/****** Object:  Database [MyShop]    Script Date: 6/2/2026 8:59:30 PM ******/
CREATE DATABASE [MyShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MyShop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MyShop.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MyShop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MyShop_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MyShop] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MyShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MyShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MyShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MyShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MyShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MyShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [MyShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MyShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MyShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MyShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MyShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MyShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MyShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MyShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MyShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MyShop] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MyShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MyShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MyShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MyShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MyShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MyShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MyShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MyShop] SET RECOVERY FULL 
GO
ALTER DATABASE [MyShop] SET  MULTI_USER 
GO
ALTER DATABASE [MyShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MyShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MyShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MyShop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MyShop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MyShop] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MyShop', N'ON'
GO
ALTER DATABASE [MyShop] SET QUERY_STORE = ON
GO
ALTER DATABASE [MyShop] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MyShop]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/2/2026 8:59:30 PM ******/
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
/****** Object:  Table [dbo].[cart]    Script Date: 6/2/2026 8:59:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[customerId] [int] NULL,
	[createAt] [datetime] NULL,
	[productId] [int] NULL,
	[quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[category]    Script Date: 6/2/2026 8:59:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](100) NULL,
	[content] [nvarchar](500) NULL,
	[createAt] [datetime] NULL,
	[updateAt] [datetime] NULL,
	[img] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[contact]    Script Date: 6/2/2026 8:59:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[contact](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [nvarchar](100) NULL,
	[lastName] [nvarchar](100) NULL,
	[phone] [nvarchar](15) NULL,
	[email] [nvarchar](50) NULL,
	[message] [nvarchar](1000) NULL,
	[createAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer]    Script Date: 6/2/2026 8:59:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [nvarchar](100) NULL,
	[lastName] [nvarchar](100) NULL,
	[address] [nvarchar](500) NULL,
	[phone] [nvarchar](15) NULL,
	[email] [nvarchar](50) NULL,
	[img] [nvarchar](200) NULL,
	[registeredAt] [datetime] NULL,
	[updateAt] [datetime] NULL,
	[dateOfBirth] [date] NULL,
	[password] [nvarchar](200) NULL,
	[randomKey] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[role] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[menu]    Script Date: 6/2/2026 8:59:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[menu](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[parentId] [int] NULL,
	[title] [nvarchar](100) NULL,
	[menuUrl] [nvarchar](100) NULL,
	[menuIndex] [int] NULL,
	[isVisible] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[payment]    Script Date: 6/2/2026 8:59:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[createAt] [datetime] NULL,
	[total] [float] NULL,
	[cartId] [int] NULL,
	[status] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[paymentDetail]    Script Date: 6/2/2026 8:59:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[paymentDetail](
	[productId] [int] NOT NULL,
	[paymentId] [int] NOT NULL,
	[price] [int] NULL,
	[quantity] [int] NULL,
	[total] [float] NULL,
	[createAt] [datetime] NULL,
 CONSTRAINT [PK_a] PRIMARY KEY CLUSTERED 
(
	[productId] ASC,
	[paymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 6/2/2026 8:59:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](100) NULL,
	[content] [nvarchar](500) NULL,
	[img] [nvarchar](200) NULL,
	[price] [int] NULL,
	[rate] [float] NULL,
	[createAt] [datetime] NULL,
	[updateAt] [datetime] NULL,
	[categoryId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[review]    Script Date: 6/2/2026 8:59:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[review](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[rate] [int] NULL,
	[message] [nvarchar](max) NULL,
	[createAt] [datetime] NULL,
	[productId] [int] NULL,
	[customerId] [int] NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[cart] ON 
GO
INSERT [dbo].[cart] ([id], [customerId], [createAt], [productId], [quantity]) VALUES (1, 1, CAST(N'2026-01-07T20:11:28.753' AS DateTime), 6, 1)
GO
INSERT [dbo].[cart] ([id], [customerId], [createAt], [productId], [quantity]) VALUES (2, 1, CAST(N'2026-01-09T14:56:16.710' AS DateTime), 23, 2)
GO
INSERT [dbo].[cart] ([id], [customerId], [createAt], [productId], [quantity]) VALUES (3, 1, CAST(N'2026-04-05T16:45:20.560' AS DateTime), 1, 1)
GO
INSERT [dbo].[cart] ([id], [customerId], [createAt], [productId], [quantity]) VALUES (4, 1, CAST(N'2026-05-28T15:28:40.320' AS DateTime), 3, 1)
GO
INSERT [dbo].[cart] ([id], [customerId], [createAt], [productId], [quantity]) VALUES (5, 1, CAST(N'2026-05-28T15:28:44.847' AS DateTime), 3, 1)
GO
INSERT [dbo].[cart] ([id], [customerId], [createAt], [productId], [quantity]) VALUES (6, 1, CAST(N'2026-05-28T15:28:50.747' AS DateTime), 3, 2)
GO
INSERT [dbo].[cart] ([id], [customerId], [createAt], [productId], [quantity]) VALUES (7, 1, CAST(N'2026-05-28T15:28:56.717' AS DateTime), 3, 2)
GO
INSERT [dbo].[cart] ([id], [customerId], [createAt], [productId], [quantity]) VALUES (8, 1, CAST(N'2026-05-28T15:29:40.697' AS DateTime), 23, 3)
GO
INSERT [dbo].[cart] ([id], [customerId], [createAt], [productId], [quantity]) VALUES (9, 1, CAST(N'2026-05-28T15:29:46.200' AS DateTime), 23, 1)
GO
INSERT [dbo].[cart] ([id], [customerId], [createAt], [productId], [quantity]) VALUES (10, 1, CAST(N'2026-05-28T15:35:37.610' AS DateTime), 4, 1)
GO
INSERT [dbo].[cart] ([id], [customerId], [createAt], [productId], [quantity]) VALUES (11, 1, CAST(N'2026-05-29T12:56:10.940' AS DateTime), 23, 1)
GO
INSERT [dbo].[cart] ([id], [customerId], [createAt], [productId], [quantity]) VALUES (12, 1, CAST(N'2026-05-29T12:58:34.960' AS DateTime), 23, 1)
GO
SET IDENTITY_INSERT [dbo].[cart] OFF
GO
SET IDENTITY_INSERT [dbo].[category] ON 
GO
INSERT [dbo].[category] ([id], [title], [content], [createAt], [updateAt], [img]) VALUES (1, N'Điện Thoại', N'Cung cấp tất cả những sản phẩm chính hãng của thương hiệu Apple như là: iPhone, iPad, Watch, Mac và cả những sản phẩm Samsung cùng với phụ Kiện Xiaomi.', CAST(N'2024-07-24T14:33:29.983' AS DateTime), CAST(N'2025-12-30T15:38:15.497' AS DateTime), N'20251230153811iphone-14-pro-max-512.jpg')
GO
INSERT [dbo].[category] ([id], [title], [content], [createAt], [updateAt], [img]) VALUES (2, N'Laptop', N'Tự hào là một trong những đơn vị hàng đầu trong lĩnh vực kinh doanh laptop, linh kiện laptop tại Khánh Hòa, Với trên 10 năm kinh nghiệm, theo phương châm "Uy tín trên từng sản phẩm"  cùng hơn 50.000 Khách Hàng thân thiết, chúng tôi cam kết tất cả các sản phẩm laptop bán ra đều có chất lượng tốt nhất trên thị trường hiện nay. Tất cả laptop, linh kiện tại showroom đều được bảo hành chuẩn chỉ theo quy chế của các hãng.', CAST(N'2024-07-24T14:33:29.983' AS DateTime), CAST(N'2025-12-30T15:38:51.710' AS DateTime), N'20251230153851vi-vn-msi-gaming-gf63-thin-11uc-i7-1228vn-slider-5.jpg')
GO
INSERT [dbo].[category] ([id], [title], [content], [createAt], [updateAt], [img]) VALUES (3, N'PC', N'Chuyên cung cấp các sản phẩm laptop xách tay Mỹ, máy tính để bàn chính hãng zin 100%, các loại linh kiện vi tính chính hãng... Đến với Worklap, người dùng sẽ được trải nghiệm các thiết bị máy tính laptop xách tay chất lượng với nhiều phân khúc giá, thương hiệu, phù hợp với nhiều nhu cầu sử dụng khác nhau.', CAST(N'2024-07-24T14:33:29.983' AS DateTime), CAST(N'2025-12-30T15:40:58.623' AS DateTime), N'20251230154058vi-vn-hp-tp014018d-i3-8x3r4pa-2.jpg')
GO
INSERT [dbo].[category] ([id], [title], [content], [createAt], [updateAt], [img]) VALUES (4, N'Tablet', N'Khi mua một chiếc tablet, chắc hẳn bạn sẽ phải tìm hiểu rất kỹ mới có thể đưa ra quyết định, vì con số chi trả cho nó không phải nhỏ. Và không biết thiết bị đó có thể hỗ trợ liên lạc, học tập, giải trí,... Vậy hãy cùng tìm hiểu kĩ về tablet và cùng tham khảo các thương hiệu Tablet HOT nhất hiện nay để có được sự lựa chọn phù hợp bạn nhé!', CAST(N'2024-07-24T14:33:29.983' AS DateTime), CAST(N'2025-12-30T15:43:44.650' AS DateTime), N'20251230154344vi-vn-tcl-tab-10-gen-2-slider--(2).jpg')
GO
INSERT [dbo].[category] ([id], [title], [content], [createAt], [updateAt], [img]) VALUES (5, N'Phụ kiện', N'Cung cấp nhiều phụ kiện mới, nổi bật khác như: Phụ Kiện Apple Watch, Củ Sạc - Sạc Không Dây, Cáp Sạc - Đầu Chuyển, Pin - Sạc Dự Phòng - Ốp Sạc, Tai Nghe - Tai Nghe Không Dây, Loa Không Dây - Tv Box, Bàn Phím - Gậy Chụp Ảnh, Tb.Lưu Trữ - Đ.C Công Nghệ, Túi Đeo - Bao Da - Sim Ghép, Phụ Kiện Công Nghệ Xiaomi , Vòng Đeo Tay Thông Minh.', CAST(N'2024-07-24T14:33:29.983' AS DateTime), CAST(N'2025-12-30T15:41:22.500' AS DateTime), N'20251230154122loa-vi-tinh-2-1-fenda-f670x-3-2.jpg')
GO
INSERT [dbo].[category] ([id], [title], [content], [createAt], [updateAt], [img]) VALUES (6, N'Smart Watch', N'Nếu bạn đang tìm kiếm một chiếc smartwatch chính hãng có thiết kế được ưa chuộng cho bản thân hoặc dành tặng cho người quen hãy tham khảo ngay top 7 các hãng đồng hồ thông minh chính hãng rất được ưa chuộng tại website của chúng tôi để tha hồ lựa chọn.', CAST(N'2024-07-24T14:33:29.983' AS DateTime), CAST(N'2025-12-30T15:43:22.550' AS DateTime), N'20251230154322SMW.jpg')
GO
SET IDENTITY_INSERT [dbo].[category] OFF
GO
SET IDENTITY_INSERT [dbo].[customer] ON 
GO
INSERT [dbo].[customer] ([id], [firstName], [lastName], [address], [phone], [email], [img], [registeredAt], [updateAt], [dateOfBirth], [password], [randomKey], [isActive], [role]) VALUES (1, N'huy', N'gia', N'96 ND', N'0917036381', N'huy.pg.64cntt@ntu.edu.vn', N'avatar-default.jpg', CAST(N'2025-12-30T15:35:06.377' AS DateTime), CAST(N'2025-12-30T15:35:06.377' AS DateTime), CAST(N'2004-04-10' AS Date), N'089bc6e0c7795a92408de895f596b610', N'vw!jm', 1, 1)
GO
INSERT [dbo].[customer] ([id], [firstName], [lastName], [address], [phone], [email], [img], [registeredAt], [updateAt], [dateOfBirth], [password], [randomKey], [isActive], [role]) VALUES (2, N'huy', N'huy', N'93 NĐ', N'0905039109', N'123@gmail.com', N'avatar-default.jpg', CAST(N'2025-12-30T15:36:02.380' AS DateTime), CAST(N'2025-12-30T15:51:47.477' AS DateTime), CAST(N'2000-03-11' AS Date), N'f5020af1e48e3d3bd547c83462006d6b', N'MeTje', 1, 0)
GO
SET IDENTITY_INSERT [dbo].[customer] OFF
GO
SET IDENTITY_INSERT [dbo].[menu] ON 
GO
INSERT [dbo].[menu] ([id], [parentId], [title], [menuUrl], [menuIndex], [isVisible]) VALUES (1, NULL, N'Trang chủ', N'/', 1, 1)
GO
INSERT [dbo].[menu] ([id], [parentId], [title], [menuUrl], [menuIndex], [isVisible]) VALUES (2, NULL, N'Cửa hàng', N'/Product', 2, 1)
GO
INSERT [dbo].[menu] ([id], [parentId], [title], [menuUrl], [menuIndex], [isVisible]) VALUES (3, NULL, N'Danh mục', NULL, 3, 1)
GO
INSERT [dbo].[menu] ([id], [parentId], [title], [menuUrl], [menuIndex], [isVisible]) VALUES (4, 3, N'Điện thoại', N'/Product?idcategory=1', 4, 1)
GO
INSERT [dbo].[menu] ([id], [parentId], [title], [menuUrl], [menuIndex], [isVisible]) VALUES (5, 3, N'Laptop', N'/Product?idcategory=2', 5, 1)
GO
INSERT [dbo].[menu] ([id], [parentId], [title], [menuUrl], [menuIndex], [isVisible]) VALUES (6, 3, N'PC', N'/Product?idcategory=3', 6, 1)
GO
INSERT [dbo].[menu] ([id], [parentId], [title], [menuUrl], [menuIndex], [isVisible]) VALUES (7, 3, N'Tablet', N'/Product?idcategory=4', 7, 1)
GO
INSERT [dbo].[menu] ([id], [parentId], [title], [menuUrl], [menuIndex], [isVisible]) VALUES (8, 3, N'Phụ kiện', N'/Product?idcategory=5', 8, 1)
GO
INSERT [dbo].[menu] ([id], [parentId], [title], [menuUrl], [menuIndex], [isVisible]) VALUES (9, 3, N'Smart Watch', N'/Product?idcategory=6', 9, 1)
GO
INSERT [dbo].[menu] ([id], [parentId], [title], [menuUrl], [menuIndex], [isVisible]) VALUES (10, NULL, N'Liên hệ', N'/contact', 10, 1)
GO
INSERT [dbo].[menu] ([id], [parentId], [title], [menuUrl], [menuIndex], [isVisible]) VALUES (11, NULL, N'Trang quản trị', N'/Admin/ProductAdmin', 11, 1)
GO
SET IDENTITY_INSERT [dbo].[menu] OFF
GO
SET IDENTITY_INSERT [dbo].[payment] ON 
GO
INSERT [dbo].[payment] ([id], [createAt], [total], [cartId], [status]) VALUES (1, CAST(N'2026-01-07T20:11:28.903' AS DateTime), 109950000, 1, N'Đã đặt hàng')
GO
INSERT [dbo].[payment] ([id], [createAt], [total], [cartId], [status]) VALUES (2, CAST(N'2026-01-09T14:56:16.920' AS DateTime), 2780000, 2, N'Đã đặt hàng')
GO
INSERT [dbo].[payment] ([id], [createAt], [total], [cartId], [status]) VALUES (3, CAST(N'2026-04-05T16:45:20.720' AS DateTime), 18790000, 3, N'Đã đặt hàng')
GO
INSERT [dbo].[payment] ([id], [createAt], [total], [cartId], [status]) VALUES (4, CAST(N'2026-05-28T15:35:37.870' AS DateTime), 18190000, 10, N'Đã đặt hàng')
GO
INSERT [dbo].[payment] ([id], [createAt], [total], [cartId], [status]) VALUES (5, CAST(N'2026-05-29T12:56:11.120' AS DateTime), 1390000, 11, N'Đã đặt hàng')
GO
INSERT [dbo].[payment] ([id], [createAt], [total], [cartId], [status]) VALUES (6, CAST(N'2026-05-29T12:58:34.967' AS DateTime), 20180000, 12, N'Đã đặt hàng')
GO
SET IDENTITY_INSERT [dbo].[payment] OFF
GO
INSERT [dbo].[paymentDetail] ([productId], [paymentId], [price], [quantity], [total], [createAt]) VALUES (1, 1, 18790000, 1, 18790000, CAST(N'2026-01-07T20:11:28.943' AS DateTime))
GO
INSERT [dbo].[paymentDetail] ([productId], [paymentId], [price], [quantity], [total], [createAt]) VALUES (1, 3, 18790000, 1, 18790000, CAST(N'2026-04-05T16:45:20.747' AS DateTime))
GO
INSERT [dbo].[paymentDetail] ([productId], [paymentId], [price], [quantity], [total], [createAt]) VALUES (1, 6, 18790000, 1, 18790000, CAST(N'2026-05-29T12:58:34.973' AS DateTime))
GO
INSERT [dbo].[paymentDetail] ([productId], [paymentId], [price], [quantity], [total], [createAt]) VALUES (3, 1, 16490000, 1, 16490000, CAST(N'2026-01-07T20:11:28.943' AS DateTime))
GO
INSERT [dbo].[paymentDetail] ([productId], [paymentId], [price], [quantity], [total], [createAt]) VALUES (4, 1, 18190000, 1, 18190000, CAST(N'2026-01-07T20:11:28.943' AS DateTime))
GO
INSERT [dbo].[paymentDetail] ([productId], [paymentId], [price], [quantity], [total], [createAt]) VALUES (4, 4, 18190000, 1, 18190000, CAST(N'2026-05-28T15:35:37.933' AS DateTime))
GO
INSERT [dbo].[paymentDetail] ([productId], [paymentId], [price], [quantity], [total], [createAt]) VALUES (5, 1, 33990000, 1, 33990000, CAST(N'2026-01-07T20:11:28.943' AS DateTime))
GO
INSERT [dbo].[paymentDetail] ([productId], [paymentId], [price], [quantity], [total], [createAt]) VALUES (6, 1, 22490000, 1, 22490000, CAST(N'2026-01-07T20:11:28.930' AS DateTime))
GO
INSERT [dbo].[paymentDetail] ([productId], [paymentId], [price], [quantity], [total], [createAt]) VALUES (23, 2, 1390000, 2, 2780000, CAST(N'2026-01-09T14:56:16.947' AS DateTime))
GO
INSERT [dbo].[paymentDetail] ([productId], [paymentId], [price], [quantity], [total], [createAt]) VALUES (23, 5, 1390000, 1, 1390000, CAST(N'2026-05-29T12:56:11.153' AS DateTime))
GO
INSERT [dbo].[paymentDetail] ([productId], [paymentId], [price], [quantity], [total], [createAt]) VALUES (23, 6, 1390000, 1, 1390000, CAST(N'2026-05-29T12:58:34.973' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[product] ON 
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (1, N'MacBook Air 13 inch M1', N'Laptop Apple MacBook Air M1 2020 thuộc dòng laptop cao cấp sang trọng có cấu hình mạnh mẽ, chinh phục được các tính năng văn phòng lẫn đồ hoạ mà bạn mong muốn, thời lượng pin dài, thiết kế mỏng nhẹ sẽ đáp ứng tốt các nhu cầu làm việc của bạn.', N'apple-macbook-air-2020.jpg', 18790000, 0, CAST(N'2024-07-24T14:46:55.457' AS DateTime), CAST(N'2024-07-24T15:54:27.633' AS DateTime), 2)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (2, N'Tai nghe Bluetooth Wireless AVA', N'Tai nghe Bluetooth True Wireless AVA+ FreeGo W26 với màu sắc đẹp mắt, diện mạo sang trọng, âm thanh sôi động, đáp ứng mọi nhu cầu từ giải trí đến làm việc, mang đến cho người dùng những trải nghiệm tốt nhất. • Kiểu dáng tai nghe thanh lịch, thiết kế hộp sạc 4 góc bo mềm mại, kích thước vừa lòng bàn tay, dễ dàng mang theo bất cứ đâu.', N'GalaxyBuds3Pro.png', 215000, 0, CAST(N'2024-07-24T15:05:25.137' AS DateTime), CAST(N'2024-07-24T15:40:06.767' AS DateTime), 5)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (3, N'Laptop MSI Gaming GF63', N'Laptop MSI Gaming GF63 Thin 11UC i7 11800H (1228VN) được trang bị bộ vi xử lý Intel Core i7 dòng H hiệu năng cao và card đồ họa NVIDIA mạnh mẽ, đáp ứng mọi nhu cầu của game thủ và người dùng làm trong ngành sáng tạo nội dung.', N'vi-vn-msi-gaming-gf63-thin-11uc-i7-1228vn-slider-5.jpg', 16490000, 0, CAST(N'2024-07-24T15:08:52.757' AS DateTime), CAST(N'2024-07-24T15:08:52.757' AS DateTime), 2)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (4, N'Laptop Acer Swift 3', N' Ngoại hình tối giản tạo sự hiện đại Laptop được thiết kế gọn nhẹ với khung vỏ kim loại phủ màu bạc trang nhã, cho mình cảm giác cứng cáp khi sờ vào, đồng thời cũng rất mát và sướng tay. Máy có khối lượng nhẹ chỉ 1.2 kg mang tính di động cao, mình có thể bỏ vừa vào balo mang theo khi đi học, đi làm. Logo “Acer” thay vì nằm chính giữa nắp lưng nay đã được bố trí lên vị trí phía cạnh trên, tạo sự mới mẻ, trông gọn gàng và cũng bắt mắt hơn.  Acer Swift 3 SF314 512 56QN i5 1240P - Thiết kế  Viền màn', N'acer-swift-3-sf314-512-56qn-i5-nxk0fsv002-ab-thumb-600x600.jpg', 18190000, 0, CAST(N'2024-07-24T15:10:13.337' AS DateTime), CAST(N'2024-07-24T15:10:13.337' AS DateTime), 2)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (5, N'MacBook Air 13 inch M2', N'MacBook Air M2 2022 một lần nữa đã khẳng định vị thế hàng đầu của Apple trong phân khúc laptop cao cấp - sang trọng vào giữa năm 2022 khi sở hữu phong cách thiết kế thời thượng, đẳng cấp cùng sức mạnh bộc phá đến từ bộ vi xử lý Apple M2 mạnh mẽ.', N'apple-macbook-air-m2-2022-16gb-600x600.jpg', 33990000, 0, CAST(N'2024-07-24T15:11:08.860' AS DateTime), CAST(N'2024-07-24T15:11:08.860' AS DateTime), 2)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (6, N'Laptop HP Envy X360', N'Laptop HP Envy X360 13 bf0112TU i5 1230U (7C0N9PA) là một trong những chiếc laptop cao cấp được yêu thích nhất hiện nay với màn hình cảm ứng mượt mà, tính di động cao cùng hiệu năng mạnh mẽ mang lại những giây phút sáng tạo và giải trí tuyệt vời cho bạn.', N'hp-envy-x360-13-bf0112tu-i5-7c0n9pa-glr-thumb-600x600.jpg', 22490000, 0, CAST(N'2024-07-24T15:11:53.750' AS DateTime), CAST(N'2024-07-24T15:11:53.750' AS DateTime), 2)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (7, N'iPhone 15 Pro Max 256GB', N'iPhone 15 Pro Max là một chiếc điện thoại thông minh cao cấp được mong đợi nhất năm 2023. Với nhiều tính năng mới và cải tiến, iPhone 15 Pro Max chắc chắn sẽ là một lựa chọn tuyệt vời cho những người dùng đang tìm kiếm một chiếc điện thoại có hiệu năng mạnh mẽ, camera chất lượng và thiết kế sang trọng.', N'iphone-15-pro-max-blue-thumbnew-600x600.jpg', 29290000, 0, CAST(N'2024-07-24T15:13:08.833' AS DateTime), CAST(N'2024-07-24T15:13:08.833' AS DateTime), 1)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (8, N'Samsung Galaxy S24 Ultra 5G', N'Samsung Galaxy S24 Ultra 5G 512GB khi ra mắt đã tạo nên cơn sốt thị trường, đặc điểm nổi bật là chip Snapdragon 8 Gen 3 for Galaxy và camera 200 MP tích hợp AI. Mẫu điện thoại này hứa hẹn làm nổi bật trong năm 2024 với sức mạnh và nhiều tính năng đỉnh cao.', N'samsung-galaxy-s24-ultra-5g638417948922989679.jpg', 29990000, 0, CAST(N'2024-07-24T15:18:46.273' AS DateTime), CAST(N'2024-07-24T15:54:17.707' AS DateTime), 1)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (9, N'Samsung Galaxy S23 Ultra 5G', N'Samsung Galaxy S23 Ultra 5G 256GB là chiếc smartphone cao cấp nhất của nhà Samsung, sở hữu cấu hình không tưởng với con chip khủng được Qualcomm tối ưu riêng cho dòng Galaxy và camera lên đến 200 MP, xứng danh là chiếc flagship Android được mong đợi nhất trong năm 2023.', N'samsung-galaxy-s23-ultra-ai-1020x570.jpg', 23990000, 0, CAST(N'2024-07-24T15:21:25.940' AS DateTime), CAST(N'2024-07-24T15:21:25.940' AS DateTime), 1)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (10, N'iPhone 14 Pro Max 128GB', N'iPhone 14 Pro Max một siêu phẩm trong giới smartphone được nhà Táo tung ra thị trường vào tháng 09/2022. Máy trang bị con chip Apple A16 Bionic vô cùng mạnh mẽ, đi kèm theo đó là thiết kế màn hình mới, hứa hẹn mang lại những trải nghiệm đầy mới mẻ cho người dùng iPhone.', N'iphone-14-pro-max-tong-quan-1020x570.jpg', 26990000, 0, CAST(N'2024-07-24T15:23:14.070' AS DateTime), CAST(N'2024-07-24T15:23:14.070' AS DateTime), 1)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (11, N'iPhone 15 Pro Max 512GB', N'Vào tháng 09/2023, cuối cùng Apple cũng đã chính thức giới thiệu iPhone 15 Pro Max 512GB thuộc dòng iPhone 15, tại sự kiện ra mắt thường niên với nhiều điểm đáng chú ý, nổi bật trong số đó có thể kể đến như sự góp mặt của chipset Apple A17 Pro có trên máy, thiết kế khung titan hay cổng Type-C lần đầu có mặt trên điện thoại iPhone.', N'vi-vn-iphone-15-pro-max-4-1020x570.jpg', 36590000, 0, CAST(N'2024-07-24T15:26:07.793' AS DateTime), CAST(N'2024-07-24T15:26:24.253' AS DateTime), 1)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (12, N'iPhone 14 Pro Max 512GB', N'Sau bao nhiêu ngày chờ đợi thì Apple đã chính thức tung ra mẫu điện thoại iPhone 14 Pro Max 512GB khi sở hữu một con chip với hiệu năng mạnh mẽ, màn hình đẹp mắt và cụm camera vô cùng chất lượng.', N'iphone-14-pro-max-512.jpg', 35990000, 0, CAST(N'2024-07-24T15:28:21.430' AS DateTime), CAST(N'2024-07-24T15:28:21.430' AS DateTime), 1)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (13, N'Oppo Watch X 47mm', N'Đồng hồ thông minh Oppo Watch X 47mm là một sản phẩm đột phá, mang đến trải nghiệm hoàn toàn mới cho người dùng. Với thiết kế thời trang sang trọng, màn hình AMOLED lớn và nhiều tính năng thông minh, chiếc đồng hồ này hứa hẹn sẽ trở thành người bạn đồng hành lý tưởng cho cuộc sống hiện đại của bạn.', N'oppo-watch-x-sld.jpg', 6490000, 0, CAST(N'2024-07-24T15:30:29.257' AS DateTime), CAST(N'2024-07-24T15:54:53.510' AS DateTime), 6)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (14, N'Xiaomi Redmi Watch 3 ', N'Tầm giá chưa đến 3 triệu đồng thì một chiếc smartwatch vừa có nghe gọi vừa được trang bị GPS độc lập khá hiếm thấy nhưng gần đây Xiaomi đã cho ra mắt sản phẩm đồng hồ thông minh Xiaomi Redmi Watch 3 có cả hai tính năng trên. Bên cạnh đó còn được trang bị nhiều tính năng hỗ trợ theo dõi sức khỏe và luyện tập phục vụ cho người dùng.', N'vi-vn-oppo-watch-x-sld.jpg', 1990000, 0, CAST(N'2024-07-24T15:31:37.303' AS DateTime), CAST(N'2024-07-24T15:40:29.647' AS DateTime), 6)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (15, N'iMac 24 inch 4.5K M3', N'Nguyên vẹn kiểu mẫu sang trọng cho không gian bày trí thêm đẳng cấp, iMac 24 inch 2023 4.5K M3 8-core CPU với sự tăng tốc vượt trội về phần hiệu năng khi sở hữu con chip M3 thế hệ hoàn toàn mới, khung hình chuẩn sắc nét 4.5K, cùng nhiều tiện ích tuyệt vời với hệ sinh thái của Apple chắc chắn sẽ là mẫu máy tính AIO tuyệt vời và đáng mua nhất vào thời gian đầu năm mới này.', N'vi-vn-imac-24-inch-2023-4-5k-m3-16gb-slider-2.jpg', 40490000, 0, CAST(N'2024-07-24T15:36:26.493' AS DateTime), CAST(N'2024-07-24T15:36:34.037' AS DateTime), 3)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (16, N'Mac mini M2 2023', N'Mac mini M2 2023 8-core CPU (Z16K) một phiên bản máy tính để bàn hoàn hảo từ nhà Apple với khả năng xử lý đồ hoạ, công việc hoàn hảo ẩn chứa trong ngoại hình nhỏ gọn sang trọng, hứa hẹn sẽ mang đến cho người dùng nhiều trải nghiệm tuyệt vời.', N'mac-mini-m2-2023-11.jpg', 19490000, 0, CAST(N'2024-07-24T15:37:27.113' AS DateTime), CAST(N'2024-07-24T15:37:27.113' AS DateTime), 3)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (17, N'HP Pavilion TP01-4018d', N'Quy tụ mọi yếu tố từ hiệu năng ổn định cho đến thiết kế sang trọng và hiện đại, sản phẩm PC HP Pavilion TP01-4018d i3 13100 (8X3R4PA) thực sự là một sự lựa chọn đáng cân nhắc cho những người đang tìm kiếm một chiếc máy tính để bàn đáng tin cậy và đầy đủ tiện ích.', N'vi-vn-hp-tp014018d-i3-8x3r4pa-2.jpg', 11090000, 0, CAST(N'2024-07-24T15:38:29.267' AS DateTime), CAST(N'2024-07-24T15:38:29.267' AS DateTime), 3)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (18, N'Mac mini M2 2023', N'Kết cấu bền bỉ và nhỏ gọn, Mac mini M2 2023 8-core CPU (Z16K0005Y) lại còn được "gắn" lên thêm nét sang trọng, cao cấp vốn có từ những dòng máy Apple, đi kèm với đó là khả năng xử lý đồ họa và đa nhiệm đỉnh cao với chip Apple M2 hứa hẹn sẽ mang đến cho người dùng những trải nghiệm sử dụng thật tuyệt vời.', N'mac-mini-m2-z16k0005y-acv-5.jpg', 24490000, 0, CAST(N'2024-07-24T15:39:23.090' AS DateTime), CAST(N'2024-07-24T15:39:23.090' AS DateTime), 3)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (19, N'iPad Pro M4 11', N'iPad Pro M4 256GB, dòng máy tính bảng cao cấp nhất năm 2024, nổi bật với hiệu năng vượt trội nhờ chip Apple M4 9 nhân CPU, màn hình Ultra Retina XDR sắc nét và camera trước đặt ở cạnh dài tiện lợi. Với thiết kế mỏng nhẹ và thời lượng pin ấn tượng, iPad Pro M4 256GB hứa hẹn là lựa chọn hoàn hảo cho người dùng yêu thích công nghệ.', N'ipad-pro-m4-11-inch-wifi-256gb-1.jpg', 248490000, 0, CAST(N'2024-07-24T15:41:42.077' AS DateTime), CAST(N'2024-07-24T15:41:42.077' AS DateTime), 4)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (20, N'TCL Tab 10 Gen 2', N'Trong năm 2023, TCL Tab 10 Gen 2 LTE được cho ra mắt, máy đã nhanh chóng chiếm được cảm tình của người dùng với mức giá bán hấp dẫn và nhiều ưu điểm nổi trội. Điểm nhấn của máy là màn hình lớn 10.36 inch, thiết kế mỏng nhẹ với mặt lưng kim loại sang trọng.', N'vi-vn-tcl-tab-10-gen-2-slider--(2).jpg', 3290000, 0, CAST(N'2024-07-24T15:42:24.760' AS DateTime), CAST(N'2024-07-24T15:42:24.760' AS DateTime), 4)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (21, N'iPad Pro M4 13 inch', N'Thiết kế thời thượng, mỏng nhẹ tối ưu, cùng công nghệ OLED hai lớp chất lượng cao, hiệu năng mạnh mẽ từ chip Apple thế hệ mới – tất cả hội tụ trong iPad Pro M4 13 inch Cellular. Đáp ứng hoàn hảo từ công việc đến giải trí, đây chính là lựa chọn xuất sắc cho những ai đam mê sáng tạo và thiết kế.', N'ipad-pro-m4-13-inch-wifi-cellular-256gb638527721204073332.jpg', 43490000, 0, CAST(N'2024-07-24T15:43:59.137' AS DateTime), CAST(N'2024-07-24T15:43:59.137' AS DateTime), 4)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (22, N'Loa Bluetooth JBL Go 3', N'Sang trọng, hiện đại, kích thước nhỏ gọn dễ mang theo. Kết nối nhanh chóng, ổn định, mượt mà với Bluetooth 5.1. Công suất 4.2 W với công nghệ JBL Pro Sound cho âm thanh mạnh mẽ, sống động. Kháng bụi, chống nước chuẩn IP67. Sạc đầy pin trong 2.5 giờ, sử dụng lên đến 5 giờ.', N'bluetooth-jbl-go-3-xanh-den-1-org.jpg', 790000, 0, CAST(N'2024-07-24T15:45:18.247' AS DateTime), CAST(N'2024-07-24T15:45:18.247' AS DateTime), 5)
GO
INSERT [dbo].[product] ([id], [title], [content], [img], [price], [rate], [createAt], [updateAt], [categoryId]) VALUES (23, N'Loa Fenda F670X', N'Sở hữu kiểu dáng mạnh mẽ, gam màu sang trọng, âm thanh sống động, hiệu ứng đèn LED RGB đẹp mắt, kết nối không dây nhanh chóng mang đến cho bạn những trải nghiệm tuyệt vời.', N'loa-vi-tinh-2-1-fenda-f670x-3-2.jpg', 1390000, 5, CAST(N'2024-07-24T15:46:15.587' AS DateTime), CAST(N'2024-07-24T15:46:15.587' AS DateTime), 5)
GO
SET IDENTITY_INSERT [dbo].[product] OFF
GO
SET IDENTITY_INSERT [dbo].[review] ON 
GO
INSERT [dbo].[review] ([id], [rate], [message], [createAt], [productId], [customerId]) VALUES (1, 5, N'abc', CAST(N'2026-01-09T14:58:28.077' AS DateTime), 23, 1)
GO
SET IDENTITY_INSERT [dbo].[review] OFF
GO
ALTER TABLE [dbo].[menu] ADD  DEFAULT ((1)) FOR [isVisible]
GO
ALTER TABLE [dbo].[payment] ADD  CONSTRAINT [DF_payment_status]  DEFAULT (N'Đã đặt hàng') FOR [status]
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD FOREIGN KEY([customerId])
REFERENCES [dbo].[customer] ([id])
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD FOREIGN KEY([productId])
REFERENCES [dbo].[product] ([id])
GO
ALTER TABLE [dbo].[payment]  WITH CHECK ADD  CONSTRAINT [FK_payment_cart] FOREIGN KEY([cartId])
REFERENCES [dbo].[cart] ([id])
GO
ALTER TABLE [dbo].[payment] CHECK CONSTRAINT [FK_payment_cart]
GO
ALTER TABLE [dbo].[paymentDetail]  WITH CHECK ADD  CONSTRAINT [FK_paymentDetail_payment] FOREIGN KEY([paymentId])
REFERENCES [dbo].[payment] ([id])
GO
ALTER TABLE [dbo].[paymentDetail] CHECK CONSTRAINT [FK_paymentDetail_payment]
GO
ALTER TABLE [dbo].[paymentDetail]  WITH CHECK ADD  CONSTRAINT [FK_paymentDetail_product] FOREIGN KEY([productId])
REFERENCES [dbo].[product] ([id])
GO
ALTER TABLE [dbo].[paymentDetail] CHECK CONSTRAINT [FK_paymentDetail_product]
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD FOREIGN KEY([categoryId])
REFERENCES [dbo].[category] ([id])
GO
ALTER TABLE [dbo].[review]  WITH CHECK ADD  CONSTRAINT [FK_review_customer] FOREIGN KEY([customerId])
REFERENCES [dbo].[customer] ([id])
GO
ALTER TABLE [dbo].[review] CHECK CONSTRAINT [FK_review_customer]
GO
ALTER TABLE [dbo].[review]  WITH CHECK ADD  CONSTRAINT [FK_review_product] FOREIGN KEY([productId])
REFERENCES [dbo].[product] ([id])
GO
ALTER TABLE [dbo].[review] CHECK CONSTRAINT [FK_review_product]
GO
USE [master]
GO
ALTER DATABASE [MyShop] SET  READ_WRITE 
GO
