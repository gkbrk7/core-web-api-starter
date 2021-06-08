using System;
using System.Collections.Generic;

namespace my_books_api.Data.ViewModels
{
    public class PublisherVM
    {
        public string Name { get; set; }
    }

    public class PublisherWithBooksAndAuthorsVM
    {
        public string Name { get; set; }
        public ICollection<BookAuthorVM> BookAuthors { get; set; }

    }

    public class BookAuthorVM
    {
        public string BookName { get; set; }
        public ICollection<string> BookAuthors { get; set; }
    }
}
