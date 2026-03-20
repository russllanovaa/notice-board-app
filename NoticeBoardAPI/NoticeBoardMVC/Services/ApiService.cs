using NoticeBoardMVC.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace NoticeBoardMVC.Services
{
    public class ApiService
    {
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClient = httpClient;
            this.httpContextAccessor = httpContextAccessor;
        }

        private void AttachToken()
        {
            var token = httpContextAccessor.HttpContext?.Session.GetString("JwtToken");
            if(!string.IsNullOrEmpty(token) )
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            AttachToken();
            var responce = await httpClient.GetAsync(endpoint);
            if (!responce.IsSuccessStatusCode) return default;
            var content = await responce.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponseViewModel<T>>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            return result != null && result.Success ? result.Data : default;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data)
        {
            AttachToken();
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await httpClient.PostAsync(endpoint, content);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string endpoint, T data)
        {
            AttachToken();
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await httpClient.PutAsync(endpoint, content);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string endpoit)
        {
            AttachToken();
            return await httpClient.DeleteAsync(endpoit);
        }
    }
}
