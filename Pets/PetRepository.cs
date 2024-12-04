using Microsoft.EntityFrameworkCore;
using SeuPet.Models;

namespace SeuPet.Repository
{
    public class PetRepository : IPetRepository
    {
        private readonly SeuPetContext _context;

        public PetRepository(SeuPetContext context)
        {
            _context = context;
        }

        public async Task<Pet> CreateAsync(Pet newPet)
        {
            await _context.Pet.AddAsync(newPet);
            await Save();
            return newPet;
        }

        public async Task<List<Pet>> GetAllAsync(int limit, int offset)
            => await _context.Pet
                            .AsNoTracking()
                            .Where(e => e.Ativo && e.AdotanteId != null)
                            .OrderByDescending(e => e.Id)
                            .Take(limit)
                            .Skip(offset)
                            .ToListAsync();  

        public async Task<Pet> GetByIdAsync(int id)
            => await _context.Pet.FirstOrDefaultAsync(e => e.Id == id && e.Ativo && e.AdotanteId != null);

        public async Task Save()
            => await _context.SaveChangesAsync();
    }
}