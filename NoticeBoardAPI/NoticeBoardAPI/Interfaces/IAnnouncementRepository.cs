using NoticeBoardAPI.DTOs;
using NoticeBoardAPI.Models;

namespace NoticeBoardAPI.Interfaces
{
    public interface IAnnouncementRepository
    {
        Task<IEnumerable<AnnouncementDto>> GetAllAsync();
        Task<AnnouncementDto?> GetByIdAsync(int id);
        Task CreateAsync(int userId, CreateAnnouncementRequest request);
        Task UpdateAsync(int id, CreateAnnouncementRequest request);
        Task DeleteAsync(int id);
    }
}
