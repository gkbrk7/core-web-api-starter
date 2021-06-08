using System;

namespace my_books_api.Data.ViewModels
{
    public class ErrorVM
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Path { get; set; }

        public override string ToString()
        {
            return $"Status Code: {StatusCode}, Message: {Message} - Path: {Path}";
        }
    }
}
