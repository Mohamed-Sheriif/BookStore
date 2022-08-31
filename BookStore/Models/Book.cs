using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public String Description { get; set; }
        [Display(Name ="Image")]
        public String ImgURL { get; set; }
        public Author Author { get; set; }
    }
}
