using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoticeBoardAPI.DTOs;
using NoticeBoardAPI.Services;
using System.Security.Claims;

namespace NoticeBoardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementsController: ControllerBase
    {
        private readonly AnnouncementService _announcementService;
        public AnnouncementsController(AnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var announcement = await _announcementService.GetAllAsync();
            return Ok(announcement);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var announcement = await _announcementService.GetByIdAsync(id);
            if (announcement == null) { return NotFound(); }
            return Ok(announcement);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAnnouncementRequest request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _announcementService.CreateAsync(userId, request); 
            return Ok();    
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateAnnouncementRequest request)
        {
            var existing = await _announcementService.GetByIdAsync(id);
            if (existing == null) { return NotFound(); }
            await _announcementService.UpdateAsync(id, request);
            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _announcementService.GetByIdAsync(id);
            if (existing == null) { return NotFound(); };
            await _announcementService.DeleteAsync(id);
            return NoContent();
        }

    }
}
