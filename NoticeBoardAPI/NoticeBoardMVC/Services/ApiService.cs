using NoticeBoardMVC.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using NoticeBoardMVC.Interfaces;
using NoticeBoardMVC.Models;

namespace NoticeBoardMVC.Services
{
    public class ApiService: IApiService
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

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        };

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            AttachToken();
            var responce = await httpClient.GetAsync(endpoint);
            if (!responce.IsSuccessStatusCode) return default;
            var content = await responce.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponseViewModel<T>>(content, _jsonOptions);
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
