CREATE OR ALTER PROCEDURE sp_InsertAnnouncement
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