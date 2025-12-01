using CarRental_DataAccess.Data;
using CarRental_DataAccess.Repository.IRepoSer;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarRental_DataAccess.Repository.RepoSer
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CarRentalDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(CarRentalDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> values = dbSet;
            values = values.Where(filter);
            return values.FirstOrDefault();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
