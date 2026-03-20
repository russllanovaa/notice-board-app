CREATE OR ALTER PROCEDURE sp_DeleteAnnouncement
    @Id INT
AS
BEGIN
    DELETE FROM Announcements
    WHERE Id = @Id
END
GO