using AutoMapper;
using Publisher.DTOs;
using Publisher.Models;

namespace Publisher.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<CreatePersonDTO, Person>()
                .ForMember(dest => dest.FirstName, source => source.MapFrom(x => x.FirstName))
                .ForMember(dest => dest.LastName, source => source.MapFrom(x => x.LastName))
                .ForMember(dest => dest.Email, source => source.MapFrom(x => x.Email));

            CreateMap<Person, PersonCreatedDTO>()
                .ForMember(dest => dest.Id, source => source.MapFrom(x => x.Id))
                .ForMember(dest => dest.FirstName, source => source.MapFrom(x => x.FirstName))
                .ForMember(dest => dest.LastName, source => source.MapFrom(x => x.LastName))
                .ForMember(dest => dest.Email, source => source.MapFrom(x => x.Email));
        }
    }
}
