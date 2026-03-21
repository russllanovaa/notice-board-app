using NoticeBoardMVC.Models;

namespace NoticeBoardMVC.Interfaces
{
    public interface IApiService
    {
        Task<T?> GetAsync<T>(string endpoint);
        Task<PagedResponseViewModel<T>?> GetPagedAsync<T>(string endpoint);
        Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data);
        Task<HttpResponseMessage> PutAsync<T>(string endpoint, T data);
        Task<HttpResponseMessage> DeleteAsync(string endpoint);
    }
}
