using NoticeBoardAPI.DTOs;
using NoticeBoardAPI.Models;

namespace NoticeBoardAPI.Interfaces
{
    public interface IAnnouncementRepository
    {
        Task<IEnumerable<AnnouncementDto>> GetAllAsync();
        Task<IEnumerable<AnnouncementDto>> GetAllAsync(int page, int pageSize);
        Task<int> GetTotalCountAsync();
        Task<AnnouncementDto?> GetByIdAsync(int id);
        Task<IEnumerable<AnnouncementDto>> GetByUserIdAsync(int userId, int page, int pageSize);
        Task<int> GetCountByUserIdAsync(int userId);
        Task CreateAsync(int userId, CreateAnnouncementRequest request);
        Task UpdateAsync(int id, CreateAnnouncementRequest request);
        Task DeleteAsync(int id);
    }
}
