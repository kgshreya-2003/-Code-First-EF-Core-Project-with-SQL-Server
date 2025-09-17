using BookLibrary.Models;  // Assuming your Models are under this namespace
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data  // Add a namespace for better organization
{
    public class AppDbContext : DbContext
    {
        // DbSets represent tables in your database
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BorrowRecord> BorrowRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=BookLibraryDB;Trusted_Connection=True;");
        }
    }
}
