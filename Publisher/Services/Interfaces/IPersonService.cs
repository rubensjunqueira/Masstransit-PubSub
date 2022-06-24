using Publisher.DTOs;
using Publisher.Models;
using Publisher.Repositories;

namespace Publisher.Services.Interfaces
{
    public interface IPersonService
    {
        PersonCreatedDTO Create(CreatePersonDTO person);
    }
}
