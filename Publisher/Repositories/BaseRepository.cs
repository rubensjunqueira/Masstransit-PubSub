using Microsoft.EntityFrameworkCore;
using Publisher.AppDbContext;
using System;
using System.Linq;

namespace Publisher.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>, IDisposable where T : class
    {
        private PublisherContext _context;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("UnitOfWork");

            _context = unitOfWork as PublisherContext;
        }

        public T Create(T entity)
        {
            var result = _context.Set<T>().Add(entity).Entity;
            _context.SaveChanges();
            return result;
        }

        public bool Delete(T delete)
        {
            _context.Set<T>().Remove(delete);
            return _context.SaveChanges() > 0;
        }

        public T Get(long id)
        {
            return _context.Set<T>().Find(id);
        }

        public IQueryable<T> List()
        {
            return _context.Set<T>();
        }

        public T Update(T update)
        {
            _context.Entry(update).State = EntityState.Modified;
            _context.SaveChanges();
            return update;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
