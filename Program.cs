using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;       
using BookLibrary.Models;
using BookLibrary.Data;                    

class Program
{
    static void Main()
    {
        using var context = new AppDbContext();

        if (!context.Books.Any())
        {
            var user = new User { FirstName = "John" };
            var book = new Book { Title = "The Alchemist", ISBN = "9780061122415" };

            context.Users.Add(user);
            context.Books.Add(book);
            context.SaveChanges();

            var borrow = new BorrowRecord
            {
                BookId = book.BookId,
                UserId = user.UserId,
                BorrowDate = DateTime.Now
            };

            context.BorrowRecords.Add(borrow);
            context.SaveChanges();

            Console.WriteLine("Data seeded.");
        }

        
        var borrowedBooks = context.BorrowRecords
            .Include(br => br.Book)
            .Include(br => br.User)
            .Select(br => new
            {
                br.Book.Title,
                br.Book.ISBN,
                br.User.FirstName,
                br.BorrowDate
            })
            .ToList();

        foreach (var item in borrowedBooks)
        {
            Console.WriteLine($"{item.FirstName} borrowed '{item.Title}' (ISBN: {item.ISBN}) on {item.BorrowDate}");
        }
    }
}
