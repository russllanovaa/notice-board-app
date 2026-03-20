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
    UpdatedDate DATETIME NULL,
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
GO

CREATE INDEX IX_Announcements_UserId ON Announcements(UserId);
CREATE INDEX IX_Announcements_CategoryId ON Announcements(CategoryId);
CREATE INDEX IX_Announcements_SubCategoryId ON Announcements(SubCategoryId);
CREATE INDEX IX_Announcements_StatusOfNote ON Announcements(StatusOfNote);
CREATE INDEX IX_SubCategories_CategoryId ON SubCategories(CategoryId);
CREATE UNIQUE INDEX IX_Users_GoogleId ON Users(GoogleId);
CREATE UNIQUE INDEX IX_Users_Email ON Users(Email);
GO

INSERT INTO Categories (NameOfCategory)
VALUES
    (N'Побутова техніка'),
    (N'Комп''ютерна техніка'),
    (N'Смартфони'),
    (N'Інше');

INSERT INTO SubCategories (CategoryId, NameOfSubCategory)
VALUES
    (1, N'Холодильники'),
    (1, N'Пральні машини'),
    (1, N'Бойлери'),
    (1, N'Печі'),
    (1, N'Витяжки'),
    (1, N'Мікрохвильові печі'),
    (2, N'ПК'),
    (2, N'Ноутбуки'),
    (2, N'Монітори'),
    (2, N'Принтери'),
    (2, N'Сканери'),
    (3, N'Android смартфони'),
    (3, N'iOS/Apple смартфони'),
    (4, N'Одяг'),
    (4, N'Взуття'),
    (4, N'Аксесуари'),
    (4, N'Спортивне обладнання'),
    (4, N'Іграшки');
GO