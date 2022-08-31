using Microsoft.EntityFrameworkCore;

namespace BookStore.Models.Repositories
{
    public class BookDbRepo : IBookStoreRepo<Book>
    {
        BookStoreDbContext db;
        public BookDbRepo(BookStoreDbContext _db)
        {
            db = _db;
        }
        public void Add(Book entity)
        {
            db.Books.Add(entity);
            db.SaveChanges();

        }

        public void Delete(int Id)
        {
            var book = Find(Id);
            db.Books.Remove(book);
            db.SaveChanges();

        }

        public Book Find(int Id)
        {
            var book = db.Books.Include(a => a.Author).SingleOrDefault(b => b.BookId == Id);
            return book;
        }

        public IList<Book> List()
        {
            return db.Books.Include(a => a.Author).ToList();
        }

        public void Update(int Id, Book entity)
        {
            db.Books.Update(entity);
            db.SaveChanges();
        }
    }
}
