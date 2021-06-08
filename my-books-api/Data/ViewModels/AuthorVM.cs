using System;
using System.Collections.Generic;

namespace my_books_api.Data.ViewModels
{
    public class AuthorVM
    {
        public string FullName { get; set; }
    }

    public class AuthorWithBooksVM
    {
        public string FullName { get; set; }
        public ICollection<string> BookTitles { get; set; }
    }
}
