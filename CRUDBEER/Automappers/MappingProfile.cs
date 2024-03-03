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
        }
    }
}
