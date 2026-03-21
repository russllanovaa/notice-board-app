using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NoticeBoardMVC.Interfaces;
using NoticeBoardMVC.Models;
using NoticeBoardMVC.Services;
using System.Diagnostics;

namespace NoticeBoardMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiService apiService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IApiService apiService, ILogger<HomeController> logger)
        {
            this.apiService = apiService;
            _logger = logger;
        }

        public IActionResult Index()
        {
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
