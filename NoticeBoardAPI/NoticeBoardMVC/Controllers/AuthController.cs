using Microsoft.AspNetCore.Mvc;
using NoticeBoardMVC.Services;
using System.Text.Json;

namespace NoticeBoardMVC.Controllers
{
    public class AuthController: Controller
    {
        private readonly ApiService apiService;
        private readonly IConfiguration configuration;

        public AuthController(ApiService apiService, IConfiguration configuration)
        {
            this.apiService = apiService;
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("JwtToken") != null)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GoogleCallback([FromBody] string idToken)
        {
            var response = await apiService.PostAsync("api/Auth/login", new { IdToken = idToken });

            if (!response.IsSuccessStatusCode)
            {
                return Unauthorized();
            }

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<JsonElement>(content);

            var token = result.GetProperty("token").GetString();
            var user = result.GetProperty("user");

            HttpContext.Session.SetString("JwtToken", token!);
            HttpContext.Session.SetString("UserName", user.GetProperty("displayName").GetString()!);
            HttpContext.Session.SetString("UserEmail", user.GetProperty("email").GetString()!);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await apiService.PostAsync<object>("api/Auth/logout", null!);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
