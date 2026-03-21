CREATE OR ALTER PROCEDURE sp_GetAnnouncementsCountByUserId
    @UserId INT
AS
BEGIN
    SELECT COUNT(*) FROM Announcements WHERE UserId = @UserId
END
GO