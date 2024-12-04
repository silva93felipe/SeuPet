using Microsoft.EntityFrameworkCore;
using SeuPet.Models;

namespace SeuPet.Repository
{
    public class AdotanteRepository : IAdotanteRepository
    {
        private readonly SeuPetContext _context;

        public AdotanteRepository(SeuPetContext context)
        {
            _context = context;
        }

        public async Task<Adotante> CreateAsync(Adotante newAdotante)
        {
            await _context.Adotante.AddAsync(newAdotante);
            await Save();
            return newAdotante;
        }

        public async Task<List<Adotante>> GetAllAsync(int limit, int offset)
            => await _context.Adotante
                            .AsNoTracking()
                            .Where(e => e.Ativo)
                            .OrderByDescending(e => e.Id)
                            .Take(limit)
                            .Skip(offset)
                            .ToListAsync();  

        public async Task<Adotante> GetByIdAsync(int id)
            => await _context.Adotante.FirstOrDefaultAsync(e => e.Id == id && e.Ativo);

        public async Task Save()
            => await _context.SaveChangesAsync();
    }
}