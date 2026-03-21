using NoticeBoardAPI.Common;
using NoticeBoardAPI.DTOs;

namespace NoticeBoardAPI.Interfaces
{
    public interface IAnnouncementService
    {
        Task<ServiceResponse<IEnumerable<AnnouncementDto>>> GetAllAsync();
        Task<ServiceResponse<IEnumerable<AnnouncementDto>>> GetAllAsync(int page, int pageSize);
        Task<ServiceResponse<int>> GetTotalCountAsync();
        Task<ServiceResponse<AnnouncementDto>> GetByIdAsync(int id);
        Task<ServiceResponse<IEnumerable<AnnouncementDto>>> GetByUserIdAsync(int userId, int page, int pageSize);
        Task<ServiceResponse<int>> GetCountByUserIdAsync(int userId);
        Task<ServiceResponse<bool>> CreateAsync(int userId, CreateAnnouncementRequest request);
        Task<ServiceResponse<bool>> UpdateAsync(int id, CreateAnnouncementRequest request);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}
