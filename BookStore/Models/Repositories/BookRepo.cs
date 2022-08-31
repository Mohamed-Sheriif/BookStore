namespace BookStore.Models.Repositories
{
    public class BookRepo : IBookStoreRepo<Book>
    {
        List<Book> books;
        public BookRepo()
        {
            books = new List<Book>()
            {
                new Book(){BookId = 1 , Title = "C# Programming" , Description  = "No Description" , Author = new Author() , ImgURL = "new photo.jpg" },
                new Book(){BookId = 2 , Title = "Java Programming" , Description  = "No Data", Author = new Author() , ImgURL = "new photo.jpg" },
                new Book(){BookId = 3 , Title = "Python Programming" , Description  = "Empty", Author = new Author() , ImgURL = "new photo.jpg"}
            };
        }
        public void Add(Book entity)
        {
            entity.BookId = books.Max(m => m.BookId) + 1;
            books.Add(entity);
        }

        public void Delete(int Id)
        {
            var book = Find(Id);
            books.Remove(book);
        }

        public Book Find(int Id)
        {
            var book = books.SingleOrDefault(b => b.BookId == Id);
            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public void Update(int Id , Book entity)
        {
            var book = Find(Id);
            book.Title = entity.Title;
            book.Description = entity.Description;
            book.Author = entity.Author;
            book.ImgURL = entity.ImgURL;
        }
    }
}
