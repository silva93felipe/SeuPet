using Microsoft.EntityFrameworkCore;
using SeuPet.Domain.Context;
using SeuPet.Domain.Contracts;
using SeuPet.Domain.Entity;

namespace SeuPet.Infra.Repository
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
            await Commit();
            return newPet;
        }

        public async Task Update(Pet request)
        {
            _context.Update(request);
            await Commit();
        }

        public async Task<List<Pet>> GetAllAsync(int limit, int offset)
            => await _context.Pet
                            .AsNoTracking()
                            .Where(e => e.Ativo && e.AdotanteId != null)
                            .OrderByDescending(e => e.Id)
                            .Take(limit)
                            .Skip(offset)
                            .ToListAsync();  

        public async Task<Pet?> GetByIdAsync(int id)
            => await _context.Pet.FirstOrDefaultAsync(e => e.Id == id && e.Ativo && e.AdotanteId != null);

        private async Task Commit()
            => await _context.SaveChangesAsync();
    }
}