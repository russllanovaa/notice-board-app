USE [notice_board];
GO

CREATE PROCEDURE sp_GetAnnouncements
AS
BEGIN
    SELECT
        a.Id,
        a.Title,
        a.DescriptionOfNote,
        a.CreateDate,
        a.StatusOfNote,
        a.UpdatedDate,
        c.NameOfCategory,
        s.NameOfSubCategory,
        u.DisplayName,
        u.Email
    FROM Announcements a
    INNER JOIN Categories c ON a.CategoryId = c.Id
    INNER JOIN SubCategories s ON a.SubCategoryId = s.Id
    INNER JOIN Users u ON a.UserId = u.Id
END
GO

CREATE PROCEDURE sp_GetAnnouncementById
    @Id INT
AS
BEGIN
    SELECT
        a.Id,
        a.Title,
        a.DescriptionOfNote,
        a.CreateDate,
        a.StatusOfNote,
        a.UpdatedDate,
        c.NameOfCategory,
        s.NameOfSubCategory,
        u.DisplayName,
        u.Email
    FROM Announcements a
    INNER JOIN Categories c ON a.CategoryId = c.Id
    INNER JOIN SubCategories s ON a.SubCategoryId = s.Id
    INNER JOIN Users u ON a.UserId = u.Id
    WHERE a.Id = @Id
END
GO

CREATE PROCEDURE sp_InsertAnnouncement
    @UserId INT,
    @CategoryId INT,
    @SubCategoryId INT,
    @Title NVARCHAR(100),
    @DescriptionOfNote NVARCHAR(250),
    @StatusOfNote NVARCHAR(50)
AS
BEGIN
    INSERT INTO Announcements (UserId, CategoryId, SubCategoryId, Title, DescriptionOfNote, CreateDate, StatusOfNote)
    VALUES (@UserId, @CategoryId, @SubCategoryId, @Title, @DescriptionOfNote, GETDATE(), @StatusOfNote)
END
GO

CREATE PROCEDURE sp_UpdateAnnouncement
    @Id INT,
    @CategoryId INT,
    @SubCategoryId INT,
    @Title NVARCHAR(100),
    @DescriptionOfNote NVARCHAR(250),
    @StatusOfNote NVARCHAR(50)
AS
BEGIN
    UPDATE Announcements
    SET
        CategoryId = @CategoryId,
        SubCategoryId = @SubCategoryId,
        Title = @Title,
        DescriptionOfNote = @DescriptionOfNote,
        StatusOfNote = @StatusOfNote,
        UpdatedDate = GETDATE()
    WHERE Id = @Id
END
GO

CREATE PROCEDURE sp_DeleteAnnouncement
    @Id INT
AS
BEGIN
    DELETE FROM Announcements
    WHERE Id = @Id
END
GO
