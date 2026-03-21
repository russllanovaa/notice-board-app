using NoticeBoardAPI.Common;
using NoticeBoardAPI.DTOs;
using NoticeBoardAPI.Repositories;
using NoticeBoardAPI.Interfaces;

namespace NoticeBoardAPI.Services
{
    public class AnnouncementService: IAnnouncementService
    {
        private readonly IAnnouncementRepository _announcementRepository;
        public AnnouncementService(IAnnouncementRepository announcementRepository)
        {
            _announcementRepository = announcementRepository;
        }

        public async Task<ServiceResponse<IEnumerable<AnnouncementDto>>> GetAllAsync()
        {
            var announcements = await _announcementRepository.GetAllAsync();
            return ServiceResponse<IEnumerable<AnnouncementDto>>.Ok(announcements); 
        }

        public async Task<ServiceResponse<AnnouncementDto>> GetByIdAsync(int id)
        {
            var announcement = await _announcementRepository.GetByIdAsync(id);
            if(announcement == null)
            {
                return ServiceResponse<AnnouncementDto>.Fail($"Оголошення з Id {id} не знайдено");
            }
            return ServiceResponse<AnnouncementDto>.Ok(announcement);
        }

        public async Task<ServiceResponse<IEnumerable<AnnouncementDto>>> GetByUserIdAsync(int userId)
        {
            var announcement = await _announcementRepository.GetByUserIdAsync(userId);
            return ServiceResponse<IEnumerable<AnnouncementDto>>.Ok(announcement);
        }

        public async Task<ServiceResponse<bool>> CreateAsync(int userId, CreateAnnouncementRequest request)
        {
            await _announcementRepository.CreateAsync(userId, request);
            return ServiceResponse<bool>.Ok(true, "Оголошення успішно створено");
        }

        public async Task<ServiceResponse<bool>> UpdateAsync(int id, CreateAnnouncementRequest request)
        {
            var existing = await _announcementRepository.GetByIdAsync(id);
            if(existing == null)
            {
                return ServiceResponse<bool>.Fail($"Оголошення з Id {id} не знайдено");
            }
            await _announcementRepository.UpdateAsync(id, request);
            return ServiceResponse<bool>.Ok(true, "Оголошення успішно оновлено");
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(int id)
        {
            var existing = await _announcementRepository.GetByIdAsync(id);
            if(existing == null)
            {
                return ServiceResponse<bool>.Fail($"Оголошення з Id {id} не знайдено");
            }
            await _announcementRepository.DeleteAsync(id);
            return ServiceResponse<bool>.Ok(true, "Оголошення успішно видалено");
        }
    }
}
