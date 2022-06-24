using Publisher.AppDbContext;
using Publisher.Models;
using Publisher.Repositories.Interfaces;
using System.Linq;

namespace Publisher.Repositories.Implementations
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
