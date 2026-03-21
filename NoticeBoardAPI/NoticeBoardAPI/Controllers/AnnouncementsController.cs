using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoticeBoardAPI.Interfaces;
using NoticeBoardAPI.DTOs;
using NoticeBoardAPI.Services;
using System.Security.Claims;

namespace NoticeBoardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementsController: ControllerBase
    {
        private readonly IAnnouncementService _announcementService;
        public AnnouncementsController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 6)
        {
            var response = await _announcementService.GetAllAsync(page, pageSize);
            var countResponse = await _announcementService.GetTotalCountAsync();

            return Ok(new
            {
                data = response.Data,
                success = true,
                message = "",
                page = page,
                pageSize = pageSize,
                totalCount = countResponse.Data,
                totalPages = (int)Math.Ceiling((double)countResponse.Data / pageSize)
            });
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
        [HttpGet("my")]
        public async Task<IActionResult> GetMy([FromQuery] int page = 1, [FromQuery] int pageSize = 6)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var response = await _announcementService.GetByUserIdAsync(userId, page, pageSize);
            var countResponse = await _announcementService.GetCountByUserIdAsync(userId);

            return Ok(new
            {
                data = response.Data,
                success = true,
                message = "",
                page = page,
                pageSize = pageSize,
                totalCount = countResponse.Data,
                totalPages = (int)Math.Ceiling((double)countResponse.Data / pageSize)
            });
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
