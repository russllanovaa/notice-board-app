namespace NoticeBoardAPI.Common
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public int? Page { get; set; }
        public int? PageSize { get; set; }

        public static ServiceResponse<T> Ok(T data, string message = "") => new()
        {
            Data = data,
            Success = true,
            Message = message
        };

        public static ServiceResponse<T> Fail(string message) => new()
        {
            Data = default,
            Success = false,
            Message = message
        };
    }
}
