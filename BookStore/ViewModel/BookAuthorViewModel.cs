using BookStore.Models;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModel
{
    public class BookAuthorViewModel
    {
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public String Description { get; set; }
        public int AuthorId { get; set; }
        public List<Author> Authors { get; set; }
        public IFormFile File { get; set; }
        public String ImgURL { get; set; }
    }
}
