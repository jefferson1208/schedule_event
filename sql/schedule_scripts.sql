IF OBJECT_ID(N'[Schedule_Migrations]') IS NULL
BEGIN
    CREATE TABLE [Schedule_Migrations] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK_Schedule_Migrations] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [Schedule_Migrations] WHERE [MigrationId] = N'20230528150309_ScheduleEventsMigrations')
BEGIN
    CREATE TABLE [Schedules] (
        [Id] uniqueidentifier NOT NULL,
        [Date] datetime2 NOT NULL,
        [Description] varchar(100) NULL,
        [Location] varchar(100) NULL,
        [Capacity] int NOT NULL,
        [TotalPeople] int NOT NULL,
        [TotalCollected] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Schedules] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [Schedule_Migrations] WHERE [MigrationId] = N'20230528150309_ScheduleEventsMigrations')
BEGIN
    CREATE TABLE [Guests] (
        [Id] uniqueidentifier NOT NULL,
        [Name] varchar(100) NULL,
        [Contribution] decimal(18,2) NOT NULL,
        [WithDrink] bit NOT NULL,
        [ScheduleId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Guests] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Guests_Schedules_ScheduleId] FOREIGN KEY ([ScheduleId]) REFERENCES [Schedules] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [Schedule_Migrations] WHERE [MigrationId] = N'20230528150309_ScheduleEventsMigrations')
BEGIN
    CREATE INDEX [IX_Guests_ScheduleId] ON [Guests] ([ScheduleId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [Schedule_Migrations] WHERE [MigrationId] = N'20230528150309_ScheduleEventsMigrations')
BEGIN
    INSERT INTO [Schedule_Migrations] ([MigrationId], [ProductVersion])
    VALUES (N'20230528150309_ScheduleEventsMigrations', N'6.0.3');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [Schedule_Migrations] WHERE [MigrationId] = N'20230529130215_ScheduleMigrations')
BEGIN
    INSERT INTO [Schedule_Migrations] ([MigrationId], [ProductVersion])
    VALUES (N'20230529130215_ScheduleMigrations', N'6.0.3');
END;
GO

COMMIT;
GO

