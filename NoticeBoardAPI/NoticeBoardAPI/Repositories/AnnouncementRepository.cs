using Dapper;
using NoticeBoardAPI.Data;
using NoticeBoardAPI.Models;
using NoticeBoardAPI.DTOs;
using NoticeBoardAPI.Interfaces;

namespace NoticeBoardAPI.Repositories
{
    public class AnnouncementRepository: IAnnouncementRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public AnnouncementRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<AnnouncementDto>> GetAllAsync()
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<AnnouncementDto>(
                "sp_GetAnnouncements",
                commandType: System.Data.CommandType.StoredProcedure
                );

        }

        public async Task<IEnumerable<AnnouncementDto>> GetAllAsync(int page, int pageSize)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<AnnouncementDto>(
                "sp_GetAnnouncements",
                new { Page = page, PageSize = pageSize },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }

        public async Task<int> GetTotalCountAsync()
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QuerySingleAsync<int>(
                "sp_GetAnnouncementsCount",
                commandType: System.Data.CommandType.StoredProcedure
            );
        }

        public async Task<AnnouncementDto?> GetByIdAsync (int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<AnnouncementDto>(
                "sp_GetAnnouncementById",
                new {Id = id},
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public async Task<IEnumerable<AnnouncementDto>> GetByUserIdAsync(int userId, int page, int pageSize)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<AnnouncementDto>(
                "sp_GetAnnouncementsByUserId",
                new { UserId = userId, Page = page, PageSize = pageSize },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }

        public async Task<int> GetCountByUserIdAsync(int userId)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QuerySingleAsync<int>(
                "sp_GetAnnouncementsCountByUserId",
                new { UserId = userId },
                commandType: System.Data.CommandType.StoredProcedure
            );
        }

        public async Task CreateAsync(int userId, CreateAnnouncementRequest request)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(
                "sp_InsertAnnouncement",
                new
                {
                    UserId = userId,
                    request.CategoryId,
                    request.SubCategoryId,
                    request.Title,
                    request.DescriptionOfNote,
                    StatusOfNote = request.StatusOfNote.ToString().ToLower()
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public async Task UpdateAsync(int id, CreateAnnouncementRequest request)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(
                "sp_UpdateAnnouncement",
                new
                {
                    Id = id,
                    request.CategoryId,
                    request.SubCategoryId,
                    request.Title,
                    request.DescriptionOfNote,
                    StatusOfNote = request.StatusOfNote.ToString().ToLower()
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(
                "sp_DeleteAnnouncement",
                new { Id = id },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }
    }
}
