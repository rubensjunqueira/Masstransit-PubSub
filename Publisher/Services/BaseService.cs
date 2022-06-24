using Publisher.Repositories;
using System.Linq;

namespace Publisher.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        private readonly BaseRepository<T> _repository;
        IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repository = new BaseRepository<T>(unitOfWork);
        }

        public virtual T Create(T entity)
        {
            var createdEntity = _repository.Create(entity);
            _unitOfWork.Save();

            return createdEntity;
        }

        public virtual bool Delete(T delete)
        {
            _repository.Delete(delete);
            return _unitOfWork.Save();
        }

        public virtual T Get(long id)
        {
            return _repository.Get(id);
        }

        public virtual IQueryable<T> List()
        {
            return _repository.List();
        }

        public virtual T Update(T update)
        {
            var updatedEntity = _repository.Update(update);
            _unitOfWork.Save();

            return updatedEntity;
        }
    }
}
