using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoticeBoardMVC.Models;
using NoticeBoardMVC.Services;
using System.Text.Json;

namespace NoticeBoardMVC.Controllers
{
    public class AnnouncementsController: Controller
    {
        private readonly ApiService apiService;
        public AnnouncementsController(ApiService apiService)
        {
            this.apiService = apiService;
        }

        private bool IsAuthenticated => HttpContext.Session.GetString("JwtToken") != null;

        private async Task FillCategoriesAsync(CreateAnnouncementViewModel model)
        {
            var categories = await apiService.GetAsync<List<CategoryViewModel>>("api/Categories") ?? new();
            model.Categories = categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.NameOfCategory
                }).ToList();

            if(model.Categories.Count > 0)
            {
                var subCategories = await apiService.GetAsync<List<SubCategoryViewModel>>(
                    $"api/Categories/{model.CategoryId}/subcategories") ?? new();
                model.SubCategories = subCategories
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.NameOfSubCategory
                    }).ToList();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (!IsAuthenticated) return RedirectToAction("Login", "Auth");

            var model = new CreateAnnouncementViewModel();
            await FillCategoriesAsync(model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAnnouncementViewModel model)
        {
            if (!IsAuthenticated) return RedirectToAction("Login", "Auth");

            if (!ModelState.IsValid)
            {
                await FillCategoriesAsync(model);
                return View(model);
            }

            var responce = await apiService.PostAsync("api/Announcements", new
            {
                model.CategoryId,
                SubCategoryId = model.CategoryId,
                model.Title,
                model.DescriptionOfNote,
                StatusOfNote = model.StatusOfNote
            });

            if (responce.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Помилка при створенні оголошення");
            await FillCategoriesAsync(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAuthenticated) return RedirectToAction("Login", "Auth");

            var announcement = await apiService.GetAsync<AnnouncementViewModel>($"api/Announcement/{id}");
            if (announcement == null) return NotFound();

            var model = new CreateAnnouncementViewModel
            {
                Title = announcement.Title,
                DescriptionOfNote = announcement.Description,
                StatusOfNote = Enum.Parse<NoticeBoardMVC.Enums.AnnouncementStatus>(announcement.StatusOfNote, true)
            };

            await FillCategoriesAsync (model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateAnnouncementViewModel model)
        {
            if (!IsAuthenticated) return RedirectToAction("Login", "Auth");

            if(!ModelState.IsValid)
            {
                await FillCategoriesAsync(model);
                return View(model);
            }

            var responce = await apiService.PutAsync($"api/Announcements/{id}", new
            {
                model.CategoryId,
                SubCategory = model.SubCategoryId,
                model.Title,
                model.DescriptionOfNote,
                StatusOfNome = model.StatusOfNote.ToString()
            });

            if (responce.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Помилка при оновленні оголошення");
            await FillCategoriesAsync(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var announcement = await apiService.GetAsync<AnnouncementViewModel>($"api/Announcements/{id}");
            if(announcement == null) return NotFound();
            return View(announcement);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsAuthenticated) return RedirectToAction("Login", "Auth");

            await apiService.DeleteAsync($"api/Announcements/{id}");
            return RedirectToAction("Index", "Home");
        }
    }
}
