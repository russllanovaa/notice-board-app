CREATE OR ALTER PROCEDURE sp_UpdateAnnouncement
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