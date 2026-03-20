using NoticeBoardAPI.Common;
using NoticeBoardAPI.DTOs;

namespace NoticeBoardAPI.Interfaces
{
    public interface IAnnouncementService
    {
        Task<ServiceResponse<IEnumerable<AnnouncementDto>>> GetAllAsync();
        Task<ServiceResponse<AnnouncementDto>> GetByIdAsync(int id);
        Task<ServiceResponse<bool>> CreateAsync(int userId, CreateAnnouncementRequest request);
        Task<ServiceResponse<bool>> UpdateAsync(int id, CreateAnnouncementRequest request);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}
