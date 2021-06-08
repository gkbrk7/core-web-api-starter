using System.Collections.Generic;

namespace my_books_api.Data.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation Properties
        public ICollection<Book> Books { get; set; }
    }
}