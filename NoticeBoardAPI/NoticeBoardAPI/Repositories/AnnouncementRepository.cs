using Dapper;
using NoticeBoardAPI.Data;
using NoticeBoardAPI.Models;
using NoticeBoardAPI.DTOs;

namespace NoticeBoardAPI.Repositories
{
    public class AnnouncementRepository
    {
        private readonly DbConnectionFactory dbConnectionFactory;
        public AnnouncementRepository(DbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<AnnouncementDto>> GetAllAsync()
        {
            using var connection = dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<AnnouncementDto>(
                "sp_GetAnnouncements",
                commandType: System.Data.CommandType.StoredProcedure
                );

        }

        public async Task<AnnouncementDto?> GetByIdAsync (int id)
        {
            using var connection = dbConnectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<AnnouncementDto>(
                "sp_GetAnnouncementById",
                new {Id = id},
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public async Task CreateAsync(int userId, CreateAnnouncementRequest request)
        {
            using var connection = dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(
                "sp_InsertAnnouncement",
                new
                {
                    UserId = userId,
                    request.CategoryId,
                    request.SubCategoryId,
                    request.Title,
                    request.DescriptionOfNote,
                    request.StatusOfNote
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public async Task UpdateAsync(int id, CreateAnnouncementRequest request)
        {
            using var connection = dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(
                "sp_UpdateAnnouncement",
                new
                {
                    Id = id,
                    request.CategoryId,
                    request.SubCategoryId,
                    request.Title,
                    request.DescriptionOfNote,
                    request.StatusOfNote
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(
                "sp_DeleteAnnouncement",
                new { Id = id },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }
    }
}
