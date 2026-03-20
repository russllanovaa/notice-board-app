using Dapper;
using NoticeBoardAPI.Data;
using NoticeBoardAPI.Models;
using NoticeBoardAPI.Interfaces;
using System.Net.WebSockets;

namespace NoticeBoardAPI.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public UserRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }   

        public async Task<UserModel?> GetByGoogleIdAsync(string googleId)
        {
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<UserModel>(
                "SELECT * FROM Users WHERE GoogleId = @GoogleId",
                new { GoogleId = googleId }
            );

        }

        public async Task<UserModel?> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<UserModel>(
                "SELECT * FROM Users WHERE Id = @Id",
                new { Id = id }
            );
        }

        public async Task<UserModel> CreateAsync(UserModel user)
        {
            using var connection = _connectionFactory.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(
                @"INSERT INTO Users (GoogleId, Email, DisplayName, AvatarUrl, CreatedAt, LastLoginAt, IsActive)
                    VALUES (@GoogleId, @Email, @DisplayName, @AvatarUrl, GETDATE(), GETDATE(), 1);
                    SELECT SCOPE_IDENTITY();",
                user
                );
            user.Id = id;
            return user;
        }

        public async Task UpdateLastLoginAsync(int userId)
        {
            using var connection = _connectionFactory.CreateConnection( );
            await connection.ExecuteAsync(
                "UPDATE Users SET LastLoginAt = GETDATE() WHERE Id = @Id",
                new { Id = userId }
                );
        }
    }
}
