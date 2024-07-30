namespace Application.ViewModels
{
    public class ApiResult<T> 
    {
        public bool Success { get; set; }
        public string? Msg { get; set; }
        public T? Data { get; set; }
    }

    public class ApiResult
    {
        public bool Success { get; set; }
        public string? Msg { get; set; }
        public object? Data { get; set; }
    }
}
