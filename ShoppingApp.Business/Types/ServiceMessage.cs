namespace ShoppingApp.Business.Types
{
    public class ServiceMessage
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }

    public class ServiceMessage<T> : ServiceMessage
    {
        public T? Data { get; set; }
    }
}
