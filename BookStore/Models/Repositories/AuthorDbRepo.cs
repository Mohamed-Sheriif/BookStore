namespace BookStore.Models.Repositories
{
    public class AuthorDbRepo : IBookStoreRepo<Author>
    {
        BookStoreDbContext db;
        public AuthorDbRepo(BookStoreDbContext _db)
        {
            db = _db;
        }
        public void Add(Author entity)
        {
            db.Authors.Add(entity);
            db.SaveChanges();

        }

        public void Delete(int Id)
        {
            var author = Find(Id);
            db.Authors.Remove(author);
            db.SaveChanges();

        }

        public Author Find(int Id)
        {
            var author = db.Authors.SingleOrDefault(a => a.AuthorId == Id);
            return author;
        }

        public IList<Author> List()
        {
            return db.Authors.ToList();
        }

        public void Update(int Id, Author entity)
        {
            db.Authors.Update(entity);
            db.SaveChanges();
        }
    }
}
