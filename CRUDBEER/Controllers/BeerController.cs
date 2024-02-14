using CRUDBEER.DTO;
using CRUDBEER.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDBEER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        public StoreContext _context;
        public BeerController(StoreContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get() =>
            await _context.Beers.Select(b => new BeerDto
            {
                Id = b.BeerID,
                Name = b.Name,
                Alcohol = b.Alcohol,
                BrandID = b.BrandID
            }).ToListAsync();
        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }
            var beerDto = new BeerDto
            {
                Id = beer.BeerID,
                Name = beer.Name,
                Alcohol = beer.Alcohol,
                BrandID = beer.BrandID
            };
            return Ok(beerDto);
        }
        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
        {
            var beer = new Beer
            {
                Name = beerInsertDto.Name,
                BrandID = beerInsertDto.BrandID,
                Alcohol = beerInsertDto.Alcohol
            };
            await _context.Beers.AddAsync(beer);
            await _context.SaveChangesAsync();
            var beerDto = new BeerDto
            {
                Id = beer.BrandID,
                Name = beer.Name,
                BrandID = beer.BrandID,
                Alcohol = beer.Alcohol
            };

            return CreatedAtAction(nameof(GetById), new { id = beer.BeerID }, beerDto);

        }
        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }

            beer.Name = beerUpdateDto.Name;
            beer.Alcohol = beerUpdateDto.Alcohol;
            beer.BrandID = beerUpdateDto.BrandID;
            await _context.SaveChangesAsync();

            var beerDto = new BeerDto
            {
                Id = beer.BrandID,
                Name = beer.Name,
                BrandID = beer.BrandID,
                Alcohol = beer.Alcohol
            };
            return Ok(beerDto);


        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }
            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();
            return NoContent();
        }
            
            
    }
}
