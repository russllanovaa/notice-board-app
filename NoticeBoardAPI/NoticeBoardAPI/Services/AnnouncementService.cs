using NoticeBoardAPI.DTOs;
using NoticeBoardAPI.Repositories;

namespace NoticeBoardAPI.Services
{
    public class AnnouncementService
    {
        private readonly AnnouncementRepository _announcementRepository;
        public AnnouncementService(AnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public async Task<IEnumerable<AnnouncementDto>> GetAllAsync()
        {
            return await _announcementRepository.GetAllAsync(); 
        }

        public async Task<AnnouncementDto?> GetByIdAsync(int id)
        {
            return await _announcementRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(int userId, CreateAnnouncementRequest request)
        {
            await _announcementRepository.CreateAsync(userId, request);
        }

        public async Task UpdateAsync(int id, CreateAnnouncementRequest request)
        {
            await _announcementRepository.UpdateAsync(id, request);
        }

        public async Task DeleteAsync(int id)
        {
            await _announcementRepository.DeleteAsync(id);
        }
    }
}
