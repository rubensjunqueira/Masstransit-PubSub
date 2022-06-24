using System.Linq;

namespace Publisher.Services
{
    public interface IBaseService<T> where T : class
    {
        T Get(long id);
        IQueryable<T> List();
        T Create(T entity);
        bool Delete(T delete);
        T Update(T update);
    }
}
