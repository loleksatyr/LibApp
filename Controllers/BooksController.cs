using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using LibApp.Services;

namespace LibApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        public string name;
        private readonly IBooksServices _booksServices;

        public BooksController(ApplicationDbContext context, IBooksServices booksServices)
        {
           _context = context;
           _booksServices = booksServices;
        }

        public IActionResult Index()
        {
            var books = _booksServices.GetBooks();
            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = _booksServices.GetBook(id);

            if (book == null)
            {
                return Content("Book not found");
            }

            return View(book);
        }

        public IActionResult Edit(int id)
        {
            var book = _booksServices.GetBook(id); 
            if (book == null)
            {
                return NotFound();
            }

            var viewModel = new BookFormViewModel(book)
            {
                //Book = book,
                Genres = _booksServices.GetGenres()
        };

            return View("BookForm", viewModel);
        }

        public IActionResult New()
        {
            var genres = _booksServices.GetGenres();
            var viewModel = new BookFormViewModel()
            {
                Genres = genres
            };

            return View("BookForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Book book)
        {
            if (book.Id == 0)
            {
                book.DateAdded = DateTime.Now;
                _booksServices.InsertBook(book);
            }
            else
            {
                _booksServices.EditBook(book);
            }

            //try
            //{
            //    _context.SaveChanges();
            //}
            //catch (DbUpdateException e)
            //{
            //    Console.WriteLine(e);
            //}  

            return RedirectToAction("Index", "Books");
        }


    }
}
