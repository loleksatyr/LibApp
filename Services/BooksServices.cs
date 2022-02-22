using LibApp.Data;
using LibApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Services
{
    public interface IBooksServices
    {
        Book GetBook(int id);
        IEnumerable<Book> GetBooks();
        IEnumerable<Genre> GetGenres();
        Book InsertBook(Book book);
        Book EditBook(Book book);
        
    }


    public class BooksServices : IBooksServices
    {
        ApplicationDbContext _context;
        public BooksServices(ApplicationDbContext dbContext) {
            _context = dbContext;
        }
        
        public Book EditBook(Book book)
        {
            var bookInDb = _context.Books.SingleOrDefault(b => b.Id == book.Id);
            bookInDb.Name = book.Name;
            bookInDb.AuthorName = book.AuthorName;
            bookInDb.ReleaseDate = book.ReleaseDate;
            bookInDb.GenreId = book.GenreId;
            bookInDb.NumberInStock = book.NumberInStock;
            _context.SaveChanges();
            return bookInDb;
        }

        public Book GetBook(int id)
        {
             return _context.Books.Include(b => b.Genre).SingleOrDefault(b => b.Id == id);
        }

        public IEnumerable<Book> GetBooks()
        {
            return _context.Books.Include(b => b.Genre).ToList();
        }

        public IEnumerable<Genre> GetGenres()
        {
            return _context.Genre.ToList();
        }

        public Book InsertBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }


    }
    
}
