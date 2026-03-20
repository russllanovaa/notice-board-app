using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoticeBoardMVC.Models;
using NoticeBoardMVC.Services;
using System.Diagnostics;

namespace NoticeBoardMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiService apiService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ApiService apiService, ILogger<HomeController> logger)
        {
            this.apiService = apiService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int? categoryId, int? subCategoryId)
        {
            var announcements = await apiService.GetAsync<List<AnnouncementViewModel>>("api/Announcements") ?? new();
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

            categoryItems.Insert(0, new SelectListItem
            {
                Value = "",
                Text = "Всі категорії"
            });

            var model = new HomeViewModel
            {
                Announcements = announcements,
                Categories = categoryItems,
                SelectCategoryId = categoryId,
                SelectSubCategoryId = subCategoryId
            };

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
