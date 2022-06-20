CREATE DATABASE [lab3];
GO

USE [lab3];
GO

CREATE TABLE [User] (
    [Id] uniqueidentifier NOT NULL,
    [Role] int NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Email] nvarchar(450) NOT NULL,
    [DateOfBirth] datetime NOT NULL,
	[Password] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id]),
	CONSTRAINT UC_Email UNIQUE ([Email])
);
GO

CREATE TABLE [Subject] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Subject] PRIMARY KEY ([Id]),
    CONSTRAINT UC_Name UNIQUE ([Name])
);
GO

CREATE TABLE [Group] (
    [Id] uniqueidentifier NOT NULL,
    [Code] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Group] PRIMARY KEY ([Id]),
    CONSTRAINT UC_Code UNIQUE ([Code])
);
GO

CREATE TABLE [StudentGroup] (
    [StudentId] uniqueidentifier NOT NULL,
    [GroupId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_StudentGroup] PRIMARY KEY CLUSTERED ([StudentId], [GroupId]),
    CONSTRAINT [FK_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Lesson] (
    [Id] uniqueidentifier NOT NULL,
    [TeacherId] uniqueidentifier NOT NULL,
    [SubjectId] uniqueidentifier NOT NULL,
    [GroupId] uniqueidentifier NOT NULL,
    [Start] datetime NOT NULL,
    [Status] int NOT NULL,
	[File] varbinary(max) NULL,
    CONSTRAINT [PK_Lesson] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SubjectId] FOREIGN KEY ([SubjectId]) REFERENCES [Subject] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_TeacherId] FOREIGN KEY ([TeacherId]) REFERENCES [User] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Group] ([Id]) ON DELETE CASCADE,
	CONSTRAINT UC_Ids UNIQUE ([GroupId], [Start])
);
GO

CREATE TABLE [Message] (
    [Id] uniqueidentifier NOT NULL,
    [From] uniqueidentifier NOT NULL,
	[To] uniqueidentifier NOT NULL,
	[Text] nvarchar(max) NOT NULL,
	[Date] datetime NOT NULL,
    CONSTRAINT [PK_Message] PRIMARY KEY ([Id]),
);
GO

CREATE OR ALTER PROC [dbo].[Select Message] 
    @Id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	BEGIN TRAN

	SELECT *
	FROM   [dbo].[Message] 
	WHERE  ([Id] = @Id OR @Id IS NULL) 

	COMMIT
GO
CREATE OR ALTER PROC [dbo].[Insert Message] 
    @Id uniqueidentifier NOT NULL,
    @From uniqueidentifier NOT NULL,
	@To uniqueidentifier NOT NULL,
	@Text nvarchar(max) NOT NULL,
	@Date datetime NOT NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED	

	BEGIN TRAN
	
	INSERT INTO [dbo].[Message] ([Id], [From], [To], [Text], [Date])
	SELECT @Id, @From, @To, @Text, @Date
               
	COMMIT
GO
CREATE OR ALTER PROC [dbo].[Update Message] 
    @Id uniqueidentifier NOT NULL,
    @From uniqueidentifier NOT NULL,
	@To uniqueidentifier NOT NULL,
	@Text nvarchar(max) NOT NULL,
	@Date datetime NOT NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED	

	BEGIN TRAN

	UPDATE [dbo].[Message]
	SET    [From] = @From, [To] = @To, [Text] = @Text, [Date] = @Date
	WHERE  [Id] = @Id

	COMMIT
GO
CREATE OR ALTER PROC [dbo].[Delete Message] 
    @Id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Message]
	WHERE  [Id] = @Id

	COMMIT
GO
----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------
CREATE OR ALTER PROC [dbo].[Select Group] 
    @Id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	BEGIN TRAN

	SELECT [Id], [Code] 
	FROM   [dbo].[Group] 
	WHERE  ([Id] = @Id OR @Id IS NULL) 

	COMMIT
GO
CREATE OR ALTER PROC [dbo].[Insert Group] 
    @Id uniqueidentifier,
    @Code nvarchar(450)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED	

	BEGIN TRAN
	
	INSERT INTO [dbo].[Group] ([Id], [Code])
	SELECT @Id, @Code
               
	COMMIT
GO
CREATE OR ALTER PROC [dbo].[Update Group] 
    @Id uniqueidentifier,
    @Code nvarchar(450)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED	

	BEGIN TRAN

	UPDATE [dbo].[Group]
	SET    [Code] = @Code
	WHERE  [Id] = @Id

	COMMIT
GO
CREATE OR ALTER PROC [dbo].[Delete Group] 
    @Id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Group]
	WHERE  [Id] = @Id

	COMMIT
GO
----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

CREATE OR ALTER PROC [dbo].[Select Lesson] 
    @Id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	BEGIN TRAN

	SELECT [Id], [TeacherId], [SubjectId], [GroupId], [Start], [Status] 
	FROM   [dbo].[Lesson] 
	WHERE  ([Id] = @Id OR @Id IS NULL) 

	COMMIT
GO
CREATE OR ALTER PROC [dbo].[Insert Lesson] 
    @Id uniqueidentifier,
    @TeacherId uniqueidentifier,
    @SubjectId uniqueidentifier,
    @GroupId uniqueidentifier,
    @Start date,
    @Status int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED	

	BEGIN TRAN
	
	INSERT INTO [dbo].[Lesson] ([Id], [TeacherId], [SubjectId], [GroupId], [Start], [Status])
	SELECT @Id, @TeacherId, @SubjectId, @GroupId, @Start, @Status
               
	COMMIT
