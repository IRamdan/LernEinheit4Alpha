CREATE DATABASE Lerneinheit3;
GO

USE Lerneinheit3;
GO

CREATE TABLE GAME (
    Ident int PRIMARY KEY IDENTITY(1,1),
    GameType VarChar(30),
    Rows int,
    Columns int,
    WinCondition int,
    DateCreated DATETIME2 DEFAULT GETDATE(),
    LastUpdated DATETIME2 DEFAULT GETDATE()
);
GO

CREATE TABLE Match (
    Ident int PRIMARY KEY IDENTITY(1,1),
    GameIdent int,
    Winner VarChar(30),
    Gamestate VarChar(30),
    IsDraw BIT,
    IsUnfinished BIT,
    DateCreated DATETIME2 DEFAULT GETDATE(),
    LastUpdated DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (GameIdent) REFERENCES GAME(Ident)
);
GO

CREATE TABLE Player (
    Ident int PRIMARY KEY IDENTITY(1,1),
    Nickname varchar(30) UNIQUE,
    Password varchar(30),
    DateCreated DATETIME2 DEFAULT GETDATE(),
    LastUpdated DATETIME2 DEFAULT GETDATE()
);
GO

CREATE TABLE PlayerMatch (
    Ident int PRIMARY KEY IDENTITY(1,1),
    MatchIdent int,
    PlayerIdent int,
    DateCreated DATETIME2 DEFAULT GETDATE(),
    LastUpdated DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (MatchIdent) REFERENCES Match(Ident),
    FOREIGN KEY (PlayerIdent) REFERENCES Player(Ident)
);
GO

CREATE TABLE MatchMove (
    Ident int PRIMARY KEY IDENTITY(1,1),
    PlayerMatchIdent int,
    GameIdent int,
    Row int,
    Col int,
    Sign NVarChar(40),
    DateCreated DATETIME2 DEFAULT GETDATE(),
    LastUpdated DATETIME2 DEFAULT GETDATE(),
    FOREIGN KEY (PlayerMatchIdent) REFERENCES PlayerMatch(Ident),
    FOREIGN KEY (GameIdent) REFERENCES GAME(Ident)
);
GO

CREATE PROCEDURE [dbo].[AddPlayerToPlayerMatch]
    @p_PlayerIdent int,
    @p_MatchIdent int 
AS
BEGIN
    INSERT INTO [dbo].[PlayerMatch](PlayerIdent, MatchIdent)
    VALUES (@p_PlayerIdent, @p_MatchIdent);
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[CreateMatch]
    @p_GameIdent INT,
    @p_Winner NVARCHAR(30),
    @p_Gamestate NVARCHAR(30)
AS
BEGIN
    INSERT INTO [dbo].[Match] (GameIdent, Winner, Gamestate, IsDraw, IsUnfinished)
    VALUES (@p_GameIdent, @p_Winner, @p_Gamestate, 0, 1);
    
    SELECT SCOPE_IDENTITY(); 
END;
GO

CREATE OR ALTER PROCEDURE [dbo].[CreatePlayerAccount]
    @p_NickName varchar(30),
    @p_Password varchar(30)
AS
BEGIN
    INSERT INTO [dbo].[Player] (NickName, Password)
    VALUES (@p_NickName, @p_Password);
END;
GO

CREATE PROCEDURE [dbo].[LoginPlayerAccount]
    @NickName varchar(30),
    @Password varchar(30)
AS
BEGIN
    IF EXISTS (SELECT * FROM [dbo].[Player] WHERE NickName = @NickName AND Password = @Password)
        SELECT 1;
    ELSE
        SELECT 0;
END;
GO

CREATE PROCEDURE [dbo].[SaveGame]
    @p_GameType VARCHAR(30),
    @p_Rows INT,
    @p_Columns INT,
    @p_WinCondition INT
AS
BEGIN
    INSERT INTO [dbo].[GAME] (GameType, Rows, Columns, WinCondition)
    VALUES (@p_GameType, @p_Rows, @p_Columns, @p_WinCondition);

    SELECT SCOPE_IDENTITY() AS GameIdent;
END;
GO

CREATE or ALTER PROCEDURE [dbo].[SaveMatch]
    @p_MatchIdent INT, 
    @p_GameIdent INT, 
    @p_Winner NVARCHAR(30),
    @p_Gamestate NVARCHAR(30),
    @p_IsDraw BIT,
	@p_IsUnfinished BIT
AS
BEGIN
    UPDATE [dbo].[Match]
    SET
        GameIdent = @p_GameIdent,
        Winner = @p_Winner,
        Gamestate = @p_Gamestate,
        IsDraw = @p_IsDraw,
		IsUnfinished = @p_IsUnfinished,
		LastUpdated = GETDATE()

    WHERE Ident = @p_MatchIdent;
