-- =====================================================================
-- Script: CreateUsersTable.sql
-- Description: Creates the Users table and unique indexes for authentication.
-- Target Database: StudentDashboardDb
-- =====================================================================

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Users')
BEGIN
    CREATE TABLE [dbo].[Users] (
        [Id]           INT            IDENTITY (1, 1) NOT NULL,
        [FullName]     NVARCHAR (100) NOT NULL,
        [Username]     NVARCHAR (50)  NOT NULL,
        [Email]        NVARCHAR (100) NOT NULL,
        [PasswordHash] NVARCHAR (MAX) NOT NULL,
        [Role]         NVARCHAR (50)  NOT NULL CONSTRAINT [DF_Users_Role] DEFAULT ('User'),
        [CreatedAt]    DATETIME2 (7)  NOT NULL CONSTRAINT [DF_Users_CreatedAt] DEFAULT (SYSUTCDATETIME()),
        [LastLoginAt]  DATETIME2 (7)  NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
    );

    CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Username]
        ON [dbo].[Users]([Username] ASC);

    CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Email]
        ON [dbo].[Users]([Email] ASC);

    PRINT 'Users table and indexes created successfully.';
END
ELSE
BEGIN
    PRINT 'Users table already exists.';
END
GO
