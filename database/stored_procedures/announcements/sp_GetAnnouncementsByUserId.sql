CREATE OR ALTER PROCEDURE sp_GetAnnouncementsByUserId
    @UserId INT,
    @Page INT = 1,
    @PageSize INT = 6
AS
BEGIN
    SELECT
        a.Id,
        a.CategoryId,
        a.SubCategoryId,
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
    WHERE a.UserId = @UserId
    ORDER BY a.CreateDate DESC
    OFFSET (@Page - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY
END
GO