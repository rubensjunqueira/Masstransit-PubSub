using AutoMapper;
using Publisher.DTOs;
using Publisher.Models;
using Publisher.Repositories.Interfaces;
using Publisher.Services.Interfaces;
using System;
using System.Linq;

namespace Publisher.Services.Implementations
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService(IMapper mapper, IPersonRepository personRepository)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public PersonCreatedDTO Create(CreatePersonDTO person)
        {
            var entity = _mapper.Map<CreatePersonDTO, Person>(person);

            if (_personRepository.List().Any(x => x.Email == entity.Email))
                throw new Exception($"{entity.Email} already exists!");

            var result = _personRepository.Create(entity);

            return _mapper.Map<Person, PersonCreatedDTO>(result);
        }
    }
}
