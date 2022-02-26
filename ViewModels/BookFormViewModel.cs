using LibApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.ViewModels
{
    public class BookFormViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Book's name")]
        [StringLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Authors's name")]
        [StringLength(255)]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Please select Genre")]
        [StringLength(255)]
        public  byte GenreId { get; set; }

        [Required(ErrorMessage = "Please pick number")]
        [Range(1,20)]
        public int NumberInStock { get; set; }

        public DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }

    //  public DateTime? DateAdded;
    
    public string Title
        {
            get
            {
                return Id != 0 ? "Edit Book" : "New Book";
            }
        }

        public BookFormViewModel(Book book) {
            Id = book.Id;
            AuthorName = book.AuthorName; 
            Name = book.Name;
            GenreId = book.GenreId;
            NumberInStock = book.NumberInStock;
            ReleaseDate = book.ReleaseDate;
            DateAdded = book.DateAdded;

        }
        public BookFormViewModel() {
            Id = 0;
        }
        


    }
}
