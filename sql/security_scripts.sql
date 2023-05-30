IF OBJECT_ID(N'[Security_Migrations]') IS NULL
BEGIN
    CREATE TABLE [Security_Migrations] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK_Security_Migrations] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [Security_Migrations] WHERE [MigrationId] = N'20230529125941_SecurityMigrations')
BEGIN
    CREATE TABLE [Roles] (
        [Id] varchar(100) NOT NULL,
        [Name] varchar(256) NULL,
        [NormalizedName] varchar(256) NULL,
        [ConcurrencyStamp] varchar(100) NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [Security_Migrations] WHERE [MigrationId] = N'20230529125941_SecurityMigrations')
BEGIN
    CREATE TABLE [Users] (
        [Id] varchar(100) NOT NULL,
        [UserName] varchar(256) NULL,
        [NormalizedUserName] varchar(256) NULL,
        [Email] varchar(256) NULL,
        [NormalizedEmail] varchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] varchar(100) NULL,
        [SecurityStamp] varchar(100) NULL,
        [ConcurrencyStamp] varchar(100) NULL,
        [PhoneNumber] varchar(100) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [Security_Migrations] WHERE [MigrationId] = N'20230529125941_SecurityMigrations')
BEGIN
    CREATE TABLE [Role_Claims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] varchar(100) NOT NULL,
        [ClaimType] varchar(100) NULL,
        [ClaimValue] varchar(100) NULL,
        CONSTRAINT [PK_Role_Claims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Role_Claims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [Security_Migrations] WHERE [MigrationId] = N'20230529125941_SecurityMigrations')
BEGIN
    CREATE TABLE [User_Claims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] varchar(100) NOT NULL,
        [ClaimType] varchar(100) NULL,
        [ClaimValue] varchar(100) NULL,
        CONSTRAINT [PK_User_Claims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_User_Claims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [Security_Migrations] WHERE [MigrationId] = N'20230529125941_SecurityMigrations')
BEGIN
    CREATE TABLE [User_Logins] (
        [LoginProvider] varchar(128) NOT NULL,
        [ProviderKey] varchar(128) NOT NULL,
        [ProviderDisplayName] varchar(100) NULL,
        [UserId] varchar(100) NOT NULL,
        CONSTRAINT [PK_User_Logins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_User_Logins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [Security_Migrations] WHERE [MigrationId] = N'20230529125941_SecurityMigrations')
BEGIN
    CREATE TABLE [User_Roles] (
        [UserId] varchar(100) NOT NULL,
        [RoleId] varchar(100) NOT NULL,
        CONSTRAINT [PK_User_Roles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_User_Roles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]),
        CONSTRAINT [FK_User_Roles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [Security_Migrations] WHERE [MigrationId] = N'20230529125941_SecurityMigrations')
BEGIN
    CREATE TABLE [User_Tokens] (
        [UserId] varchar(100) NOT NULL,
        [LoginProvider] varchar(128) NOT NULL,
        [Name] varchar(128) NOT NULL,
        [Value] varchar(100) NULL,
        CONSTRAINT [PK_User_Tokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_User_Tokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [Security_Migrations] WHERE [MigrationId] = N'20230529125941_SecurityMigrations')
BEGIN
    CREATE INDEX [IX_Role_Claims_RoleId] ON [Role_Claims] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [Security_Migrations] WHERE [MigrationId] = N'20230529125941_SecurityMigrations')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [Roles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [Security_Migrations] WHERE [MigrationId] = N'20230529125941_SecurityMigrations')
BEGIN
    CREATE INDEX [IX_User_Claims_UserId] ON [User_Claims] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [Security_Migrations] WHERE [MigrationId] = N'20230529125941_SecurityMigrations')
BEGIN
    CREATE INDEX [IX_User_Logins_UserId] ON [User_Logins] ([UserId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [Security_Migrations] WHERE [MigrationId] = N'20230529125941_SecurityMigrations')
BEGIN
    CREATE INDEX [IX_User_Roles_RoleId] ON [User_Roles] ([RoleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [Security_Migrations] WHERE [MigrationId] = N'20230529125941_SecurityMigrations')
BEGIN
    CREATE INDEX [EmailIndex] ON [Users] ([NormalizedEmail]);
END;
GO

IF NOT EXISTS(SELECT * FROM [Security_Migrations] WHERE [MigrationId] = N'20230529125941_SecurityMigrations')
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [Users] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;
GO

IF NOT EXISTS(SELECT * FROM [Security_Migrations] WHERE [MigrationId] = N'20230529125941_SecurityMigrations')
BEGIN
    INSERT INTO [Security_Migrations] ([MigrationId], [ProductVersion])
    VALUES (N'20230529125941_SecurityMigrations', N'6.0.3');
END;
GO

COMMIT;
GO

