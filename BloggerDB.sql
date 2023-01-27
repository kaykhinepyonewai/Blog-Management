USE [master]
GO
/****** Object:  Database [BloggerDB]    Script Date: 1/27/2023 5:00:58 PM ******/
CREATE DATABASE [BloggerDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BloggerDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\BloggerDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BloggerDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\BloggerDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BloggerDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BloggerDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BloggerDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BloggerDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BloggerDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BloggerDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BloggerDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [BloggerDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BloggerDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BloggerDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BloggerDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BloggerDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BloggerDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BloggerDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BloggerDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BloggerDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BloggerDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BloggerDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BloggerDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BloggerDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BloggerDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BloggerDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BloggerDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BloggerDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BloggerDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BloggerDB] SET  MULTI_USER 
GO
ALTER DATABASE [BloggerDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BloggerDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BloggerDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BloggerDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BloggerDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BloggerDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BloggerDB] SET QUERY_STORE = OFF
GO
USE [BloggerDB]
GO
/****** Object:  Table [dbo].[Articles]    Script Date: 1/27/2023 5:00:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articles](
	[ArticleId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Slug] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Excerpt] [nvarchar](max) NOT NULL,
	[Thumbnail] [nvarchar](255) NULL,
	[Status] [nvarchar](50) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[DeleteStatus] [int] NOT NULL,
	[ReportStatus] [int] NULL,
	[ReportId] [int] NULL,
	[ReportAt] [datetime] NULL,
 CONSTRAINT [PK_Articles] PRIMARY KEY CLUSTERED 
(
	[ArticleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 1/27/2023 5:00:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Slug] [nvarchar](255) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 1/27/2023 5:00:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ArticleId] [int] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Likes]    Script Date: 1/27/2023 5:00:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Likes](
	[LikeId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Status] [int] NULL,
	[ArticleId] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Likes] PRIMARY KEY CLUSTERED 
(
	[LikeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Photos]    Script Date: 1/27/2023 5:00:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Photos](
	[PhotoId] [int] IDENTITY(1,1) NOT NULL,
	[ArticleId] [int] NOT NULL,
	[PhotoName] [nvarchar](255) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[DeleteStatus] [int] NOT NULL,
 CONSTRAINT [PK_Photos] PRIMARY KEY CLUSTERED 
(
	[PhotoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reports]    Script Date: 1/27/2023 5:00:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reports](
	[ReportId] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](255) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Reports] PRIMARY KEY CLUSTERED 
(
	[ReportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 1/27/2023 5:00:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Role] [nvarchar](100) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 1/27/2023 5:00:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[SecondName] [nvarchar](50) NULL,
	[Username] [nvarchar](150) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[ConfirmPassword] [nvarchar](255) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Mobile] [nvarchar](50) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedAt] [datetime] NOT NULL,
	[Profile] [nvarchar](255) NULL,
	[Gender] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Articles] ON 

INSERT [dbo].[Articles] ([ArticleId], [UserId], [CategoryId], [Title], [Slug], [Description], [Excerpt], [Thumbnail], [Status], [CreatedAt], [DeleteStatus], [ReportStatus], [ReportId], [ReportAt]) VALUES (2119, 1008, 34, N'Why a High-Fat Diet Could Reduce the Brain’s Ability to Regulate Food Intake', N'why-a-high-fat-diet-could-reduce-the-brains-ability-to-regulate-food-intake', N'&lt;p style=&quot;margin-bottom: 1em; color: rgb(34, 34, 34); font-family: Lato, sans-serif; font-size: 20px; background-color: rgb(255, 255, 255);&quot;&gt;&lt;span style=&quot;font-weight: 700; line-height: 1;&quot;&gt;&lt;em style=&quot;line-height: 1;&quot;&gt;Summary:&amp;nbsp;&lt;/em&gt;&lt;/span&gt;&lt;em style=&quot;line-height: 1;&quot;&gt;Study reports in the short term, astrocytes regulate caloric intake by controlling the signaling pathway between the gut and brain. Eating high-fat or high-calorie diets disrupts this pathway.&lt;/em&gt;&lt;/p&gt;&lt;p style=&quot;margin-bottom: 1em; color: rgb(34, 34, 34); font-family: Lato, sans-serif; font-size: 20px; background-color: rgb(255, 255, 255);&quot;&gt;&lt;span style=&quot;font-weight: 700; line-height: 1;&quot;&gt;&lt;em style=&quot;line-height: 1;&quot;&gt;Source:&amp;nbsp;&lt;/em&gt;&lt;/span&gt;&lt;em style=&quot;line-height: 1;&quot;&gt;The Physiological Society&lt;/em&gt;&lt;/p&gt;&lt;p style=&quot;margin-bottom: 1em; color: rgb(34, 34, 34); font-family: Lato, sans-serif; font-size: 20px; background-color: rgb(255, 255, 255);&quot;&gt;&lt;span style=&quot;font-weight: 700; line-height: 1;&quot;&gt;Regularly eating a high-fat/calorie diet could reduce the brain’s ability to regulate calorie intake.&lt;/span&gt;&lt;/p&gt;&lt;p style=&quot;margin-bottom: 1em; color: rgb(34, 34, 34); font-family: Lato, sans-serif; font-size: 20px; background-color: rgb(255, 255, 255);&quot;&gt;New research in rats published in&amp;nbsp;&lt;em style=&quot;line-height: 1;&quot;&gt;The Journal of Physiology&lt;/em&gt;&amp;nbsp;has found that after short periods of being fed a high-fat/high calorie diet, the brain adapts to react to what is being ingested and reduces the amount of food eaten to balance calorie intake.&lt;/p&gt;&lt;p style=&quot;margin-bottom: 1em; color: rgb(34, 34, 34); font-family: Lato, sans-serif; font-size: 20px; background-color: rgb(255, 255, 255);&quot;&gt;The researchers, from Penn State College of Medicine in the United States, suggest that calorie intake is regulated in the short-term by cells called astrocytes (large star-shaped cells in the brain that regulate many different functions of neurons in the brain) that control the signaling pathway between the brain and the gut. Continuously eating a high-fat/calorie diet seems to disrupt this signaling pathway.&lt;/p&gt;&lt;p style=&quot;margin-bottom: 1em; color: rgb(34, 34, 34); font-family: Lato, sans-serif; font-size: 20px; background-color: rgb(255, 255, 255);&quot;&gt;&lt;img src=&quot;https://neurosciencenews.com/files/2023/01/high-fat-food-regulation-neurosicences-public.jpg&quot; style=&quot;width: 100%;&quot;&gt;&lt;br&gt;&lt;/p&gt;', N'Summary: Study reports in the short term, astrocytes regulate caloric intake by controlling the signaling pathway between the gut and brain. Eating high-fat...', N'../../../Upload/img_highfatfoodregulationneurosicencespublic_1234930371.jpg', N'active', CAST(N'2023-01-27T16:46:59.000' AS DateTime), 0, 0, NULL, NULL)
INSERT [dbo].[Articles] ([ArticleId], [UserId], [CategoryId], [Title], [Slug], [Description], [Excerpt], [Thumbnail], [Status], [CreatedAt], [DeleteStatus], [ReportStatus], [ReportId], [ReportAt]) VALUES (2120, 1008, 34, N'6 daily practices this 82-year-old triathlete and neurosurgeon uses to lengthen his healthspan', N'6-daily-practices-this-82-year-old-triathlete-and-neurosurgeon-uses-to-lengthen-his-healthspan', N'&lt;div data-component=&quot;text-block&quot; class=&quot;ssrcss-11r1m41-RichTextComponentWrapper ep2nwvo0&quot; style=&quot;margin-top: 1rem; margin-bottom: 1rem; font-variant-numeric: inherit; font-variant-east-asian: inherit; font-stretch: inherit; font-size: 16px; line-height: inherit; font-family: ReithSans, Helvetica, Arial, freesans, sans-serif; max-width: 36.25rem; color: rgb(20, 20, 20); background-color: rgb(255, 255, 255);&quot;&gt;&lt;div class=&quot;ssrcss-7uxr49-RichTextContainer e5tfeyi1&quot; style=&quot;font: inherit; overflow-wrap: break-word;&quot;&gt;&lt;p style=&quot;padding-right: 30px; position: relative; color: rgb(62, 72, 85); font-family: Averta, Helvetica, Arial, sans-serif; font-size: 18px;&quot;&gt;&lt;a href=&quot;https://www.cnbc.com/2022/11/18/living-to-90-or-100-longevity-tips-and-life-expectancy-calculator.html?&amp;amp;amp;qsearchterm=age%2090&quot; style=&quot;color: rgb(98, 88, 255); cursor: pointer; text-decoration-line: none;&quot;&gt;Living to 90&lt;/a&gt;&amp;nbsp;is a feat in itself, but imagine being able to compete in triathlons even as you near that distinguished age.&amp;nbsp;&lt;/p&gt;&lt;p style=&quot;padding-right: 30px; position: relative; color: rgb(62, 72, 85); font-family: Averta, Helvetica, Arial, sans-serif; font-size: 18px;&quot;&gt;This is the reality for&amp;nbsp;&lt;a href=&quot;https://www.josephmaroon.com/biography/&quot; target=&quot;_blank&quot; style=&quot;color: rgb(98, 88, 255); cursor: pointer; text-decoration-line: none;&quot;&gt;Dr. Joseph Maroon&lt;/a&gt;, an 82-year-old triathlete who also works as a consultant neurosurgeon for the Pittsburgh Steelers and medical director of the World Wrestling Entertainment (WWE).&lt;/p&gt;&lt;div class=&quot;BoxInline-styles-makeit-container--iJPde  &quot; style=&quot;margin-right: auto; margin-bottom: 20px; margin-left: auto; max-width: 300px;&quot;&gt;&lt;div id=&quot;BoxInline-ArticleBody-5&quot; class=&quot;BoxInline-styles-makeit-container--iJPde&quot; data-module=&quot;mps-slot&quot; style=&quot;margin-right: auto; margin-bottom: 20px; margin-left: auto; max-width: 300px;&quot;&gt;&lt;/div&gt;&lt;/div&gt;&lt;p style=&quot;padding-right: 30px; position: relative; color: rgb(62, 72, 85); font-family: Averta, Helvetica, Arial, sans-serif; font-size: 18px;&quot;&gt;“I tell people my goal in life is to die young as late as possible,” Maroon says. “I’m focused on my healthspan, not so much my lifespan.”&lt;/p&gt;&lt;p style=&quot;padding-right: 30px; position: relative; color: rgb(62, 72, 85); font-family: Averta, Helvetica, Arial, sans-serif; font-size: 18px;&quot;&gt;Maroon credits his value of mind and body connection for his ability to compete in eight Ironman Triathlons, nine marathons and over 70 Olympic-distance triathlon events.&amp;nbsp;&lt;/p&gt;&lt;p style=&quot;padding-right: 30px; position: relative; color: rgb(62, 72, 85); font-family: Averta, Helvetica, Arial, sans-serif; font-size: 18px;&quot;&gt;He experienced a “lifequake,” as he describes it, when his dad passed; it both led him to his lowest point of quitting neurosurgery and flipping burgers at age 40, and his purpose, when the banker who held the mortgage of the burger joint, invited him to go running.&lt;/p&gt;&lt;p style=&quot;padding-right: 30px; position: relative; color: rgb(62, 72, 85); font-family: Averta, Helvetica, Arial, sans-serif; font-size: 18px;&quot;&gt;“I noticed coincidentally with my physical activity, my depression was slowly lifting, my 15-pound weight loss was coming down, my brain was starting to function again and eventually, I was able to get back to neurosurgery,” Maroon says. “The physical activity literally saved my life.”&lt;/p&gt;&lt;p class=&quot;ssrcss-1q0x1qg-Paragraph eq5iqo00&quot; style=&quot;margin-bottom: 0px; font: inherit;&quot;&gt;&lt;span class=&quot;transition-fade-appear-done transition-fade-enter-done&quot; style=&quot;color: rgb(62, 72, 85); font-family: Averta, Helvetica, Arial, sans-serif; font-size: 18px;&quot;&gt;&lt;/span&gt;&lt;/p&gt;&lt;p style=&quot;padding-right: 30px; position: relative; color: rgb(62, 72, 85); font-family: Averta, Helvetica, Arial, sans-serif; font-size: 18px;&quot;&gt;From that point forward, Maroon made some serious changes to his daily habits, which he believes added extra years to both&amp;nbsp;&lt;a href=&quot;https://www.cnbc.com/2022/12/30/healthspan-lifespan-how-does-diet-factor-in.html?&amp;amp;amp;qsearchterm=healthspan&quot; style=&quot;color: rgb(98, 88, 255); cursor: pointer; text-decoration-line: none;&quot;&gt;his “healthspan” and his lifespan&lt;/a&gt;.&lt;/p&gt;&lt;p style=&quot;padding-right: 30px; position: relative; color: rgb(62, 72, 85); font-family: Averta, Helvetica, Arial, sans-serif; font-size: 18px;&quot;&gt;&lt;br&gt;&lt;/p&gt;&lt;/div&gt;&lt;/div&gt;', N'Living to 90 is a feat in itself, but imagine being able to compete in triathlons even as you near that distinguished age. 

This is the reality for...', N'../../../Upload/img_107115544thumb_2_102859590.jpg', N'active', CAST(N'2023-01-27T16:54:04.000' AS DateTime), 0, 0, NULL, NULL)
INSERT [dbo].[Articles] ([ArticleId], [UserId], [CategoryId], [Title], [Slug], [Description], [Excerpt], [Thumbnail], [Status], [CreatedAt], [DeleteStatus], [ReportStatus], [ReportId], [ReportAt]) VALUES (2121, 1009, 33, N'Corning and Samsung confirm Galaxy S23 features Gorilla Glass Victus 2', N'corning-and-samsung-confirm-galaxy-s23-features-gorilla-glass-victus-2', N'&lt;h1 itemprop=&quot;name&quot; class=&quot;mb-2&quot; style=&quot;font-size: 30px; color: rgb(43, 43, 43); font-family: &amp;quot;samsungone 400&amp;quot;; background-color: rgb(255, 255, 255);&quot;&gt;&lt;span style=&quot;font-family: &amp;quot;samsungone 700&amp;quot;;&quot;&gt;Corning and Samsung confirm Galaxy S23 features Gorilla Glass Victus 2&lt;/span&gt;&lt;/h1&gt;&lt;div itemscope=&quot;&quot; itemtype=&quot;https://schema.org/ImageObject&quot; class=&quot;article-image&quot; style=&quot;position: relative; padding-bottom: 358.109px; overflow: hidden; color: rgb(43, 43, 43); font-family: &amp;quot;samsungone 400&amp;quot;; font-size: 16px; background-color: rgb(255, 255, 255);&quot;&gt;&lt;span style=&quot;outline-color: initial; outline-width: initial; height: 358.109px; width: 636.656px;&quot;&gt;&lt;a href=&quot;https://www.sammobile.com/wp-content/uploads/2023/01/Samsung-Galaxy-S23-Series.jpg&quot; itemprop=&quot;url&quot; class=&quot;ari-fancybox&quot; data-fancybox-group=&quot;gallery&quot; data-title=&quot;&quot; data-fancybox=&quot;gallery&quot; style=&quot;color: rgb(43, 43, 43); text-decoration-line: none; outline: none;&quot;&gt;&lt;img src=&quot;https://www.sammobile.com/wp-content/uploads/2023/01/Samsung-Galaxy-S23-Series-1200x675.jpg&quot; width=&quot;640&quot; height=&quot;360&quot; itemprop=&quot;contentUrl&quot; class=&quot;img-fluid img-rounded&quot; alt=&quot;Samsung Galaxy S23 Series&quot; loading=&quot;lazy&quot; decoding=&quot;async&quot; data-src=&quot;https://www.sammobile.com/wp-content/uploads/2023/01/Samsung-Galaxy-S23-Series-1200x675.jpg&quot; data-srcset=&quot;&quot; srcset=&quot;&quot; style=&quot;border-radius: 10px; width: 100%; position: absolute; object-fit: cover;&quot;&gt;&lt;/a&gt;&lt;/span&gt;&lt;/div&gt;&lt;div class=&quot;single-article-details&quot; style=&quot;display: flex; -webkit-box-orient: horizontal; -webkit-box-direction: normal; flex-direction: row; -webkit-box-align: center; align-items: center; -webkit-box-pack: justify; justify-content: space-between; margin-top: 15px; margin-bottom: 15px; color: rgb(43, 43, 43); font-family: &amp;quot;samsungone 400&amp;quot;; font-size: 16px; background-color: rgb(255, 255, 255);&quot;&gt;&lt;div class=&quot;single-article-details__right&quot; title=&quot;Fri, 27 Jan 2023 07:22:11 +0100&quot;&gt;&lt;p style=&quot;margin-bottom: 30px; font-size: 16px; line-height: 26px; background-color: rgb(255, 255, 255);&quot;&gt;Usually, Samsung is the&amp;nbsp;&lt;a href=&quot;https://www.sammobile.com/news/how-durable-is-gorilla-glass-victus-galaxy-note-20-ultra-video/&quot; style=&quot;color: rgb(38, 163, 217); text-decoration-line: none; font-family: &amp;quot;samsungone 700&amp;quot;, sans-serif;&quot;&gt;first in the smartphone industry to incorporate Corning’s new Gorilla Glass&lt;/a&gt;&amp;nbsp;protection. A few months ago,&amp;nbsp;&lt;a href=&quot;https://www.sammobile.com/news/new-gorilla-glass-victus-2-improves-drop-resistance-at-no-cost/&quot; style=&quot;color: rgb(38, 163, 217); text-decoration-line: none; font-family: &amp;quot;samsungone 700&amp;quot;, sans-serif;&quot;&gt;Corning unveiled Gorilla Glass Victus 2&lt;/a&gt;&amp;nbsp;and promised that it would be more shatter resistant while featuring the same scratch resistance. Now, the&amp;nbsp;&lt;a href=&quot;https://www.corning.com/gorillaglass/worldwide/en/news/news-releases/2023/01/samsung-first-to-use-corning-gorilla-glass-victus-2.html&quot; style=&quot;color: rgb(38, 163, 217); text-decoration-line: none; font-family: &amp;quot;samsungone 700&amp;quot;, sans-serif;&quot;&gt;company has confirmed that Gorilla Glass Victus 2 will first be used&lt;/a&gt;&amp;nbsp;in the next-generation Galaxy smartphone.&lt;/p&gt;&lt;p style=&quot;margin-bottom: 30px; font-size: 16px; line-height: 26px; background-color: rgb(255, 255, 255);&quot;&gt;It means that the&amp;nbsp;&lt;a href=&quot;https://www.sammobile.com/samsung/galaxy-s23/&quot; style=&quot;color: rgb(38, 163, 217); text-decoration-line: none; font-family: &amp;quot;samsungone 700&amp;quot;, sans-serif;&quot;&gt;Galaxy S23&lt;/a&gt;&amp;nbsp;series features Corning’s Gorilla Glass Victus 2 protection on its front (over the screen) and rear. This new protection panel is made using glass composition and offers improved drop performance on rough surfaces like concrete. Gorilla Glass Victus 2 will resist shattering when a phone is dropped from waist height on a concrete surface. It is also claimed to offer shatter resistance from a head-level height on the tarmac.&lt;/p&gt;&lt;/div&gt;&lt;/div&gt;', N'Usually, Samsung is the first in the smartphone industry to incorporate Corning’s new Gorilla Glass protection. A few months ago, Corning unveiled Gorilla...', N'../../../Upload/img_5a03cae86b113fad17cc02acd5768f09_1508413907.jpg', N'active', CAST(N'2023-01-27T16:58:07.000' AS DateTime), 0, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Articles] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryId], [Name], [Slug], [CreatedAt]) VALUES (32, N'Sports', N'sports', CAST(N'2023-01-27T14:39:43.953' AS DateTime))
INSERT [dbo].[Categories] ([CategoryId], [Name], [Slug], [CreatedAt]) VALUES (33, N'Technology', N'technology', CAST(N'2023-01-27T15:17:11.237' AS DateTime))
INSERT [dbo].[Categories] ([CategoryId], [Name], [Slug], [CreatedAt]) VALUES (34, N'Health', N'health', CAST(N'2023-01-27T15:17:25.670' AS DateTime))
INSERT [dbo].[Categories] ([CategoryId], [Name], [Slug], [CreatedAt]) VALUES (35, N'Business', N'business', CAST(N'2023-01-27T15:18:05.423' AS DateTime))
INSERT [dbo].[Categories] ([CategoryId], [Name], [Slug], [CreatedAt]) VALUES (36, N'Entertainment', N'entertainment', CAST(N'2023-01-27T15:18:27.047' AS DateTime))
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Likes] ON 

INSERT [dbo].[Likes] ([LikeId], [UserId], [Status], [ArticleId], [CreatedAt]) VALUES (1200, 1008, NULL, 2119, CAST(N'2023-01-27T16:47:13.000' AS DateTime))
INSERT [dbo].[Likes] ([LikeId], [UserId], [Status], [ArticleId], [CreatedAt]) VALUES (1201, 1008, NULL, 2121, CAST(N'2023-01-27T16:58:45.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Likes] OFF
GO
SET IDENTITY_INSERT [dbo].[Photos] ON 

INSERT [dbo].[Photos] ([PhotoId], [ArticleId], [PhotoName], [CreatedAt], [DeleteStatus]) VALUES (2291, 2121, N'../../../Photos/img_bf88d900a7d1989d0c0ee9694caac66a_1740298190.jpg', CAST(N'2023-01-27T16:58:07.000' AS DateTime), 0)
INSERT [dbo].[Photos] ([PhotoId], [ArticleId], [PhotoName], [CreatedAt], [DeleteStatus]) VALUES (2292, 2121, N'../../../Photos/img_50bb750d0bfb9adfe3adc973cfe97136_1201654545.jpg', CAST(N'2023-01-27T16:58:07.000' AS DateTime), 0)
INSERT [dbo].[Photos] ([PhotoId], [ArticleId], [PhotoName], [CreatedAt], [DeleteStatus]) VALUES (2293, 2121, N'../../../Photos/img_5a03cae86b113fad17cc02acd5768f09_1620810582.jpg', CAST(N'2023-01-27T16:58:08.000' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Photos] OFF
GO
SET IDENTITY_INSERT [dbo].[Reports] ON 

INSERT [dbo].[Reports] ([ReportId], [Message], [CreatedAt]) VALUES (6, N'Harassment', CAST(N'2023-01-25T11:31:32.247' AS DateTime))
INSERT [dbo].[Reports] ([ReportId], [Message], [CreatedAt]) VALUES (9, N'Rules Violation', CAST(N'2023-01-27T15:17:36.007' AS DateTime))
INSERT [dbo].[Reports] ([ReportId], [Message], [CreatedAt]) VALUES (11, N'Spam', CAST(N'2023-01-27T16:40:35.560' AS DateTime))
SET IDENTITY_INSERT [dbo].[Reports] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleId], [Role], [CreatedAt]) VALUES (1, N'Admin', CAST(N'2022-12-29T00:00:00.000' AS DateTime))
INSERT [dbo].[Roles] ([RoleId], [Role], [CreatedAt]) VALUES (2, N'User', CAST(N'2022-12-29T00:00:00.000' AS DateTime))
INSERT [dbo].[Roles] ([RoleId], [Role], [CreatedAt]) VALUES (3, N'Banned', CAST(N'2022-12-29T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [RoleId], [FirstName], [SecondName], [Username], [Email], [Password], [ConfirmPassword], [Address], [Mobile], [CreatedAt], [UpdatedAt], [Profile], [Gender]) VALUES (1008, 1, N'Admin', N'Admin', N'admin', N'admin@gmail.com', N'928C3281148971C0444B50F76C9D1C17236FAB97E4ADCEB1D80D53748341CF8D', N'928C3281148971C0444B50F76C9D1C17236FAB97E4ADCEB1D80D53748341CF8D', N'No(123), 12 street', N'0912345678', CAST(N'2023-01-24T21:34:50.000' AS DateTime), CAST(N'2023-01-24T21:52:38.283' AS DateTime), N'~/img/Dashboard/Profile/img_oslo3_1537763929.png', N'Male')
INSERT [dbo].[Users] ([UserId], [RoleId], [FirstName], [SecondName], [Username], [Email], [Password], [ConfirmPassword], [Address], [Mobile], [CreatedAt], [UpdatedAt], [Profile], [Gender]) VALUES (1009, 2, N'User', N'user', N'user01', N'user@gmail.com', N'6A1032B7316C96BDC5CADCF8BBDE8887DC1173049A2BED33FAE9B625365B8695', N'6A1032B7316C96BDC5CADCF8BBDE8887DC1173049A2BED33FAE9B625365B8695', N'No(123), 12 street', N'0912345678', CAST(N'2023-01-24T21:48:53.000' AS DateTime), CAST(N'2023-01-27T12:01:09.560' AS DateTime), N'~/img/Dashboard/Profile/img_oslo8_2097214810.png', N'Female')
INSERT [dbo].[Users] ([UserId], [RoleId], [FirstName], [SecondName], [Username], [Email], [Password], [ConfirmPassword], [Address], [Mobile], [CreatedAt], [UpdatedAt], [Profile], [Gender]) VALUES (1011, 2, N'adfwfwfe', N'aefwef', N'user', N'user04@gmail.com', N'573BAAF97BAF09386714A1FEC13442AF48650453971ED8BCDDB9BFA73E1049E9', N'573BAAF97BAF09386714A1FEC13442AF48650453971ED8BCDDB9BFA73E1049E9', N'afwefefwf', N'0912345678', CAST(N'2023-01-26T19:17:07.000' AS DateTime), CAST(N'2023-01-26T19:45:27.903' AS DateTime), N'~/img/Dashboard/Profile/Guest.png', N'Male')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Articles] ADD  CONSTRAINT [DF_Articles_Status]  DEFAULT ((0)) FOR [Status]
GO
ALTER TABLE [dbo].[Articles] ADD  CONSTRAINT [DF_Articles_ReportStatus]  DEFAULT ((0)) FOR [ReportStatus]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Profile]  DEFAULT (N'~/img/Dashboard/Profile/Guest.png') FOR [Profile]
GO
ALTER TABLE [dbo].[Articles]  WITH CHECK ADD  CONSTRAINT [FK_Articles_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
GO
ALTER TABLE [dbo].[Articles] CHECK CONSTRAINT [FK_Articles_Categories]
GO
ALTER TABLE [dbo].[Articles]  WITH CHECK ADD  CONSTRAINT [FK_Articles_Reports] FOREIGN KEY([ReportId])
REFERENCES [dbo].[Reports] ([ReportId])
GO
ALTER TABLE [dbo].[Articles] CHECK CONSTRAINT [FK_Articles_Reports]
GO
ALTER TABLE [dbo].[Articles]  WITH CHECK ADD  CONSTRAINT [FK_Articles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Articles] CHECK CONSTRAINT [FK_Articles_Users]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([ArticleId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Articles]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users]
GO
ALTER TABLE [dbo].[Likes]  WITH CHECK ADD  CONSTRAINT [FK_Likes_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([ArticleId])
GO
ALTER TABLE [dbo].[Likes] CHECK CONSTRAINT [FK_Likes_Articles]
GO
ALTER TABLE [dbo].[Likes]  WITH CHECK ADD  CONSTRAINT [FK_Likes_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Likes] CHECK CONSTRAINT [FK_Likes_Users]
GO
ALTER TABLE [dbo].[Photos]  WITH CHECK ADD  CONSTRAINT [FK_Photos_Articles] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Articles] ([ArticleId])
GO
ALTER TABLE [dbo].[Photos] CHECK CONSTRAINT [FK_Photos_Articles]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
USE [master]
GO
ALTER DATABASE [BloggerDB] SET  READ_WRITE 
GO
