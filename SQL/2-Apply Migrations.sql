USE [TvMaze];

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Shows] (
    [Id] uniqueidentifier NOT NULL DEFAULT (NEWID()),
    [ShowId] int NOT NULL,
    [Url] nvarchar(max) NULL,
    [Name] nvarchar(max) NOT NULL,
    [OfficialSite] nvarchar(max) NULL,
    CONSTRAINT [PK_Shows] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Casts] (
    [Id] uniqueidentifier NOT NULL,
    [CastId] bigint NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Birthday] datetimeoffset NULL,
    [CharacterName] nvarchar(max) NOT NULL,
    [ShowId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Casts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Casts_Shows_ShowId] FOREIGN KEY ([ShowId]) REFERENCES [Shows] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Casts_ShowId] ON [Casts] ([ShowId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230208123457_intialial', N'7.0.2');
GO

COMMIT;
GO

