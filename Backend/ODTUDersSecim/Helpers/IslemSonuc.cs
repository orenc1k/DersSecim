namespace ODTUDersSecim.Helpers
{
    public class IslemSonuc<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public T? Model { get; set; }

        public IslemSonuc<T> Basarili(T? data = default(T?), string message = "İşlem başarılı.")
        {
            return new IslemSonuc<T>
            {
                Success = true,
                Model = data,
                Message = message
            };
        }

        public IslemSonuc<T> Basarisiz(string message)
        {
            return new IslemSonuc<T>
            {
                Success = false,
                Message = message
            };
        }
    }
}
