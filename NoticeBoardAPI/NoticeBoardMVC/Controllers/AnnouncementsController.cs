using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoticeBoardMVC.Interfaces;
using NoticeBoardMVC.Enums;
using NoticeBoardMVC.Models;
using NoticeBoardMVC.Services;
using System.Text.Json;

namespace NoticeBoardMVC.Controllers
{
    public class AnnouncementsController: Controller
    {
        private readonly IApiService apiService;
        public AnnouncementsController(IApiService apiService)
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
                    Text = c.NameOfCategory,
                    Selected = c.Id == model.CategoryId 
                }).ToList();

            if (model.CategoryId > 0)
            {
                var subCategories = await apiService.GetAsync<List<SubCategoryViewModel>>(
                    $"api/Categories/{model.CategoryId}/subcategories") ?? new();
                model.SubCategories = subCategories
                    .Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.NameOfSubCategory,
                        Selected = s.Id == model.SubCategoryId 
                    }).ToList();
            }
        }

        [HttpGet]
        public async Task<IActionResult> MyAnnouncements(int page = 1)
        {
            if (!IsAuthenticated) return RedirectToAction("Login", "Auth");

            const int pageSize = 6;

            var pagedResult = await apiService.GetPagedAsync<List<AnnouncementViewModel>>(
                $"api/Announcements/my?page={page}&pageSize={pageSize}") ?? new();

            var announcements = pagedResult.Data ?? new();

            var model = new MyAnnouncementsViewModel
            {
                Announcements = announcements,
                Total = pagedResult.TotalCount,
                Active = announcements.Count(a => a.StatusOfNote == AnnouncementStatus.Active),
                Inactive = announcements.Count(a => a.StatusOfNote == AnnouncementStatus.Inactive),
                CurrentPage = page,
                TotalPages = pagedResult.TotalPages
            };

            return View(model);
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
                SubCategoryId = model.SubCategoryId,
                model.Title,
                model.DescriptionOfNote,
                StatusOfNote = model.StatusOfNote
            });

            if (responce.IsSuccessStatusCode)
            {
                return RedirectToAction("All", "Announcements");
            }

            ModelState.AddModelError("", "Помилка при створенні оголошення");
            await FillCategoriesAsync(model);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> All(int? categoryId, int? subCategoryId, int page = 1)
        {
            const int pageSize = 6;

            var pagedResult = await apiService.GetPagedAsync<List<AnnouncementViewModel>>(
                $"api/Announcements?page={page}&pageSize={pageSize}") ?? new();

            var announcements = pagedResult.Data ?? new();
            var categories = await apiService.GetAsync<List<CategoryViewModel>>("api/Categories") ?? new();

            if (categoryId.HasValue)
            {
                announcements = announcements
                    .Where(a => a.NameOfCategory == categories
                        .FirstOrDefault(c => c.Id == categoryId)?.NameOfCategory)
                    .ToList();
            }

            if (subCategoryId.HasValue)
            {
                var subCategories = await apiService.GetAsync<List<SubCategoryViewModel>>(
                    $"api/Categories/{categoryId}/subcategories") ?? new();
                announcements = announcements
                    .Where(a => a.NameOfSubCategory == subCategories
                        .FirstOrDefault(s => s.Id == subCategoryId)?.NameOfSubCategory)
                    .ToList();
            }

            var categoryItems = categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.NameOfCategory,
                    Selected = c.Id == categoryId
                }).ToList();

            categoryItems.Insert(0, new SelectListItem { Value = "", Text = "Всі категорії" });

            var model = new AllAnnouncementsViewModel
            {
                Announcements = announcements,
                Categories = categoryItems,
                SelectedCategoryId = categoryId,
                SelectedSubCategoryId = subCategoryId,
                CurrentPage = page,
                TotalPages = pagedResult.TotalPages,
                TotalCount = pagedResult.TotalCount
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!IsAuthenticated) return RedirectToAction("Login", "Auth");

            var announcement = await apiService.GetAsync<AnnouncementViewModel>($"api/Announcements/{id}");
            if (announcement == null) return NotFound();

            var model = new CreateAnnouncementViewModel
            {
                CategoryId = announcement.CategoryId,
                SubCategoryId = announcement.SubCategoryId,
                Title = announcement.Title,
                DescriptionOfNote = announcement.DescriptionOfNote,
                StatusOfNote = announcement.StatusOfNote
            };

            await FillCategoriesAsync(model);
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
                SubCategoryId = model.SubCategoryId,
                model.Title,
                model.DescriptionOfNote,
                StatusOfNote = model.StatusOfNote.ToString()
            });

            if (responce.IsSuccessStatusCode)
            {
                return RedirectToAction("All", "Announcements");
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
            return RedirectToAction("All", "Announcements");
        }
    }
}
