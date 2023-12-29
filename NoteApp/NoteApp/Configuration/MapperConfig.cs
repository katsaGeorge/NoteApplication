using AutoMapper;
using NoteApp.Data;
using NoteApp.DTO;

namespace NoteApp.Configuration
{
    public class MapperConfig : Profile
    {

        public MapperConfig()
        {
            CreateMap<UserCreateDTO, User>().ReverseMap();
            CreateMap<UserReadOnlyDTO, User>().ReverseMap();
            CreateMap<UserCreateDTO, UserReadOnlyDTO>().ReverseMap();
            
            CreateMap<NoteCreateDTO, NoteReadOnlyDTO>().ReverseMap();
            CreateMap<NoteCreateDTO, Note>().ReverseMap();
            CreateMap<NoteUpdatedDTO, Note>().ReverseMap();
            CreateMap<NoteReadOnlyDTO, Note>().ReverseMap();
           
        }
    }
}
