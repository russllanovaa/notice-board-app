CREATE DATABASE notice_board;
GO

USE [notice_board];
GO

CREATE TABLE Categories (
    Id INT PRIMARY KEY IDENTITY(1,1),
    NameOfCategory NVARCHAR(100) NOT NULL
);

CREATE TABLE SubCategories (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CategoryId INT NOT NULL,
    NameOfSubCategory NVARCHAR(100) NOT NULL,
    CONSTRAINT FK_SubCategories_Categories FOREIGN KEY (CategoryId)
        REFERENCES Categories(Id)
        ON DELETE CASCADE
);

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    GoogleId NVARCHAR(100) NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    DisplayName NVARCHAR(50) NOT NULL,
    AvatarUrl NVARCHAR(200),
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    LastLoginAt DATETIME NOT NULL DEFAULT GETDATE(),
    IsActive BIT NOT NULL DEFAULT 1
);

CREATE TABLE Announcements (
    Id INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL,
    CategoryId INT NOT NULL,
    SubCategoryId INT NOT NULL,
    Title NVARCHAR(100) NOT NULL,
    DescriptionOfNote NVARCHAR(250),
    CreateDate DATETIME NOT NULL DEFAULT GETDATE(),
    StatusOfNote NVARCHAR(50) NOT NULL DEFAULT 'active',
    CONSTRAINT FK_Announcements_Users FOREIGN KEY (UserId)
        REFERENCES Users(Id)
        ON DELETE CASCADE,
    CONSTRAINT FK_Announcements_Categories FOREIGN KEY (CategoryId)
        REFERENCES Categories(Id)
        ON DELETE NO ACTION,
    CONSTRAINT FK_Announcements_SubCategories FOREIGN KEY (SubCategoryId)
        REFERENCES SubCategories(Id)
        ON DELETE NO ACTION
);