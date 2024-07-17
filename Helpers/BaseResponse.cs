namespace KitapService.Helpers
{
    public class BaseResponse<T>
    {
        public bool success { get; set; }
        public T? data { get; set; }
        public string? error { get; set; }
        public int count { get; set; } = 0;
    }
}
