using System.Linq.Expressions;

namespace CarRental_DataAccess.Repository.IRepoSer
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
