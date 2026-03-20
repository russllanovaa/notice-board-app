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
            var responce = await _announcementService.GetAllAsync();
            return Ok(responce);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var responce = await _announcementService.GetByIdAsync(id);
            if (!responce.Success)
            {
                return NotFound();
            }
            return Ok(responce);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAnnouncementRequest request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var responce = await _announcementService.CreateAsync(userId, request); 
            return Ok(responce);    
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateAnnouncementRequest request)
        {
            var responce = await _announcementService.UpdateAsync(id, request);
            if (responce == null) 
            { 
                return NotFound(); 
            }
            return Ok(responce);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var responce = await _announcementService.DeleteAsync(id);
            if (!responce.Success)
            {
                return NotFound(responce);
            }
            return NoContent();
        }

    }
}
