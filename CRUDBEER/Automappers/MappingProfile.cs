using AutoMapper;
using CRUDBEER.DTO;
using CRUDBEER.Models;

namespace CRUDBEER.Automappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BeerInsertDto,Beer>();
            CreateMap<Beer, BeerDto>()
                .ForMember(dto=> dto.Id,
                            m=>m.MapFrom(b => b.BeerID));
        }
    }
}
