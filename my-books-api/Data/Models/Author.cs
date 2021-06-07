using System;
using System.Collections.Generic;

namespace my_books_api.Data.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        // Navigation Properties
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
