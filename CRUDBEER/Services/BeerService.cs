using AutoMapper;
using CRUDBEER.DTO;
using CRUDBEER.Models;
using CRUDBEER.Repository;
using Microsoft.EntityFrameworkCore;

namespace CRUDBEER.Services
{
    public class BeerService : ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        
        private IRepository<Beer> _beerRepository;
        private IMapper _mapper;
        public BeerService(IRepository<Beer> beerRepository,
            IMapper mapper) 
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BeerDto>> Get() 
        {
            var beers = await _beerRepository.Get();
            return beers.Select(b => new BeerDto
            {
                Id = b.BeerID,
                Name = b.Name,
                Alcohol = b.Alcohol,
                BrandID = b.BrandID
            });
        }
    public async Task<BeerDto> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);
            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BeerID,
                    Name = beer.Name,
                    Alcohol = beer.Alcohol,
                    BrandID = beer.BrandID
                };
                return beerDto;
            }
            
            return null;
        }
        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            var beer = _mapper.Map<Beer>(beerInsertDto);
            await _beerRepository.Add(beer);
            await _beerRepository.Save();

            var beerDto = new BeerDto
            {
                Id = beer.BrandID,
                Name = beer.Name,
                BrandID = beer.BrandID,
                Alcohol = beer.Alcohol
            };
            return beerDto;

        }
        public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {

            var beer = await _beerRepository.GetById(id);
            if (beer != null) 
            {
                beer.Name = beerUpdateDto.Name;
                beer.Alcohol = beerUpdateDto.Alcohol;
                beer.BrandID = beerUpdateDto.BrandID;

                _beerRepository.Update(beer);
                await _beerRepository.Save();

                var beerDto = new BeerDto
                {
                    Id = beer.BrandID,
                    Name = beer.Name,
                    BrandID = beer.BrandID,
                    Alcohol = beer.Alcohol
                };

                return beerDto;
            }
            return null;
        }

        public async Task<BeerDto> Delete(int id)
        {
            var beer = await _beerRepository.GetById(id);
            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BrandID,
                    Name = beer.Name,
                    BrandID = beer.BrandID,
                    Alcohol = beer.Alcohol
                };
                _beerRepository.Delete(beer);
                await _beerRepository.Save();

                return beerDto;
            }
            return null;
        }

      

        

        
    }
}
