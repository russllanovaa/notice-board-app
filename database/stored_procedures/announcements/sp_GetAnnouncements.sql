CREATE OR ALTER PROCEDURE sp_GetAnnouncements
AS
BEGIN
    SELECT
        a.Id,
        a.Title,
        a.DescriptionOfNote,
        a.CreateDate,
        a.UpdatedDate,
        a.StatusOfNote,
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