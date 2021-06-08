using System;

namespace my_books_api.Data.ViewModels
{
    public class CustomActionResultVM<T>
    {
        public Exception Exception { get; set; }
        public T Data { get; set; }
    }
}
