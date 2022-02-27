using AutoMapper;
using LibApp.Data;
using LibApp.Dtos;
using LibApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BooksController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
       
        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = _context.Books
                                        .Include(c => c.Genre)
                                        .ToList()
                                       .Select(_mapper.Map<Book, BookDto>);

            return Ok(books);
        }

        
        [HttpGet("{id}")]
        public BookDto GetBook(int id)
        {
            var book = _context.Books.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);
            if (book == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }

            return _mapper.Map<BookDto>(book);
        }

        
        [HttpPost]
        public BookDto CreateBook(BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }

            var book = _mapper.Map<Book>(bookDto);
            _context.Books.Add(book);
            _context.SaveChanges();
            bookDto.Id = book.Id;

            return bookDto;
        }


        [HttpPut("{id}")]
        public void UpdateBook(int id, BookDto bookDto)
        {
            {
                if (!ModelState.IsValid)
                {
                    throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
                }

                var bookInDb = _context.Customers.SingleOrDefault(c => c.Id == bookDto.Id);
                if (bookInDb == null)
                {
                    throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
                }


                _mapper.Map(bookDto, bookInDb);
                _context.SaveChanges();
            }
        }
        
        [HttpGet("genres")]
        public IActionResult GetGenres()
        {
            var bookInDb = _context.Genre.ToList();
            if (bookInDb == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }

            return Ok(bookInDb);
        }


       
        [HttpDelete("{id}")]
        public void DeleteBook(int id)
        {
            var bookInDb = _context.Books.SingleOrDefault(c => c.Id == id);
            if (bookInDb == null)
            {
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }

            _context.Books.Remove(bookInDb);
            _context.SaveChanges();
        }

        private ApplicationDbContext _context;
        private readonly IMapper _mapper;

 
        }
      
    }


