using System.Linq;

namespace Publisher.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        T Create(T entity);
        IQueryable<T> List();
        T Get(long id);
        T Update(T update);
        bool Delete(T delete);
    }
}
