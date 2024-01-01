using AutoMapper;

namespace MVC_WebInterface.Mapping
{
    public class AutoMapperBaseProfile : Profile
    {
        public AutoMapperBaseProfile()
        {
            CreateMap<Personel, PersonelDTO>()                
                .ForMember(dto => dto.Id, m => m.MapFrom(x => x.Sicil))                
                .ReverseMap();

        }
    }
}
