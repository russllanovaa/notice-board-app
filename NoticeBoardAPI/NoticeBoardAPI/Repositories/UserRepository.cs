using Dapper;
using NoticeBoardAPI.Data;
using NoticeBoardAPI.Models;

namespace NoticeBoardAPI.Repositories
{
    public class UserRepository
    {
        private readonly DbConnectionFactory _connectionFactory;
        public UserRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }   

        public async Task<User?> GetByGoogleIdAsync(string googleId)
        {
            using var connection = _connectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(
                "SELECT * FROM Users WHERE GoogleId = @GoogleId",
                new { GoogleId = googleId }
            );

        }

        public async Task<User> CreateAsync(User user)
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
