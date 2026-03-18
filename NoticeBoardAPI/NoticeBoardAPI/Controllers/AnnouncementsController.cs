using Microsoft.AspNetCore.Mvc;
using NoticeBoardAPI.Models;
using NoticeBoardAPI.DTOs;
using NoticeBoardAPI.Repositories;

namespace NoticeBoardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementsController: ControllerBase
    {
        private readonly AnnouncementRepository _announcementRepository;
        public AnnouncementsController(AnnouncementRepository announcementsRepository)
        {
            _announcementRepository = announcementsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var announcement = await _announcementRepository.GetAllAsync();
            return Ok(announcement);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var announcement = await _announcementRepository.GetByIdAsync(id);
            if (announcement == null) { return NotFound(); }
            return Ok(announcement);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAnnouncementRequest request)
        {
            await _announcementRepository.CreateAsync(1, request); //before auntification
            return Ok();    
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateAnnouncementRequest request)
        {
            var existing = await _announcementRepository.GetByIdAsync(id);
            if (existing == null) { return NotFound(); }
            await _announcementRepository.UpdateAsync(id, request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _announcementRepository.GetByIdAsync(id);
            if (existing == null) { return NotFound(); };
            await _announcementRepository.DeleteAsync(id);
            return NoContent();
        }

    }
}
