namespace BookStore.Models.Repositories
{
    public interface IBookStoreRepo<TEntity>
    {
        IList<TEntity> List();
        TEntity Find(int Id);
        void Add(TEntity entity);
        void Update(int Id , TEntity entity);
        void Delete(int Id);

    }
}
