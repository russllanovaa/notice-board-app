using NoticeBoardAPI.Models;

namespace NoticeBoardAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel?> GetByGoogleIdAsync(string googleId);
        Task<UserModel?> GetByIdAsync(int id);
        Task<UserModel> CreateAsync(UserModel user);
        Task UpdateLastLoginAsync(int userId);
    }
}