GO
CREATE OR ALTER PROC [dbo].[Update Lesson] 
    @Id uniqueidentifier,
    @TeacherId uniqueidentifier,
    @SubjectId uniqueidentifier,
    @GroupId uniqueidentifier,
    @Start date,
    @Status int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED	

	BEGIN TRAN

	UPDATE [dbo].[Lesson]
	SET    [TeacherId] = @TeacherId, [SubjectId] = @SubjectId, [GroupId] = @GroupId, [Start] = @Start, [Status] = @Status
	WHERE  [Id] = @Id

	COMMIT
GO
CREATE OR ALTER PROC [dbo].[Delete Lesson] 
    @Id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Lesson]
	WHERE  [Id] = @Id

	COMMIT
GO
----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

CREATE OR ALTER PROC [dbo].[Select StudentGroup] 
    @StudentId uniqueidentifier,
    @GroupId uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	BEGIN TRAN

	SELECT [StudentId], [GroupId] 
	FROM   [dbo].[StudentGroup] 
	WHERE  ([StudentId] = @StudentId OR @StudentId IS NULL) 
	       AND ([GroupId] = @GroupId OR @GroupId IS NULL) 

	COMMIT
GO
CREATE OR ALTER PROC [dbo].[Insert StudentGroup] 
    @StudentId uniqueidentifier,
    @GroupId uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED	

	BEGIN TRAN
	
	INSERT INTO [dbo].[StudentGroup] ([StudentId], [GroupId])
	SELECT @StudentId, @GroupId
               
	COMMIT
GO

CREATE OR ALTER PROC [dbo].[Delete StudentGroup] 
    @StudentId uniqueidentifier,
    @GroupId uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[StudentGroup]
	WHERE  [StudentId] = @StudentId
	       AND [GroupId] = @GroupId

	COMMIT
GO
----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

CREATE OR ALTER PROC [dbo].[Select Subject] 
    @Id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	BEGIN TRAN

	SELECT [Id], [Name] 
	FROM   [dbo].[Subject] 
	WHERE  ([Id] = @Id OR @Id IS NULL) 

	COMMIT
GO
CREATE OR ALTER PROC [dbo].[Insert Subject] 
    @Id uniqueidentifier,
    @Name nvarchar(450)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED	

	BEGIN TRAN
	
	INSERT INTO [dbo].[Subject] ([Id], [Name])
	SELECT @Id, @Name
               
	COMMIT
GO
CREATE OR ALTER PROC [dbo].[Update Subject] 
    @Id uniqueidentifier,
    @Name nvarchar(450)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED	

	BEGIN TRAN

	UPDATE [dbo].[Subject]
	SET    [Name] = @Name
	WHERE  [Id] = @Id
	
	COMMIT
GO
CREATE OR ALTER PROC [dbo].[Delete Subject] 
    @Id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Subject]
	WHERE  [Id] = @Id

	COMMIT
GO
----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------

CREATE OR ALTER PROC [dbo].[Select User] 
    @Id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	BEGIN TRAN

	SELECT [Id], [Role], [FirstName], [LastName], [Email], [DateOfBirth], [Password]
	FROM   [dbo].[User] 
	WHERE  ([Id] = @Id OR @Id IS NULL) 

	COMMIT
GO
CREATE OR ALTER PROC [dbo].[Insert User] 
    @Id uniqueidentifier,
    @Role int,
    @FirstName nvarchar(MAX),
    @LastName nvarchar(MAX),
    @Email nvarchar(450),
    @DateOfBirth datetime,
	@Password nvarchar(max)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED	

	BEGIN TRAN
	
	INSERT INTO [dbo].[User] ([Id], [Role], [FirstName], [LastName], [Email], [DateOfBirth], [Password])
	SELECT @Id, @Role, @FirstName, @LastName, @Email, @DateOfBirth, @Password
	
	COMMIT
GO
CREATE OR ALTER PROC [dbo].[Update User] 
    @Id uniqueidentifier,
    @Role int,
    @FirstName nvarchar(MAX),
    @LastName nvarchar(MAX),
    @Email nvarchar(450),
    @DateOfBirth datetime,
	@Password nvarchar(MAX)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED	

	BEGIN TRAN

	UPDATE [dbo].[User]
	SET    [Role] = @Role, [FirstName] = @FirstName, [LastName] = @LastName, [Email] = @Email, [DateOfBirth] = @DateOfBirth, [Password] = @Password
	WHERE  [Id] = @Id
	
	COMMIT
GO
CREATE OR ALTER PROC [dbo].[Delete User] 
    @Id uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[User]
	WHERE  [Id] = @Id

	COMMIT
GO
----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------
CREATE OR ALTER PROC [dbo].[GetUserByEmail] 
    @Email nvarchar(450)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	BEGIN TRAN

	SELECT [Id], [Role], [FirstName], [LastName], [Email], [DateOfBirth], [Password]
	FROM   [dbo].[User] 
	WHERE  ([Email] = @Email) 

	COMMIT
GO
----------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------
CREATE OR ALTER PROC [dbo].[GetStudentsByLesson] 
    @LessonId uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON
	SET TRANSACTION ISOLATION LEVEL READ COMMITTED

	BEGIN TRAN

	DECLARE @GroupId uniqueidentifier;
	select @GroupId = GroupId from [Lesson] where Id = @LessonId

	SELECT [Id], [Role], [FirstName], [LastName], [Email], [DateOfBirth], [Password]
	FROM   [dbo].[User] u
	JOIN [StudentGroup] sg on sg.StudentId = u.Id and sg.GroupId = @GroupId
	WHERE  u.[Role] = 1

	COMMIT
GO