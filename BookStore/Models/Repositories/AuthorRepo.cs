namespace BookStore.Models.Repositories
{
    public class AuthorRepo : IBookStoreRepo<Author>
    {
        List<Author> Authors;
        public AuthorRepo()
        {
            Authors = new List<Author>()
            {
                new Author(){AuthorId = 1 , FullName = "Mohamed Sherif"},
                new Author(){AuthorId = 2 , FullName = "Omar Samir"},
                new Author(){AuthorId = 3 , FullName = "Ahmed Fathy"}
            };
        }
        public void Add(Author entity)
        {
            entity.AuthorId = Authors.Max(m => m.AuthorId) + 1;
            Authors.Add(entity);
        }

        public void Delete(int Id)
        {
            var author = Find(Id);
            Authors.Remove(author);
        }

        public Author Find(int Id)
        {
            var author = Authors.SingleOrDefault(a => a.AuthorId == Id);
            return author;
        }

        public IList<Author> List()
        {
            return Authors;
        }

        public void Update(int Id, Author entity)
        {
            var author = Find(Id);
            author.FullName = entity.FullName;
        }
    }
}
