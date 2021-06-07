using System;
using Microsoft.EntityFrameworkCore;
using my_books_api.Data.Models;

namespace my_books_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
