namespace Web.Core.Framework
{
    public class JsonModel<T>
    {
        // status properties
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        // data properties
        public T Data { get; set; }
        public int TotalItemCount { get; set; }
        public int TotalPage { get; set; }
    }
}
