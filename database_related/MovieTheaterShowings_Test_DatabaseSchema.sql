USE [master]
GO  

/****** Object:  Database [MovieTheaterShowings_Test]    Script Date: 6/23/2017 8:59:21 AM ******/
CREATE DATABASE [MovieTheaterShowings_Test]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MovieTheaterShowings_Test', FILENAME = N'C:\Users\jeremy.brammer\Documents\Visual Studio 2013\Projects\MovieTheaterShowings\database_related\file_databases\MovieTheaterShowings_Test.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MovieTheaterShowings_Test_log', FILENAME = N'C:\Users\jeremy.brammer\Documents\Visual Studio 2013\Projects\MovieTheaterShowings\database_related\file_databases\MovieTheaterShowings_Test_log.ldf' , SIZE = 1280KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET COMPATIBILITY_LEVEL = 110
GO

ALTER DATABASE [MovieTheaterShowings_Test] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET ARITHABORT OFF 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET  MULTI_USER 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET DELAYED_DURABILITY = DISABLED 
GO
USE [MovieTheaterShowings_Test]
GO
/****** Object:  Table [dbo].[LinkerUserToRoles]    Script Date: 6/23/2017 8:59:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LinkerUserToRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_LinkerUserToRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Movies]    Script Date: 6/23/2017 8:59:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Movies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](75) NOT NULL,
	[RunTime] [int] NOT NULL,
	[Image] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Movies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 6/23/2017 8:59:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ShowTimes]    Script Date: 6/23/2017 8:59:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShowTimes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShowingDateTime] [datetime] NOT NULL,
	[MovieId] [int] NOT NULL,
 CONSTRAINT [PK_ShowTimes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/23/2017 8:59:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[password_hash] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [unique_Movie_title]    Script Date: 6/23/2017 8:59:22 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [unique_Movie_title] ON [dbo].[Movies]
(
	[Title] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Unique_Username_Users]    Script Date: 6/23/2017 8:59:22 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [Unique_Username_Users] ON [dbo].[Users]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LinkerUserToRoles]  WITH CHECK ADD  CONSTRAINT [FK_LinkerUserToRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LinkerUserToRoles] CHECK CONSTRAINT [FK_LinkerUserToRoles_Roles]
GO
ALTER TABLE [dbo].[LinkerUserToRoles]  WITH CHECK ADD  CONSTRAINT [FK_LinkerUserToRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LinkerUserToRoles] CHECK CONSTRAINT [FK_LinkerUserToRoles_Users]
GO
ALTER TABLE [dbo].[ShowTimes]  WITH CHECK ADD  CONSTRAINT [FK_ShowTimes_Movies] FOREIGN KEY([MovieId])
REFERENCES [dbo].[Movies] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ShowTimes] CHECK CONSTRAINT [FK_ShowTimes_Movies]
GO
ALTER TABLE [dbo].[Movies]  WITH CHECK ADD  CONSTRAINT [CK_Movies_TitleHasLength] CHECK  (([Title] IS NULL OR [Title]<>''))
GO
ALTER TABLE [dbo].[Movies] CHECK CONSTRAINT [CK_Movies_TitleHasLength]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [CK_Users_NameHasLength] CHECK  (([Name] IS NULL OR [Name]<>''))
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [CK_Users_NameHasLength]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [CK_Users_password_hashHasLength] CHECK  (([password_hash] IS NULL OR [password_hash]<>''))
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [CK_Users_password_hashHasLength]
GO
/****** Object:  StoredProcedure [dbo].[delete_movie]    Script Date: 6/23/2017 8:59:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[delete_movie]
	@MovieID int,
	@RowsAffected int OUTPUT
AS
BEGIN
	DELETE TOP(1)
	FROM [Movies]
	WHERE Id = @MovieID
END

SELECT @RowsAffected = (SELECT @@ROWCOUNT)




GO
/****** Object:  StoredProcedure [dbo].[insert_Movie]    Script Date: 6/23/2017 8:59:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[insert_Movie]
		@Title varchar(75),	
		@RunTime int,	
		@Image varchar(MAX),
		-- This is a variable to return that will have the newly inserted MovieId.
		@IdentityOutput int OUTPUT
	AS
	BEGIN
		INSERT INTO [Movies]
		(
			Title,
			RunTime,
			Image
		)
		VALUES
		(
			@Title,
			@RunTime,
			@Image
		)
		SELECT @IdentityOutput = SCOPE_IDENTITY()
		
	END




GO
/****** Object:  StoredProcedure [dbo].[insert_ShowTime]    Script Date: 6/23/2017 8:59:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE PROCEDURE [dbo].[insert_ShowTime]
		@ShowingDateTime datetime,	
		@MovieId int,	
		-- This is a variable to return that will have the newly inserted MovieId.
		@IdentityOutput int OUTPUT
	AS
	BEGIN
		INSERT INTO [ShowTimes]
		(
			ShowingDateTime,
			MovieId
		)
		VALUES
		(
			@ShowingDateTime,
			@MovieId
		)
		SELECT @IdentityOutput = SCOPE_IDENTITY()
		
	END




GO
/****** Object:  StoredProcedure [dbo].[insert_User]    Script Date: 6/23/2017 8:59:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


	CREATE PROCEDURE [dbo].[insert_User]
		@Name varchar(50),	
		@Role varchar(50),	
		@password_hash varchar(255),
		-- An output variable to return the RoleId.
		@RoleIDOutput int OUTPUT,
		-- This is a variable to return that will have the newly inserted UserId.
		@IdentityOutput int OUTPUT
	AS
	BEGIN
		INSERT INTO [Users]
		(
			Name,
			password_hash
		)
		VALUES
		(
			@Name,
			@password_hash
		)
		SELECT @IdentityOutput = SCOPE_IDENTITY()
		-- Try to find the user's role, and insert that relationship in the linker table.
		--DECLARE @RoleID int -- create a variable
		SET @RoleIDOutput = (SELECT TOP(1) Id FROM dbo.Roles WHERE Name = @Role) -- Use the passed in string role name to find the RoleId integer.
		INSERT INTO dbo.LinkerUserToRoles (UserId, RoleId) VALUES (@IdentityOutput, @RoleIDOutput) -- Insert this in the LinkerUserToRoles table.
		
	END





GO
/****** Object:  StoredProcedure [dbo].[reset_db]    Script Date: 6/23/2017 8:59:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[reset_db]
AS
BEGIN
	--ALTER TABLE [dbo].[LinkerUserToRoles] NOCHECK CONSTRAINT ALL;
	DELETE FROM LinkerUserToRoles;
	DBCC CHECKIDENT ('MovieTheaterShowings_Test.dbo.LinkerUserToRoles',RESEED, 0)
	--TRUNCATE TABLE LinkerUserToRoles;
	--ALTER TABLE [dbo].[LinkerUserToRoles] WITH CHECK CHECK CONSTRAINT ALL;

	--TRUNCATE TABLE Roles; 
	DELETE FROM Roles;
	DBCC CHECKIDENT ('MovieTheaterShowings_Test.dbo.Roles',RESEED, 0)
	INSERT INTO Roles (Name) VALUES ('admin')
	INSERT INTO Roles (Name) VALUES ('user')

	--ALTER TABLE [dbo].[Users] NOCHECK CONSTRAINT ALL;
	DELETE FROM Users;
	DBCC CHECKIDENT ('MovieTheaterShowings_Test.dbo.Users',RESEED, 0);
	DECLARE @RoleIDoutput int;
	DECLARE @Identityoutput int;
	EXEC insert_User @Name = 'admin', @Role = 'admin', @password_hash = '$2a$12$/C1SjWLrAlhfGoUvOBpLjeKlH2GpJVKi7dff1UcumUoweYVts4gBe', @RoleIDOutput = @RoleIDoutput OUTPUT, @IdentityOutput = @Identityoutput OUTPUT;
	--TRUNCATE TABLE Users;
	--ALTER TABLE [dbo].[Users] WITH CHECK CHECK CONSTRAINT ALL;

	--ALTER TABLE [dbo].[ShowTimes] NOCHECK CONSTRAINT ALL;
	DELETE FROM ShowTimes;
	DBCC CHECKIDENT ('MovieTheaterShowings_Test.dbo.ShowTimes',RESEED, 0)
	--TRUNCATE TABLE ShowTimes;
	--ALTER TABLE [dbo].[ShowTimes] WITH CHECK CHECK CONSTRAINT ALL;

	--ALTER TABLE [dbo].[Movies] NOCHECK CONSTRAINT ALL;
	DELETE FROM Movies;
	DBCC CHECKIDENT ('MovieTheaterShowings_Test.dbo.Movies',RESEED, 0)
	--TRUNCATE TABLE Movies;
	--ALTER TABLE [dbo].[Movies] WITH CHECK CHECK CONSTRAINT ALL;
END





GO
/****** Object:  StoredProcedure [dbo].[search_movies_by_title]    Script Date: 6/23/2017 8:59:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  StoredProcedure [dbo].[search_movies_by_title_like]    Script Date: 6/23/2017 8:59:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[search_movies_by_title_like]
	@Title varchar(75)
AS
BEGIN
	SELECT *
	FROM [Movies]
	WHERE Title LIKE @Title;	
END



GO
/****** Object:  StoredProcedure [dbo].[select_all_movies]    Script Date: 6/23/2017 8:59:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[select_all_movies]
AS
BEGIN
	SELECT *
	FROM [Movies]
END




GO
/****** Object:  StoredProcedure [dbo].[select_all_showtimes_by_movie_id]    Script Date: 6/23/2017 8:59:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[select_all_showtimes_by_movie_id]
	@MovieID int
AS
BEGIN
	SELECT *
	FROM [ShowTimes]
	WHERE MovieId=@MovieID
END




GO
/****** Object:  StoredProcedure [dbo].[select_movie_by_id]    Script Date: 6/23/2017 8:59:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[select_movie_by_id]
	@MovieID int	
AS
BEGIN
	SELECT TOP(1) *
	FROM [Movies]
	WHERE Id=@MovieID
	

END




GO
/****** Object:  StoredProcedure [dbo].[select_user_by_name]    Script Date: 6/23/2017 8:59:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[select_user_by_name]
	@UserName varchar(50),
	-- Output variables:
	@Id int OUTPUT,
	@password_hash varchar(255) OUTPUT,
	@RoleId int OUTPUT,
	@RoleName varchar(50) OUTPUT
AS
BEGIN
	SELECT TOP(1) @Id = Id, @password_hash = password_hash
	FROM [Users]
	WHERE Name=@UserName
	
	-- Get the role information:
	SELECT TOP(1) @RoleId = LinkerUserToRoles.RoleId, @RoleName = Roles.Name FROM LinkerUserToRoles JOIN Roles ON LinkerUserToRoles.RoleId = Roles.Id WHERE UserId = @Id
	
END





GO
/****** Object:  StoredProcedure [dbo].[update_movie]    Script Date: 6/23/2017 8:59:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[update_movie]
	@MovieID int,
	@Title varchar(75),
	@RunTime int,
	@Image varchar(MAX)
AS
BEGIN
	UPDATE TOP(1)
		Movies
	SET 
		Title = @Title,
		RunTime = @RunTime,
		Image = @Image
	WHERE
		Id = @MovieID	
END

GO
USE [master]
GO
ALTER DATABASE [MovieTheaterShowings_Test] SET  READ_WRITE 
GO

/****** Insert basic data  ******/

USE [MovieTheaterShowings_Test]
INSERT INTO Roles (Name) VALUES ('admin');
INSERT INTO Roles (Name) VALUES ('user');
DECLARE @RoleIDoutput int;
DECLARE @Identityoutput int;
EXEC insert_User @Name = 'admin', @Role = 'admin', @password_hash = '$2a$12$/C1SjWLrAlhfGoUvOBpLjeKlH2GpJVKi7dff1UcumUoweYVts4gBe', @RoleIDOutput = @RoleIDoutput OUTPUT, @IdentityOutput = @Identityoutput OUTPUT;
GO