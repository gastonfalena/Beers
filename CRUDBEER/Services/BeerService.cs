﻿using AutoMapper;
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
        public List<string> Errors{ get; }
        public BeerService(IRepository<Beer> beerRepository,
            IMapper mapper) 
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BeerDto>> Get() 
        {
            var beers = await _beerRepository.Get();
            return beers.Select(b => _mapper.Map<BeerDto>(b));
        }
    public async Task<BeerDto> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);
            if (beer != null)
            {
                var beerDto = _mapper.Map<BeerDto>(beer);
                return beerDto;
            }
            
            return null;
        }
        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            var beer = _mapper.Map<Beer>(beerInsertDto);
            await _beerRepository.Add(beer);
            await _beerRepository.Save();

            var beerDto = _mapper.Map<BeerDto>(beer);
            return beerDto;

        }
        public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {

            var beer = await _beerRepository.GetById(id);
            if (beer != null) 
            {
                // Cuando mandamos dos parametros se va a modificar el objeto existente
                beer = _mapper.Map<BeerUpdateDto, Beer>(beerUpdateDto,beer);

                _beerRepository.Update(beer);
                await _beerRepository.Save();

                var beerDto = _mapper.Map<BeerDto>(beer);





                return beerDto;
            }
            return null;
        }

        public async Task<BeerDto> Delete(int id)
        {
            var beer = await _beerRepository.GetById(id);
            if (beer != null)
            {
                var beerDto = _mapper.Map<BeerDto>(beer);
                _beerRepository.Delete(beer);
                await _beerRepository.Save();

                return beerDto;
            }
            return null;
        }

        public bool Validate(BeerInsertDto beerInsertDto) 
        {
            return true;
        }
        public bool Validate(BeerUpdateDto beerUpdateDto) 
        {
            return true;
        }
    }
}