END;
GO

CREATE PROCEDURE [dbo].[SaveMove]
    @p_PlayerMatchIdent int,
    @p_GameIdent int,
    @p_Row int,
    @p_Col int,
    @p_Sign nvarchar(40)
AS
BEGIN
    INSERT INTO [dbo].[MatchMove] (PlayerMatchIdent, GameIdent, Row, Col, Sign)
    VALUES (@p_PlayerMatchIdent, @p_GameIdent, @p_Row, @p_Col, @p_Sign);
END;
GO

CREATE OR ALTER FUNCTION [dbo].[SuccessRate] (
    @p_Wins VARCHAR(30),
    @p_Matches VARCHAR(30)
)
RETURNS DECIMAL(4,2)
AS
BEGIN
    DECLARE @WinsAsDecimal DECIMAL(4,2)
    DECLARE @MatchesAsDecimal DECIMAL(4,2)
    DECLARE @SuccessRate DECIMAL(4,2)  

    SET @WinsAsDecimal = CONVERT(DECIMAL(4,2), @p_Wins)
    SET @MatchesAsDecimal = CONVERT(DECIMAL(4,2), @p_Matches)

    IF @MatchesAsDecimal = 0
        SET @SuccessRate = 0; 
    ELSE
        SET @SuccessRate = @WinsAsDecimal / @MatchesAsDecimal * 100;

    RETURN @SuccessRate;  
END;
GO

CREATE OR ALTER VIEW [dbo].[PlayerGameStats] AS
WITH CalculatedStats AS (
    SELECT
        [dbo].[Player].Nickname,
        COUNT([dbo].[Match].Ident) AS TotalGames,
        SUM(CASE
            WHEN ISNULL(TRY_CAST([Match].Winner AS INT), 0) = [Player].Ident THEN 1
            ELSE 0
        END) AS TotalWins,
        SUM(CASE
            WHEN ISNULL(TRY_CAST([Match].Winner AS INT), 0) <> [Player].Ident THEN 1
            ELSE 0
        END) AS TotalLosses,
        SUM(CASE WHEN [Match].IsDraw = 0 THEN 0 ELSE 1 END) AS TotalDraws,
        SUM(CASE WHEN [Match].IsUnfinished = 1 THEN 1 ELSE 0 END) AS TotalUnfinishedGames
    FROM
        [dbo].[Player]
    LEFT JOIN
        [dbo].[PlayerMatch] ON [dbo].[Player].Ident = [dbo].[PlayerMatch].PlayerIdent 
    LEFT JOIN
        [dbo].[Match] ON [dbo].[PlayerMatch].MatchIdent = [dbo].[Match].Ident 
    GROUP BY
        [dbo].[Player].Ident, [dbo].[Player].Nickname
)
SELECT 
    Nickname,
    TotalGames,
    TotalWins,
    TotalLosses,
    TotalDraws,
    TotalUnfinishedGames,
    dbo.SuccessRate(TotalWins,TotalGames) AS SuccessRate
FROM 
    CalculatedStats;
GO

CREATE OR ALTER VIEW [dbo].[Leaderboard]
AS
WITH CalculatedStats AS (
    SELECT
        [Player].Ident,
        [Player].Nickname,
        COUNT([Match].Ident) AS TotalGames,
        SUM(CASE
            WHEN ISNUMERIC([Match].Winner) = 1 AND CAST([Match].Winner AS INT) = [Player].Ident THEN 1
            ELSE 0
        END) AS TotalWins,
        SUM(CASE
            WHEN ISNUMERIC([Match].Winner) = 1 AND CAST([Match].Winner AS INT) <> [Player].Ident THEN 1
            ELSE 0
        END) AS TotalLosses,
        SUM(CASE WHEN [Match].IsDraw = 0 THEN 0 ELSE 1 END) AS TotalDraws,
        SUM(CASE WHEN [Match].IsUnfinished = 1 THEN 1 ELSE 0 END) AS TotalUnfinishedGames
    FROM
        [dbo].[Player]
    LEFT JOIN
        [dbo].[PlayerMatch] ON [Player].Ident = [dbo].[PlayerMatch].PlayerIdent 
    LEFT JOIN
        [dbo].[Match] ON [dbo].[PlayerMatch].MatchIdent = [dbo].[Match].Ident
    GROUP BY
        [Player].Ident, [Player].Nickname
)
SELECT TOP 20
    Nickname,
    CAST(((TotalWins * 5 + TotalDraws * 2 + TotalUnfinishedGames * -1)) AS DECIMAL(5, 2)) AS Points
FROM 
    CalculatedStats
ORDER BY
    Points DESC
GO





