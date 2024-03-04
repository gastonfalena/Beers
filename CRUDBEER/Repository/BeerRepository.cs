﻿using CRUDBEER.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDBEER.Repository
{
    public class BeerRepository : IRepository<Beer>
    {
        private StoreContext _context;
        public BeerRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Beer>> Get()
            => await _context.Beers.ToListAsync();
        public async Task<Beer> GetById(int id)
            => await _context.Beers.FindAsync(id);
        public async Task Add(Beer entity)
            => await _context.Beers.AddAsync(entity);
        public void Update(Beer entity)
        {
            _context.Beers.Attach(entity);
            _context.Beers.Entry(entity).State = EntityState.Modified;
        }
        public void Delete(Beer entity)
            => _context.Beers.Remove(entity);
        public async Task Save()
           => await _context.SaveChangesAsync();

        public IEnumerable<Beer> Search(Func<Beer,bool> filter) =>
            //Desacoplamos el metodo Search para que la responabilidad la tenga quien invoca a search
            _context.Beers.Where(filter).ToList();
    }
}
