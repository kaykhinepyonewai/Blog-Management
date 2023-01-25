USE [master]
GO
/****** Object:  Database [BloggerDB]    Script Date: 1/25/2023 11:37:27 AM ******/
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
/****** Object:  Table [dbo].[Articles]    Script Date: 1/25/2023 11:37:28 AM ******/
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
/****** Object:  Table [dbo].[Categories]    Script Date: 1/25/2023 11:37:28 AM ******/
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
/****** Object:  Table [dbo].[Comments]    Script Date: 1/25/2023 11:37:28 AM ******/
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
/****** Object:  Table [dbo].[Likes]    Script Date: 1/25/2023 11:37:28 AM ******/
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
/****** Object:  Table [dbo].[Photos]    Script Date: 1/25/2023 11:37:28 AM ******/
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
/****** Object:  Table [dbo].[Reports]    Script Date: 1/25/2023 11:37:28 AM ******/
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
/****** Object:  Table [dbo].[Roles]    Script Date: 1/25/2023 11:37:28 AM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 1/25/2023 11:37:28 AM ******/
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

INSERT [dbo].[Articles] ([ArticleId], [UserId], [CategoryId], [Title], [Slug], [Description], [Excerpt], [Thumbnail], [Status], [CreatedAt], [DeleteStatus], [ReportStatus], [ReportId], [ReportAt]) VALUES (2089, 1008, 28, N'Samsung’s new 120Hz OLED display for laptops could be used in Galaxy Book 3 Ultra', N'samsungs-new-120hz-oled-display-for-laptops-could-be-used-in-galaxy-book-3-ultra', N'While the company didn’t explicitly mention which Galaxy Book will use this new 16-inch OLED panel, it sits well with the Galaxy Book 3 Ultra whose specifications were leaked a few days ago. Samsung Display plans to develop more large-sized OCTA (On-Cell Touch AMOLED) panels for laptops because more laptop brands are equipping newer models with OLED panels.

Hojung Lee, Head of Product Planning Team for Small and Medium-sized Display Division at Samsung Display, said, “The difficulty level of the touch-integrated technology surges greatly as the display area increases, which leads to the increased number of touch sensors required. We were able to achieve the natural feel and touch responsiveness for larger displays through the development of new materials and process technologies', N'While the company didn’t explicitly mention which Galaxy Book will use this new 16-inch OLED panel, it sits well with the Galaxy Book 3 Ultra whose specifications...', N'../../../Upload/img_30090be6ea64e415b0286b4cfaf28a0f_1305631520.jpg', N'active', CAST(N'2023-01-24T22:10:16.000' AS DateTime), 0, 0, NULL, NULL)
INSERT [dbo].[Articles] ([ArticleId], [UserId], [CategoryId], [Title], [Slug], [Description], [Excerpt], [Thumbnail], [Status], [CreatedAt], [DeleteStatus], [ReportStatus], [ReportId], [ReportAt]) VALUES (2090, 1008, 28, N'M2 Pro and M2 Max MacBook Pros Feature Faster SSD Write Speeds, Tests Show', N'm2-pro-and-m2-max-macbook-pros-feature-faster-ssd-write-speeds-tests-show', N'This week, select reviewers and media outlets had the chance to review the new MacBook Pros ahead of their availability starting Tuesday, January 24. As part of their reviews, Macworld and Tom&#39;s Guide tested the SSD read and write speeds of the M2 Pro and M2 Max chips in Apple&#39;s latest laptops.

When Macworld tested with the Blackmagic disk speed app, the 16-inch MacBook Pro with M2 Pro in a 2TB storage configuration achieved a read speed of 5,372 MB/s and a write speed of 6,491 MB/s. The previous-generation 14-inch MacBook Pro with an M1 Pro chip and 1TB of storage scored a slightly higher 5,797 MB/s read speed than the 16-inch M2 Pro; however, it scored a lower write speed of 5,321 MB/s.', N'This week, select reviewers and media outlets had the chance to review the new MacBook Pros ahead of their availability starting Tuesday, January 24. As...', N'../../../Upload/img_7a8457168ae8ed6720175b88cdaf1219_1757815257.jpg', N'active', CAST(N'2023-01-24T22:16:46.000' AS DateTime), 0, 0, NULL, NULL)
INSERT [dbo].[Articles] ([ArticleId], [UserId], [CategoryId], [Title], [Slug], [Description], [Excerpt], [Thumbnail], [Status], [CreatedAt], [DeleteStatus], [ReportStatus], [ReportId], [ReportAt]) VALUES (2091, 1009, 26, N'Person tests positive for tuberculosis at Penn Wood Middle School in Delaware County', N'person-tests-positive-for-tuberculosis-at-penn-wood-middle-school-in-delaware-county', N'DARBY, Pennsylvania (WPVI) -- The Delaware County Health Department says a person at Penn Wood Middle School in the William Penn School District has tested positive for tuberculosis.

Representatives are working to identify students and staff of the Darby school who might have been exposed to the illness.

The health department stresses that students and staff can continue active learning in the school setting because TB is not as contagious as the flu and requires extended close contact (around at least 15 hours of contact per week) to spread.

&quot;The important first step is to identify close contacts of the person who was infected. Those identified as close contacts are being notified by the Health Department for testing arrangements,&quot; the DCHD said in a statement released Monday.

Parents with concerns should call the Delaware County Health Department Wellness Line, available Monday through Friday, from 8:30 a.m. to 4:30 p.m., by phone at (484) 276 - 2100 or by email at DelcoWellness@co.delaware.pa.us.

Here is more information on TB from the CDC:

TB bacteria spread through the air from one person to another. When a person with TB disease of the lungs or throat coughs, speaks, or sings, TB bacteria can get into the air. People nearby may breathe in these bacteria and become infected.

TB is NOT spread by:

- shaking someone&#39;s hand

- sharing food or drink

- touching bed linens or toilet seats

- sharing toothbrushes

- kissing

When a person breathes in TB bacteria, the bacteria can settle in the lungs and begin to grow. From there, they can move through the blood to other parts of the body, such as the kidney, spine, and brain.

TB disease in the lungs or throat can be infectious. This means that the bacteria can spread to other people. TB in other parts of the body, such as the kidney or spine, is usually not infectious.

People with TB disease are most likely to spread it to people they spend time with every day. This includes family members, friends, and coworkers or schoolmates.', N'DARBY, Pennsylvania (WPVI) -- The Delaware County Health Department says a person at Penn Wood Middle School in the William Penn School District has tested...', N'../../../Upload/img_markusfrieauffij0kixl4uysunsplash_2038454018.jpg', N'active', CAST(N'2023-01-24T22:52:11.000' AS DateTime), 0, 0, NULL, NULL)
INSERT [dbo].[Articles] ([ArticleId], [UserId], [CategoryId], [Title], [Slug], [Description], [Excerpt], [Thumbnail], [Status], [CreatedAt], [DeleteStatus], [ReportStatus], [ReportId], [ReportAt]) VALUES (2096, 1009, 27, N'DeSantis attacked as ''authoritarian'' for saying teachers should take away cellphones during class', N'desantis-attacked-as-authoritarian-for-saying-teachers-should-take-away-cellphones-during-class', N'MSNBC columnist and New York University professor Ruth Ben-Ghiat was mocked for attacking Gov. Ron DeSantis, R-Fla., as a dangerous authoritarian for suggesting that teachers should confiscate phones during class.

DeSantis announced new measures on Monday to combat critical race theory in education while also protecting educators’ rights to push back against unions. Among one of his comments also included efforts by school boards to improve education, such as tackling the use of phones during class.

&quot;I think to myself, ‘Why are these kids on their phones during class all the time?’ I mean, I think a school district would be totally within their rights to say, ‘You know what, leave your phone in some cubby or something, go sit in class and learn, and if you get it at recess and you want to text people, fine.’ But they should not be always on their phones being distracted from the lesson. So, I think that our school boards will be able to lean in on some stuff, too, to buttress what we’re doing. And I look forward to being able to do that,&quot; DeSantis said. The clip spread on Twitter with Ben-Ghiat claiming that this was another example of DeSantis pushing an &quot;authoritarian&quot; mindset.

&quot;He is so dangerous in every way. Truly an authoritarian personality,&quot; Ben-Ghiat tweeted.

Many Twitter users mocked the comment for resembling a liberal parody as well as suggesting students without phones in class is akin to authoritarianism.

National Review senior writer Dan McLaughlin wrote, &quot;Apparently, teachers controlling their own classrooms is now authoritarianism.&quot;

The Spectator contributing editor Stephen Miller joked, &quot;’Students shouldn&#39;t have cell phones and be on TikTok in the classroom.’ ‘He&#39;s Hitler.’&quot;

&quot;Next thing you know these Nazis will ask kids not to chew gum in class,&quot; Federalist senior editor David Harsanyi tweeted.


&quot;I thought this was satire. I was wrong,&quot; podcast host Chrissy Clark commented.

Radio host Paul Zeise wrote, &quot;Imagine being so beholden to your political philosophy that you are shrieking about the radical MAGA crazy idea that kids shouldn&#39;t have their cell phones out in classrooms! Oh the horror! Kids should be made to pay attention in class!! These people are making themselves a parody.&quot;

&quot;What is wrong with you?&quot; Lafayette Co. president Ellen Carmichael asked.

Florida Gov. Ron DeSantis
Florida Gov. Ron DeSantis (AP Photo / Lynne Sladky / File)

‘LGBTQ-FRIENDLY’ COLLEGE STUDENTS WORRIED IT WILL BE DOMINATED BY CONSERVATIVES: ‘DESANTIS UNIVERSITY’ 

After several responses, Ben-Ghiat followed up her tweet with another attack against DeSantis and a defense of her comment.

&quot;It is not the phones, people. It is the arrogance of a man who wants to control everyone and everything: businesses, students, other poiticians [sic] He wants to dictate what you learn and read, how you train your employees, and much more,&quot; Ben-Ghiat wrote.

DeSantis’ comments came after the Florida Department of Education rejected an Advanced Placement (AP) African-American studies course after it was revealed that it promoted elements of critical race theory as well as &quot;Black queer studies.&quot;
 ‘LGBTQ-FRIENDLY’ COLLEGE STUDENTS WORRIED IT WILL BE DOMINATED BY CONSERVATIVES: ‘DESANTIS UNIVERSITY’ 

After several responses, Ben-Ghiat followed up her tweet with another attack against DeSantis and a defense of her comment.

&quot;It is not the phones, people. It is the arrogance of a man who wants to control everyone and everything: businesses, students, other poiticians [sic] He wants to dictate what you learn and read, how you train your employees, and much more,&quot; Ben-Ghiat wrote.

DeSantis’ comments came after the Florida Department of Education rejected an Advanced Placement (AP) African-American studies course after it was revealed that it promoted elements of critical race theory as well as &quot;Black queer studies.&quot;', N'MSNBC columnist and New York University professor Ruth Ben-Ghiat was mocked for attacking Gov. Ron DeSantis, R-Fla., as a dangerous authoritarian for suggesting...', N'../../../Upload/img_gettyimages1244625128_751785761.jpg', N'active', CAST(N'2023-01-25T09:11:56.000' AS DateTime), 0, 0, NULL, NULL)
INSERT [dbo].[Articles] ([ArticleId], [UserId], [CategoryId], [Title], [Slug], [Description], [Excerpt], [Thumbnail], [Status], [CreatedAt], [DeleteStatus], [ReportStatus], [ReportId], [ReportAt]) VALUES (2097, 1010, 29, N'Effects of ChatGPT and Natural Language Technology on Programmers', N'effects-of-chatgpt-and-natural-language-technology-on-programmers', N'If you are a software developer or a computer programmer then you may have been alarmed by the demonstrated skills of the trending Chatbot called ChatGPT developed by OpenAI. The ChatGPT is a powerful AI bot that engages in a human-like dialogue based on a prompt.

ChatGPT and natural language technology are aligned hand in hand as natural language technology is the branch of AI by which ChatGPT has been developed. In this article, we will discuss the effects of ChatGPT on programmers and we also going to discuss the effects of natural language technology on programmers.

Just before Christmas, ChatGPT was released in a public beta. It is a chatbot built on the GPT-3 Large Language Model (LLM) that uses generative AI and natural language processing (NLP) to produce text that is nearly indistinguishable from human-written text. It quickly went viral due to its impressive abilities, and it now has millions of users.

You get to tell the chatbot to write a Shakespearean poem about trees or an article about the applications of AI in the IT industry the AI chatbot will write for you.

What has surprised many software developers is that it can also be used to generate computer code. You just need to instruct the ChatGPT what to do, and it will gladly create websites, applications, and even simple games in a variety of programming languages. Some of the most commonly used languages for software development are Python, C, and Javascript.

While copywriters, novelists, and journalists are confident that, with the impressive results of ChatGPT they are confident that it is not yet at the point where they should be concerned about their jobs. The results of the prose text the ChatGPT produces are not very personal and are prone to factual errors. The Chatbot does not consider whether its output is interesting, amusing, frightening, or capable of evoking any other emotion an author might want to convey. All of these elements are necessary if you want your writing to be engaging to readers.

When writing code, however, that is irrelevant; what is important is that the program you create does what it is supposed to do. It either works or it doesn’t. Interpreters that run human (or machine) generated code as applications will not stop reading it halfway because it is uninteresting!

Are ChatGPT and NLP Putting Programming and Software Development Jobs at Risk?
Despite this, it appears that ChatGPT and other NLP technologies available today will not render all programmers, coders, and software developers obsolete overnight.

ChatGPT will only create simple programs. If you ask the chatbot and natural language technology to do something complicated, such as a sophisticated game or business application, they will admit their weakness and tell you that the task is currently beyond their capabilities.

Computers, for example, cannot yet tell us what types of code or applications are required to achieve our goals. Even if it knows this because we tell it, ChatGPT, in particular, cannot (for the time being) attempt to create software that specifically gives us as users a competitive advantage over users of other software.

For example, we can’t tell ChatGPT to make an e-commerce platform that sells more than Amazon. If that’s what we want, we’ll still have to spend effort and time figuring out what it is about Amazon’s platform that makes it so great, and then figuring out how to improve it.

As a result, ChatGPT (and other current NLP-based tools) are still limited in their ability to create software that gives us a competitive advantage or competes with human creativity and Genuity.

One caveat here is that, while we can do our best to predict what will happen in the future, no one has a crystal ball. Many people who were used to AIs conversing at the level of Alexa or Siri were taken aback by how good ChatGPT is.

Future developments may accelerate our progress toward a point where human programmers – or many other types of professionals – are no longer required. For the time being, we can assume that a wide range of skills are still required to develop software that computers are unlikely to be able to replicate any time soon.

', N'If you are a software developer or a computer programmer then you may have been alarmed by the demonstrated skills of the trending Chatbot called ChatGPT...', N'../../../Upload/img_10effectsofchatgptandnaturallanguagetechnologyonprogrammers_1335729104.jpg', N'active', CAST(N'2023-01-25T09:36:30.000' AS DateTime), 0, 0, NULL, NULL)
INSERT [dbo].[Articles] ([ArticleId], [UserId], [CategoryId], [Title], [Slug], [Description], [Excerpt], [Thumbnail], [Status], [CreatedAt], [DeleteStatus], [ReportStatus], [ReportId], [ReportAt]) VALUES (2098, 1010, 26, N'Here’s how gardening can improve your health', N'heres-how-gardening-can-improve-your-health', N'Those who garden look forward to the season of seed packets and plantings, careful tending and abundant harvests. But research indicates another reason to eagerly anticipate gardening: improving your health.

A study in the journal the Lancet Planetary Health found that people who participate in community gardening programs eat more fiber and get more physical activity than their counterparts who don’t garden. Both of these factors are associated with better health.

Though research on gardening abounds, the researchers wrote that they were able to find only three other studies that tested gardening’s effects on disease risk factors by assigning participants randomly to groups who did and didn’t garden, then comparing their health.

In this case, the researchers ran a study at 37 community gardens in Denver and Aurora, Colo. After raising awareness of the program in a variety of neighborhoods, they recruited those on the waiting lists for the study. All 291 participants were adults and had not gardened within the last two years. More than half were from low-income households.


The group assigned to garden was provided with a garden plot, seeds, seedlings and an introduction to gardening course. Those assigned to the non-gardening group were offered the same deal during the next gardening season. Participants were all given health surveys that looked at such factors as body weight, waist circumference, physical activity and diet.

During the study, researchers found, those who gardened ate more fruit and vegetables than their counterparts, increasing their consumption by about 1.13 servings per day. They consumed 1.4 grams more fiber a day than the control group, and increased their fiber intake by 7 percent over the course of the program. They were slightly more active, too, increasing their moderate to vigorous physical activity during the study period. Gardeners also reported less stress and anxiety than their non-gardening counterparts.

Though the gains were modest, researchers said that they are the types of small changes recommended by experts as a way to prevent the risk of chronic diseases. Smoking, poor diet and a sedentary lifestyle all contribute to that risk.', N'Those who garden look forward to the season of seed packets and plantings, careful tending and abundant harvests. But research indicates another reason...', N'../../../Upload/img_crystaljopkctua_craounsplash_1369444224.jpg', N'active', CAST(N'2023-01-25T09:50:57.000' AS DateTime), 0, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Articles] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryId], [Name], [Slug], [CreatedAt]) VALUES (26, N'Health', N'health', CAST(N'2023-01-24T21:50:33.333' AS DateTime))
INSERT [dbo].[Categories] ([CategoryId], [Name], [Slug], [CreatedAt]) VALUES (27, N'Education', N'education', CAST(N'2023-01-24T21:50:44.403' AS DateTime))
INSERT [dbo].[Categories] ([CategoryId], [Name], [Slug], [CreatedAt]) VALUES (28, N'Technology', N'technology', CAST(N'2023-01-24T21:50:57.063' AS DateTime))
INSERT [dbo].[Categories] ([CategoryId], [Name], [Slug], [CreatedAt]) VALUES (29, N'Language', N'language', CAST(N'2023-01-24T21:51:11.970' AS DateTime))
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Comments] ON 

INSERT [dbo].[Comments] ([CommentId], [UserId], [ArticleId], [Message], [CreatedAt], [UpdatedAt]) VALUES (9, 1010, 2089, N'GOAT!!', CAST(N'2023-01-25T10:38:16.447' AS DateTime), CAST(N'2023-01-25T10:38:16.447' AS DateTime))
SET IDENTITY_INSERT [dbo].[Comments] OFF
GO
SET IDENTITY_INSERT [dbo].[Likes] ON 

INSERT [dbo].[Likes] ([LikeId], [UserId], [Status], [ArticleId], [CreatedAt]) VALUES (1045, 1008, NULL, 2089, CAST(N'2023-01-25T07:54:49.000' AS DateTime))
INSERT [dbo].[Likes] ([LikeId], [UserId], [Status], [ArticleId], [CreatedAt]) VALUES (1047, 1008, NULL, 2096, CAST(N'2023-01-25T09:16:18.000' AS DateTime))
INSERT [dbo].[Likes] ([LikeId], [UserId], [Status], [ArticleId], [CreatedAt]) VALUES (1048, 1010, NULL, 2089, CAST(N'2023-01-25T10:37:45.000' AS DateTime))
INSERT [dbo].[Likes] ([LikeId], [UserId], [Status], [ArticleId], [CreatedAt]) VALUES (1049, 1010, NULL, 2090, CAST(N'2023-01-25T10:38:00.000' AS DateTime))
INSERT [dbo].[Likes] ([LikeId], [UserId], [Status], [ArticleId], [CreatedAt]) VALUES (1050, 1008, NULL, 2090, CAST(N'2023-01-25T10:39:09.000' AS DateTime))
INSERT [dbo].[Likes] ([LikeId], [UserId], [Status], [ArticleId], [CreatedAt]) VALUES (1051, 1008, NULL, 2091, CAST(N'2023-01-25T11:33:37.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Likes] OFF
GO
SET IDENTITY_INSERT [dbo].[Photos] ON 

INSERT [dbo].[Photos] ([PhotoId], [ArticleId], [PhotoName], [CreatedAt], [DeleteStatus]) VALUES (2254, 2089, N'../../../Photos/img_866b06e8c1390090fcb2632150e37eae_1956671840.jpg', CAST(N'2023-01-24T22:10:16.000' AS DateTime), 0)
INSERT [dbo].[Photos] ([PhotoId], [ArticleId], [PhotoName], [CreatedAt], [DeleteStatus]) VALUES (2255, 2089, N'../../../Photos/img_30090be6ea64e415b0286b4cfaf28a0f_228344230.jpg', CAST(N'2023-01-24T22:10:16.000' AS DateTime), 0)
INSERT [dbo].[Photos] ([PhotoId], [ArticleId], [PhotoName], [CreatedAt], [DeleteStatus]) VALUES (2256, 2089, N'../../../Photos/img_521977b10ce0babd22fbd9aa882ea1a2_2024455986.jpg', CAST(N'2023-01-24T22:10:16.000' AS DateTime), 0)
INSERT [dbo].[Photos] ([PhotoId], [ArticleId], [PhotoName], [CreatedAt], [DeleteStatus]) VALUES (2257, 2090, N'../../../Photos/img_elnazasadi4rt_gbhtcdcunsplash_800015575.jpg', CAST(N'2023-01-24T22:16:46.000' AS DateTime), 0)
INSERT [dbo].[Photos] ([PhotoId], [ArticleId], [PhotoName], [CreatedAt], [DeleteStatus]) VALUES (2258, 2090, N'../../../Photos/img_tomasstanislavsky9prq44avkikunsplash_1219171612.jpg', CAST(N'2023-01-24T22:16:46.000' AS DateTime), 0)
INSERT [dbo].[Photos] ([PhotoId], [ArticleId], [PhotoName], [CreatedAt], [DeleteStatus]) VALUES (2259, 2090, N'../../../Photos/img_alesnesetrilim7lzjxelhgunsplash_1989699540.jpg', CAST(N'2023-01-24T22:16:46.000' AS DateTime), 0)
INSERT [dbo].[Photos] ([PhotoId], [ArticleId], [PhotoName], [CreatedAt], [DeleteStatus]) VALUES (2271, 2091, N'../../../Photos/img_markusfrieauffij0kixl4uysunsplash_2011747841.jpg', CAST(N'2023-01-25T08:52:51.000' AS DateTime), 0)
INSERT [dbo].[Photos] ([PhotoId], [ArticleId], [PhotoName], [CreatedAt], [DeleteStatus]) VALUES (2272, 2091, N'../../../Photos/img_izzyparkkotplk22w0wunsplash_1053948159.jpg', CAST(N'2023-01-25T08:52:51.000' AS DateTime), 0)
INSERT [dbo].[Photos] ([PhotoId], [ArticleId], [PhotoName], [CreatedAt], [DeleteStatus]) VALUES (2273, 2091, N'../../../Photos/img_kellysikkemar2htbxekgwqunsplash_1892260233.jpg', CAST(N'2023-01-25T08:52:52.000' AS DateTime), 0)
INSERT [dbo].[Photos] ([PhotoId], [ArticleId], [PhotoName], [CreatedAt], [DeleteStatus]) VALUES (2274, 2096, N'../../../Photos/img_desantisproposesteachersbillofrightstocombatwokecurriculum_213142116.jpg', CAST(N'2023-01-25T09:11:56.000' AS DateTime), 0)
INSERT [dbo].[Photos] ([PhotoId], [ArticleId], [PhotoName], [CreatedAt], [DeleteStatus]) VALUES (2275, 2096, N'../../../Photos/img_floridagovrondesantis_1754197972.jpg', CAST(N'2023-01-25T09:11:56.000' AS DateTime), 0)
INSERT [dbo].[Photos] ([PhotoId], [ArticleId], [PhotoName], [CreatedAt], [DeleteStatus]) VALUES (2276, 2096, N'../../../Photos/img_gettyimages1244625128_25870362.jpg', CAST(N'2023-01-25T09:11:56.000' AS DateTime), 0)
INSERT [dbo].[Photos] ([PhotoId], [ArticleId], [PhotoName], [CreatedAt], [DeleteStatus]) VALUES (2277, 2091, N'../../../Photos/img_3ddd4_1494021392.png', CAST(N'2023-01-25T10:25:59.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Photos] OFF
GO
SET IDENTITY_INSERT [dbo].[Reports] ON 

INSERT [dbo].[Reports] ([ReportId], [Message], [CreatedAt]) VALUES (5, N'Spam', CAST(N'2023-01-25T11:31:18.557' AS DateTime))
INSERT [dbo].[Reports] ([ReportId], [Message], [CreatedAt]) VALUES (6, N'Harassment', CAST(N'2023-01-25T11:31:32.247' AS DateTime))
INSERT [dbo].[Reports] ([ReportId], [Message], [CreatedAt]) VALUES (7, N'Rules Violation', CAST(N'2023-01-25T11:31:50.347' AS DateTime))
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
INSERT [dbo].[Users] ([UserId], [RoleId], [FirstName], [SecondName], [Username], [Email], [Password], [ConfirmPassword], [Address], [Mobile], [CreatedAt], [UpdatedAt], [Profile], [Gender]) VALUES (1009, 2, N'User', N'user', N'user01', N'user@gmail.com', N'6A1032B7316C96BDC5CADCF8BBDE8887DC1173049A2BED33FAE9B625365B8695', N'6A1032B7316C96BDC5CADCF8BBDE8887DC1173049A2BED33FAE9B625365B8695', N'No(123), 12 street', N'0912345678', CAST(N'2023-01-24T21:48:53.000' AS DateTime), CAST(N'2023-01-24T21:53:50.580' AS DateTime), N'~/img/Dashboard/Profile/img_oslo8_2097214810.png', N'Female')
INSERT [dbo].[Users] ([UserId], [RoleId], [FirstName], [SecondName], [Username], [Email], [Password], [ConfirmPassword], [Address], [Mobile], [CreatedAt], [UpdatedAt], [Profile], [Gender]) VALUES (1010, 2, N'User02', N'User02', N'user02', N'user02@gmail.com', N'0667AD0763572ED3AE722B150E76D705B72D2219C2DC9ECFB2FC6DBE1CCDE79B', N'0667AD0763572ED3AE722B150E76D705B72D2219C2DC9ECFB2FC6DBE1CCDE79B', N'No(123),12street', N'0912345678', CAST(N'2023-01-25T09:32:57.000' AS DateTime), CAST(N'2023-01-25T09:38:44.223' AS DateTime), N'~/img/Dashboard/Profile/img_oslo7_1625526231.png', N'Male')
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
